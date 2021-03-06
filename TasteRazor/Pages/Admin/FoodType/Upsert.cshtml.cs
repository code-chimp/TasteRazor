using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TasteRazor.DataAccess.Repository.Contracts;
using TasteRazor.Utility;

namespace TasteRazor.Pages.Admin.FoodType
{
    [Authorize(Roles = Constants.ManagerRole)]
    public class Upsert : PageModel
    {
        private readonly IUnitOfWork _service;

        public Upsert(IUnitOfWork service)
        {
            _service = service;
        }

        public IActionResult OnGet(int? id)
        {
            FoodTypeObj = id != null ?
                _service.FoodType.GetFirstOrDefault(_ => _.Id == id) :
                new Models.FoodType();

            return Page();
        }

        [BindProperty]
        public Models.FoodType FoodTypeObj { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (FoodTypeObj.Id == 0)
            {
                _service.FoodType.Add(FoodTypeObj);
            }
            else
            {
                _service.FoodType.Update(FoodTypeObj);
            }

            _service.Save();
            return RedirectToPage("./Index");
        }
    }
}
