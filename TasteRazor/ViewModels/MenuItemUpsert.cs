using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using TasteRazor.Models;

namespace TasteRazor.ViewModels
{
    public class MenuItemUpsert
    {
        public MenuItem MenuItem { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public IEnumerable<SelectListItem> FoodTypeList { get; set; }
    }
}
