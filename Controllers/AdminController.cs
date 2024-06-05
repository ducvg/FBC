using Azure.Core;
using FBC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Drawing.Printing;
using System.Text;

namespace FBC.Controllers
{
    [Route("[controller]")]
    public class AdminController : Controller
    {
        private readonly Fbc1Context _context;

        public AdminController(Fbc1Context context)
        {
            _context = context;
        }

        [HttpPost("ExchangeRequest/Complete/{requestId:int}")]
        public async Task<IActionResult> CompleteRequest(int requestId, int ConditionNum, string bookDescription, int[] categories, 
            [Bind("Title,Author,Publisher,Response,Credit,Condition,NoPage,Weight,Width,Height,Length")] ExchangeRequest request)
        {
            var rq = await _context.ExchangeRequests.FirstOrDefaultAsync(c => c.ExchangeId == requestId);
            if(rq == null)
            {
                return NotFound();
            }
            rq.CompleteDate = DateOnly.FromDateTime(DateTime.Now);
            rq.Status = 2;
            rq.Title = request.Title;
            rq.Author = request.Author;
            rq.Publisher = request.Publisher;
            rq.Response = request.Response;
            rq.Credit = request.Credit; 
            rq.Condition = request.Condition;
            rq.NoPage = request.NoPage;
            rq.Weight = request.Weight;
            rq.Width = request.Width;
            rq.Height = request.Height;
            rq.Length = request.Length;
            rq.Categories.Clear();
            foreach (var id in categories)
            {
                var cat = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
                rq.Categories.Add(cat);
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
                return Redirect("/Admin/ExchangeRequest/detail/" + requestId);
            }

            Book book = new()
            {
                Title = rq.Title,
                Author = rq.Author,
                Publisher = rq.Publisher,
                Description = bookDescription,
                Condition = rq.Condition,
                ConditionNumeric = ConditionNum,
                NoPage = rq.NoPage.Value,
                Weight = rq.Weight,
                Width = rq.Width,
                Height = rq.Height,
                Length = rq.Length,
                Image1 = rq.Image1,
                Image2 = rq.Image2,
                Image3 = rq.Image3,
                Image4 = rq.Image4,
                Status = 1,
                Credit = rq.Credit
            };
            foreach (var cate in rq.Categories)
            {
                book.Categories.Add(cate);
            }

            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return Redirect("/Admin/ExchangeRequest/detail/"+requestId);
        }

        [HttpPost("ExchangeRequest/detail/{requestId:int}")]
        public async Task<IActionResult> Detail(int requestId, string response, int status)
        {
            var rq = await _context.ExchangeRequests.FirstOrDefaultAsync(c => c.ExchangeId == requestId);
            if(rq == null)
            {
                return NotFound();
            }
            rq.Response = response;
            rq.Status = status;
            if (status == 3)
            {
                rq.CompleteDate = DateOnly.FromDateTime(DateTime.Now);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("ExchangeRequest","Admin");
        }

        [HttpGet("ExchangeRequest/detail/{requestId:int}")]
        public async Task<IActionResult> Detail(int requestId)
        {
            var rq = await _context.ExchangeRequests.Include(u => u.User).Include(c => c.Categories).FirstOrDefaultAsync(c => c.ExchangeId == requestId);
            if(rq == null)
            {
                return NotFound();
            }
            var allCategories = await _context.Categories.ToListAsync();
            var rqCategoryIds = rq.Categories.Select(c => c.CategoryId).ToList();
            var orderedCategories = rq.Categories.ToList();
            orderedCategories.AddRange(allCategories.Where(c => !rqCategoryIds.Contains(c.CategoryId)));
            ViewBag.Categories = orderedCategories;

            return View(rq);
        }

        [HttpGet("ExchangeRequest/detail")]
        public async Task<IActionResult> IndexDetail()
        {
            return View();
        }

        [Route("ExchangeRequest/{pageNumber:int?}")]
        public async Task<IActionResult> ExchangeRequest(int? pageNumber, string? email, int? creditFrom, int? creditTo, int? status, DateTime? createDateFrom, DateTime? createDateTo)
        {
            int pageSize = 10;
            int currentPageNumber = pageNumber ?? 1;
            var query = _context.ExchangeRequests.AsQueryable();

            // Apply filters
            if (!string.IsNullOrEmpty(email))
            {
                query = query.Where(r => r.User.NormalizedEmail.Contains(email.ToUpper()));
            }
            if (creditFrom.HasValue)
            {
                query = query.Where(r => r.Credit >= creditFrom.Value);
            }
            if (creditTo.HasValue)
            {
                query = query.Where(r => r.Credit <= creditTo.Value);
            }
            if (status.HasValue && status != -1)
            {
                query = query.Where(r => r.Status == status.Value);
            }
            if (createDateFrom.HasValue)
            {
                query = query.Where(r => r.CreateDate >= DateOnly.FromDateTime(createDateFrom.Value));
            }
            if (createDateTo.HasValue)
            {
                query = query.Where(r => r.CreateDate <= DateOnly.FromDateTime(createDateTo.Value));
            }

            // Pagination
            int count = await query.CountAsync();
            ViewData["totalPage"] = (int)Math.Ceiling(count / (double)pageSize);

            List<ExchangeRequest> requests = await query
                .Include(r => r.User)
                .Skip((currentPageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return View(requests);
        }


    }
}
