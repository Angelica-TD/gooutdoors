using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Outdoors.ly.Models
{
    public class ActivityCreateViewModel
    {
        [Required(ErrorMessage = "This is required")]
        [Display(Name = "What")]
        public string Name { get; set; }

        [Required(ErrorMessage = "This is required")]
        [Display(Name = "Where")]
        public string Venue { get; set; }

        [Required(ErrorMessage = "This is required")]
        [Display(Name = "When")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "This is required")]
        [Display(Name = "What Time")]
        [DataType(DataType.Time)]
        public TimeSpan StartTime { get; set; }

        [Display(Name = "Details")]
        [DataType(DataType.MultilineText)]
        public string? Details { get; set; }

        // Select lists for supplies and invitees
        [Display(Name = "Needed Supplies")]
        public List<int> SelectedSupplyIds { get; set; } = new();

        [Display(Name = "Invitees")]
        public List<int> SelectedUserIds { get; set; } = new();

        // Used to populate the multi-selects
        public List<SelectListItem> Supplies { get; set; } = new();
        public List<SelectListItem> Users { get; set; } = new();
    }
}
