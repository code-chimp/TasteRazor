using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using TasteRazor.DataAccess.Repository.Contracts;
using TasteRazor.Models;

namespace TasteRazor.DataAccess.Repository
{
    public class FoodTypeRepository : Repository<FoodType>, IFoodTypeRepository
    {
        private readonly ApplicationDbContext _context;
        public FoodTypeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetSelectListItems()
        {
            return _context.FoodType
                .Select(foodType => new SelectListItem
                {
                    Text = foodType.Name,
                    Value = foodType.Id.ToString()
                });
        }

        public void Update(FoodType foodType)
        {
            var existing = _context.FoodType.FirstOrDefault(_ => _.Id == foodType.Id);

            if (existing != null)
            {
                existing.Name = foodType.Name;
                _context.SaveChanges();
            }
        }
    }
}
