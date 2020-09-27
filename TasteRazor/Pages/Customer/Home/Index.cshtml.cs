using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TasteRazor.DataAccess.Repository.Contracts;
using TasteRazor.Models;

namespace TasteRazor.Pages.Customer.Home
{
    public class Index : PageModel
    {
        private readonly IUnitOfWork _service;

        public Index(IUnitOfWork service)
        {
            _service = service;
        }

        public IEnumerable<MenuItem> MenuItems { get; set; }
        public IEnumerable<Category> Categories { get; set; }

        public void OnGet()
        {
            MenuItems = _service.MenuItem.GetAll(includeProperties:"Category,FoodType");
            Categories = _service.Category.GetAll(orderBy: _ => _.OrderBy(c => c.DisplayOrder));
        }
    }
}
