using System.ComponentModel.DataAnnotations;
using Outdoors.ly.Models;

namespace Outdoors.ly.Models
{
    public class Supply
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string ItemName { get; set; }

        public int Quantity { get; set; } = 1;

        // Who owns it
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
