using System.Linq;
using TasteRazor.DataAccess.Repository.Contracts;
using TasteRazor.Models;

namespace TasteRazor.DataAccess.Repository
{
    public class MenuItemRepository : Repository<MenuItem>, IMenuItemRepository
    {
        private readonly ApplicationDbContext _context;

        public MenuItemRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(MenuItem entity)
        {
            var menuItem = _context.MenuItem.FirstOrDefault(_ => _.Id == entity.Id);

            if (menuItem != null)
            {
                menuItem.Name = entity.Name;
                menuItem.Description = entity.Description;
                menuItem.Price = entity.Price;
                menuItem.CategoryId = entity.CategoryId;
                menuItem.FoodTypeId = entity.FoodTypeId;

                if (entity.Image != null)
                {
                    menuItem.Image = entity.Image;
                }

                _context.SaveChanges();
            }
        }
    }
}
