using Outdoors.ly.Models;

namespace Outdoors.ly.Models
{
    public class ActivityUser
    {
        public int ActivityId { get; set; }
        public Activity Activity { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public AttendanceStatus Status { get; set; } = AttendanceStatus.Invited;
    }

    public enum AttendanceStatus
    {
        Invited,
        Confirmed,
        Declined
    }
}
