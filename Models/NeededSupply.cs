using System.ComponentModel.DataAnnotations;
using Outdoors.ly.Models;

namespace Outdoors.ly.Models
{
    public class NeededSupply
    {
        public int Id { get; set; }

        [Required]
        public string ItemName { get; set; }

        public int Quantity { get; set; }

        // Link to existing supply (optional)
        public int? ExistingSupplyId { get; set; }
        public Supply? ExistingSupply { get; set; }

        // The activity that needs this item
        public int ActivityId { get; set; }
        public Activity Activity { get; set; }

        // Who is bringing it (optional)
        public int? AssignedUserId { get; set; }
        public User? AssignedUser { get; set; }

        // Whether it's already fulfilled
        public bool IsFulfilled => ExistingSupplyId != null;
    }
}
