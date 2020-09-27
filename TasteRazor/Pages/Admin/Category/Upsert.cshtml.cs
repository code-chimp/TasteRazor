using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TasteRazor.DataAccess.Repository.Contracts;
using TasteRazor.Utility;

namespace TasteRazor.Pages.Admin.Category
{
    [Authorize(Roles = Constants.ManagerRole)]
    public class Upsert : PageModel
    {
        private readonly IUnitOfWork _service;

        public Upsert(IUnitOfWork service)
        {
            _service = service;
        }

        [BindProperty]
        public Models.Category CategoryObj { get; set; }

        public IActionResult OnGet(int? id)
        {
            CategoryObj = new Models.Category();

            if (id != null)
            {
                CategoryObj = _service.Category.GetFirstOrDefault(_ => _.Id == id);

                if (CategoryObj == null)
                {
                    return NotFound();
                }
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (CategoryObj.Id == 0)
            {
                _service.Category.Add(CategoryObj);
            }
            else
            {
                _service.Category.Update(CategoryObj);
            }

            _service.Save();
            return RedirectToPage("./Index");
        }
    }
}
