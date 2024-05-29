using FBC.Models;
using FBC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FBC.ViewComponents
{
    public class MenuCategoryViewComponent: ViewComponent
    {
        private readonly Fbc1Context _context;

        public MenuCategoryViewComponent(Fbc1Context context) => _context = context;
        public IViewComponentResult Invoke()
        {
            var data = _context.Categories.Select(c => new MenuCategoriesVM
            {
                categoryId = c.CategoryId,
                categoryName = c.Name,
            }).OrderBy(c=>c.categoryName);
            return View("Default",data);
        }
    }
}
