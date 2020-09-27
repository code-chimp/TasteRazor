using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using TasteRazor.DataAccess.Repository.Contracts;

namespace TasteRazor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController : Controller
    {
        private readonly IUnitOfWork _service;
        private readonly IWebHostEnvironment _env;

        public MenuItemController(IUnitOfWork service, IWebHostEnvironment env)
        {
            _service = service;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var items = await _service.MenuItem.GetAllAsync(includeProperties: "Category,FoodType");
            return Json(new {data = items});
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var entity = _service.MenuItem.GetFirstOrDefault(_ => _.Id == id);

                if (entity == null)
                {
                    return Json(new {success = false, message = "MenuItem not found."});
                }

                var imagePath = Path.Combine(_env.WebRootPath, "images", "menuItems", entity.Image);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }

                _service.MenuItem.Remove(entity);
                _service.Save();

                return Json(new {success = true, message = "MenuItem Deleted"});
            }
            catch (Exception ex)
            {
                return Json(new {success = false, message = $"Error deleting MenuItem: {ex.Message}"});
            }
        }
    }
}
