using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TasteRazor.DataAccess.Repository.Contracts;
using TasteRazor.Models;

namespace TasteRazor.Pages.Customer.Home
{
    [Authorize]
    public class Details : PageModel
    {
        private readonly IUnitOfWork _service;

        public Details(IUnitOfWork service)
        {
            _service = service;
        }

        [BindProperty]
        public ShoppingCart ViewModel { get; set; }

        public void OnGet(int id)
        {
            ViewModel = new ShoppingCart
            {
                MenuItemId = id,
                MenuItem = _service.MenuItem.GetFirstOrDefault(_ => _.Id == id, includeProperties: "Category,FoodType")
            };
        }
    }
}
