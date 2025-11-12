using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Outdoors.ly.Models
{
    public class SupplyEditViewModel
    {
        [Required(ErrorMessage = "This is required")]
        [Display(Name = "Item name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "This is required")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "This is required")]
        [Display(Name = "Item with")]
        public int userId { get; set; }
        public string userName { get; set; }
    }
}
