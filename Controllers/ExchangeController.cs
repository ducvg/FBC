﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FBC.Models;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations;
using System.Text;

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
            var filename = lastId+1;
            CropData cropData = new();
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/asset/image/exchange");
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
                filePath = Path.Combine(path, filename+"_front.png");

                // Crop and Save image;
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
                CropSaveImage(cropData,edgeImage, filePath);
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
