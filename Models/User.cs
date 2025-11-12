using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Outdoors.ly.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        // Supplies they currently own
        public ICollection<Supply> Supplies { get; set; } = new List<Supply>();

        // Activities they're part of
        public ICollection<ActivityUser> ActivityUsers { get; set; } = new List<ActivityUser>();

        public NotificationSetting NotificationSetting { get; set; }
    }
}
