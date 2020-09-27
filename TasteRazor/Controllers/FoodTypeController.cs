using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TasteRazor.DataAccess.Repository.Contracts;

namespace TasteRazor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodTypeController : Controller
    {
        private readonly IUnitOfWork _service;

        public FoodTypeController(IUnitOfWork service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var types = await _service.FoodType.GetAllAsync();
            return Json(new {data = types});
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var entity = _service.FoodType.GetFirstOrDefault(_ => _.Id == id);

            if (entity != null)
            {
                _service.FoodType.Remove(entity);
                _service.Save();

                return Json(new {success = true, message = "FoodType Deleted"});
            }

            return Json(new {success = false, message = "FoodType not found."});
        }
    }
}
