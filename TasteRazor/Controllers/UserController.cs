using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TasteRazor.DataAccess.Repository.Contracts;

namespace TasteRazor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _service;

        public UserController(IUnitOfWork service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _service.ApplicationUser.GetAllAsync();
            return Json(new {data = users});
        }

        [HttpPost]
        public IActionResult ToggleLock([FromBody]string id)
        {
            var entity = _service.ApplicationUser.GetFirstOrDefault(_ => _.Id == id, enableTracking: true);

            if (entity != null)
            {
                if (entity.LockoutEnd != null && entity.LockoutEnd > DateTime.Now)
                {
                    entity.LockoutEnd = DateTime.Now;
                }
                else
                {
                    entity.LockoutEnd = DateTime.Now.AddHours(3);
                }
                _service.Save();

                return Json(new {success = true, message = "User updated."});
            }

            return Json(new {success = false, message = "Error (un)locking user."});
        }
    }
}
