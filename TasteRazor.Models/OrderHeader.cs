using System.ComponentModel.DataAnnotations;

namespace TasteRazor.Models
{
    public class OrderHeader
    {
        [Key]
        public int Id { get; set; }
    }
}
