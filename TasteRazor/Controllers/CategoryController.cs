using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TasteRazor.DataAccess.Repository.Contracts;

namespace TasteRazor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _service;

        public CategoryController(IUnitOfWork service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categories = await _service.Category.GetAllAsync();
            return Json(new {data = categories});
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var entity = _service.Category.GetFirstOrDefault(_ => _.Id == id);

            if (entity != null)
            {
                _service.Category.Remove(entity);
                _service.Save();

                return Json(new {success = true, message = "Category Deleted."});
            }

            return Json(new {success = false, message = "Category not found"});
        }
    }
}
