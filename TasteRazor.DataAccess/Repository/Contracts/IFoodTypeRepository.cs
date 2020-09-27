using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using TasteRazor.Models;

namespace TasteRazor.DataAccess.Repository.Contracts
{
    public interface IFoodTypeRepository : IRepository<FoodType>
    {
        IEnumerable<SelectListItem> GetSelectListItems();
        void Update(FoodType foodType);
    }
}
