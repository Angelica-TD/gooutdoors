using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Outdoors.ly.Models
{
    public class Activity
    {
        [Key]
        public int Id { get; set; }

        // Date range (start and end)
        [Required(ErrorMessage = "This is required")]
        [Display(Name = "When")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        // Start time
        [Required(ErrorMessage = "This is required")]
        [Display(Name = "What Time")]
        [DataType(DataType.Time)]
        public TimeSpan StartTime { get; set; }

        // Venue
        [Required(ErrorMessage = "This is required")]
        [StringLength(100)]
        [Display(Name = "Where")]
        public string Venue { get; set; }

        // Activity name
        [Required(ErrorMessage = "This is required")]
        [StringLength(100)]
        [Display(Name = "What")]
        public string Name { get; set; }

        // Optional details
        [Display(Name = "Details")]
        [DataType(DataType.MultilineText)]
        public string? Details { get; set; }

        // Items needed for this activity
        public ICollection<NeededSupply> NeededSupplies { get; set; } = new List<NeededSupply>();

        // People linked to the activity (invitees, attendees)
        public ICollection<ActivityUser> ActivityUsers { get; set; } = new List<ActivityUser>();

        // Days countdown — can be computed automatically
        [NotMapped]
        [Display(Name = "Days Countdown")]
        public int DaysCountdown => (StartDate.Date - DateTime.Now.Date).Days < 0 ? 0 : (StartDate.Date - DateTime.Now.Date).Days;

        [NotMapped]
        [Display(Name = "Days Past")]
        public int DaysPast => StartDate < DateTime.Now
                ? (int)(DateTime.Now - StartDate).TotalDays
                : 0;
    }
}
