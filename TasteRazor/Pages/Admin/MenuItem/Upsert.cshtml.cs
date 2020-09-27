using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TasteRazor.DataAccess.Repository.Contracts;
using TasteRazor.ViewModels;

namespace TasteRazor.Pages.Admin.MenuItem
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _service;
        private readonly IWebHostEnvironment _env;

        [BindProperty]
        public MenuItemUpsert ViewModel { get; set; }

        public UpsertModel(IUnitOfWork service, IWebHostEnvironment env)
        {
            _service = service;
            _env = env;
        }

        public IActionResult OnGet(int? id)
        {
            ViewModel = new MenuItemUpsert()
            {
                MenuItem = id != null ?
                    _service.MenuItem.GetFirstOrDefault(_ => _.Id == id) :
                    new Models.MenuItem(),
                CategoryList = _service.Category.GetSelectListItems(),
                FoodTypeList = _service.FoodType.GetSelectListItems()
            };

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var webRoot = _env.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            if (ViewModel.MenuItem.Id == 0)
            {
                var uploadPath = Path.Combine(webRoot, "images", "menuItems");
                var extension = Path.GetExtension(files[0].FileName);
                var filename = $"{Guid.NewGuid().ToString()}{extension}";

                using (var fileStream = new FileStream(Path.Combine(uploadPath, filename), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }

                ViewModel.MenuItem.Image = filename;

                _service.MenuItem.Add(ViewModel.MenuItem);
            }
            else
            {
                var menuItem = _service.MenuItem.Get(ViewModel.MenuItem.Id);

                if (files.Count > 0)
                {
                    var uploadPath = Path.Combine(webRoot, "images", "menuItems");
                    var extension = Path.GetExtension(files[0].FileName);
                    var filename = $"{Guid.NewGuid().ToString()}{extension}";

                    var imagePath = Path.Combine(webRoot, menuItem.Image.TrimStart('\\'));
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }

                    using (var fileStream = new FileStream(Path.Combine(uploadPath, filename), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    ViewModel.MenuItem.Image = filename;
                    _service.MenuItem.Add(ViewModel.MenuItem);
                }
                else
                {
                    ViewModel.MenuItem.Image = menuItem.Image;
                }

                _service.MenuItem.Update(ViewModel.MenuItem);
            }

            _service.Save();
            return RedirectToPage("./Index");
        }
    }
}
