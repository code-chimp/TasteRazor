using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TasteRazor.DataAccess.Repository.Contracts;

namespace TasteRazor.Pages.Admin.Category
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _service;

        public UpsertModel(IUnitOfWork service)
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
