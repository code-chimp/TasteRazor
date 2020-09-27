using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using TasteRazor.DataAccess.Repository.Contracts;
using TasteRazor.Models;

namespace TasteRazor.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetSelectListItems()
        {
            return _context.Category
                .Select(category => new SelectListItem
                {
                    Text = category.Name,
                    Value = category.Id.ToString()
                });
        }

        public void Update(Category data)
        {
            var category = _context.Category.FirstOrDefault(c => c.Id == data.Id);

            if (category != null)
            {
                category.Name = data.Name;
                category.DisplayOrder = data.DisplayOrder;

                _context.SaveChanges();
            }
        }
    }
}
