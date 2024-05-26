using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FBC.Models;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Drawing;
using System.Drawing.Imaging;

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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("Title,Author,Publisher,Description,Condition,Credit,NoPage,Weight,Width,Height,Length")] ExchangeRequest exchangeRequest, int[] categories,
            IFormFile frontImage, IFormFile backImage, IFormFile spineImage, IFormFile edgeImage,
            string front, string back, string spine, string edge)
        {
            var lastId = await _context.ExchangeRequests.OrderByDescending(e => e.Id).Select(e => e.Id).FirstOrDefaultAsync();
            var filename = 1;
            CropData cropData = new();
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/asset/image/exchange");
            string filePath;

            var user = HttpContext.Current.User;

            if (user.Identity.IsAuthenticated)
            {
                var userId = user.Identity.Name; // Lấy ID người dùng
                var username = user.Claims.FirstOrDefault(c => c.Type == "name")?.Value; // Lấy tên người dùng
                var email = user.Claims.FirstOrDefault(c => c.Type == "email")?.Value; // Lấy email người dùng
            }

            ExchangeRequest rq = new()
            {
                Title = exchangeRequest.Title,
                Author = exchangeRequest.Author,
                Publisher = exchangeRequest.Publisher,
                Description = exchangeRequest.Description,
                Condition = exchangeRequest.Condition,
                Status = 0,
                Credit = exchangeRequest.Credit
            };
            foreach (var id in categories)
            {
                var cat = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
                rq.Categories.Add(cat);
            }

            if (!string.IsNullOrEmpty(front))
            {
                cropData = JsonConvert.DeserializeObject<CropData>(front);
                filePath = Path.Combine(path, filename+"_front.png");

                // Crop and Save image;
                CropSaveImage(cropData, frontImage, filePath);
                rq.Image1 = filePath;
            }
            if (!string.IsNullOrEmpty(back))
            {
                cropData = JsonConvert.DeserializeObject<CropData>(back);
                filePath = Path.Combine(path, filename + "_back.png");
                CropSaveImage(cropData, backImage, filePath);
                rq.Image2 = filePath;
            }
            if (!string.IsNullOrEmpty(spine))
            {
                cropData = JsonConvert.DeserializeObject<CropData>(spine);
                filePath = Path.Combine(path, filename + "_spine.png");
                CropSaveImage(cropData, spineImage, filePath);
                rq.Image3 = filePath;
            }
            if (!string.IsNullOrEmpty(edge))
            {
                cropData = JsonConvert.DeserializeObject<CropData>(edge);
                filePath = Path.Combine(path, filename + "_edge.png");
                CropSaveImage(cropData,edgeImage, filePath);
                rq.Image4 = filePath;
            }
            await Console.Out.WriteLineAsync(rq.ToString());

            _context.SaveChanges();
            return RedirectToAction("Index", "");
        }

        private void CropSaveImage(CropData? cropData, IFormFile image, string filePath)
        {
            Rectangle crop = new Rectangle(cropData.x, cropData.y, cropData.width, cropData.height);
            Image img;
            using (Stream stream = image.OpenReadStream())
            {
                img = Image.FromStream(stream);
            }
            var bmp = new Bitmap(crop.Width, crop.Height);
            using (var gr = Graphics.FromImage(bmp))
            {
                gr.DrawImage(img, new Rectangle(0, 0, bmp.Width, bmp.Height), crop, GraphicsUnit.Pixel);
            }
            bmp.Save(filePath);
        }
    }
}
