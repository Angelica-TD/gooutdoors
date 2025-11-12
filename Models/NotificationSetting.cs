using Outdoors.ly.Models;

namespace Outdoors.ly.Models
{
    public class NotificationSetting
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public bool NotifyWhenDesignated { get; set; } = true;
        public bool NotifyWhenSupplyAdded { get; set; } = true;
    }
}
