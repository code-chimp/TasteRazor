using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TasteRazor.Models
{
    public class ShoppingCart
    {
        [Key]
        public int Id { get; set; }

        public int MenuItemId { get; set; }
        [ForeignKey("MenuItemId"), NotMapped]
        public virtual MenuItem MenuItem { get; set; }

        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId"), NotMapped]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Range(1, 100, ErrorMessage = "Please select at least one item")]
        public int Count { get; set; } = 1;
    }
}
