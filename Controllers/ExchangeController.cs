using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FBC.Models;
using Newtonsoft.Json;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations;
using System.Text;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace FBC.Controllers
{
    public class ExchangeController : Controller
    {
        private readonly Fbc1Context _context;

        public ExchangeController(Fbc1Context context)
        {
            _context = context;
        }

        // GET: Exchange
        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories.ToListAsync();
            return View(categories);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("Title,Author,Publisher,Description,Condition,Credit,NoPage,Weight,Width,Height,Length")] ExchangeRequest exchangeRequest, int[] categories,
            IFormFile frontImage, IFormFile backImage, IFormFile spineImage, IFormFile edgeImage,
            string front, string back, string spine, string edge)
        {
            var lastId = await _context.ExchangeRequests.OrderByDescending(e => e.ExchangeId).Select(e => e.ExchangeId).FirstOrDefaultAsync();
            var filename = lastId + 1;
            CropData cropData = new();
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/asset/image/exchange");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string filePath;

            ExchangeRequest rq = new()
            {
                Title = exchangeRequest.Title.Trim(),
                Author = exchangeRequest.Author.Trim(),
                Publisher = exchangeRequest.Publisher.Trim(),
                Description = exchangeRequest.Description?.Trim(),
                Condition = exchangeRequest.Condition.Trim(),
                Status = 0,
                Credit = exchangeRequest.Credit,
                NoPage = exchangeRequest.NoPage,
                Weight = exchangeRequest.Weight,
                Width = exchangeRequest.Width,
                Height = exchangeRequest.Height,
                Length = exchangeRequest.Length,
                Id = User.Identity.GetUserId()
            };
            foreach (var id in categories.Take(5))
            {
                var cat = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
                rq.Categories.Add(cat);
            }
            string serverPath = "/asset/image/exchange/";
            if (!string.IsNullOrEmpty(front))
            {
                cropData = JsonConvert.DeserializeObject<CropData>(front);
                filePath = Path.Combine(path, filename + "_front.png");

                // Crop and Save image
                CropSaveImage(cropData, frontImage, filePath);
                rq.Image1 = serverPath + filename + "_front.png";
            }
            if (!string.IsNullOrEmpty(back))
            {
                cropData = JsonConvert.DeserializeObject<CropData>(back);
                filePath = Path.Combine(path, filename + "_back.png");
                CropSaveImage(cropData, backImage, filePath);
                rq.Image2 = serverPath + filename + "_back.png";
            }
            if (!string.IsNullOrEmpty(spine))
            {
                cropData = JsonConvert.DeserializeObject<CropData>(spine);
                filePath = Path.Combine(path, filename + "_spine.png");
                CropSaveImage(cropData, spineImage, filePath);
                rq.Image3 = serverPath + filename + "_spine.png";
            }

            if (!string.IsNullOrEmpty(edge))
            {
                cropData = JsonConvert.DeserializeObject<CropData>(edge);
                filePath = Path.Combine(path, filename + "_edge.png");
                CropSaveImage(cropData, edgeImage, filePath);
                rq.Image4 = serverPath + filename + "_edge.png";
            }

            var validationContext = new ValidationContext(rq, null, null);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(rq, validationContext, validationResults, true);

            if (!isValid)
            {
                var errorDetailBuilder = new StringBuilder();
                foreach (var validationResult in validationResults)
                {
                    errorDetailBuilder.AppendLine(validationResult.ErrorMessage + "<br>");
                }
                TempData["SubmitError"] = "Dữ liệu nhập không hợp lệ, vui lòng kiểm tra lại !";
                TempData["SubmitErrorDetail"] = errorDetailBuilder.ToString();
                return RedirectToAction("Index", "Exchange");
            }
            rq.CreateDate = DateOnly.FromDateTime(DateTime.Now); ;

            _context.ExchangeRequests.Add(rq);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "");
        }

        private void CropSaveImage(CropData? cropData, IFormFile image, string filePath)
        {
            if (cropData == null)
            {
                throw new ArgumentNullException(nameof(cropData));
            }

            using (var stream = image.OpenReadStream())
            using (Image<Rgba32> img = Image.Load<Rgba32>(stream))
            {
                // Apply optional transformations
                if (cropData.rotate.HasValue)
                {
                    img.Mutate(x => x.Rotate((float)cropData.rotate.Value));
                }

                if (cropData.scaleX.HasValue || cropData.scaleY.HasValue)
                {
                    int scaleX = cropData.scaleX ?? 1;
                    int scaleY = cropData.scaleY ?? 1;
                    img.Mutate(x => x.Resize(img.Width * scaleX, img.Height * scaleY));
                }

                // Apply cropping
                var cropRectangle = new Rectangle(cropData.x, cropData.y, cropData.width, cropData.height);
                img.Mutate(x => x.Crop(cropRectangle));

                // Save the result
                img.Save(filePath);
            }
        }
    }
}
