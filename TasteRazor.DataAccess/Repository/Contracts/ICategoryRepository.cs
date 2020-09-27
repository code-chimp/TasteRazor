using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using TasteRazor.Models;

namespace TasteRazor.DataAccess.Repository.Contracts
{
    public interface ICategoryRepository : IRepository<Category>
    {
        IEnumerable<SelectListItem> GetSelectListItems();

        void Update(Category category);
    }
}
