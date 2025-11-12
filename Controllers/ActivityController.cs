using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Outdoors.ly.Data;
using Outdoors.ly.Models;

namespace Outdoors.ly.Controllers
{
    [Route("activities")]
    public class ActivityController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ActivityController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET /activities
        [HttpGet("")]
        //[HttpGet("index")]
        public IActionResult Index()
        {
            var TodayDate = DateTime.Now.Date;

            List<Activity> upcoming = _db.Activities
                .Where(a => a.StartDate.Date > TodayDate)
                .OrderBy(a => a.StartDate)
                .Take(3)
                .ToList();

            List<Activity> past = _db.Activities
                .Where(a => a.StartDate.Date < TodayDate)
                //.OrderBy(a => a.StartDate)
                .OrderByDescending(a => a.StartDate)
                .Take(3)
                .ToList();

            List<Activity> today = _db.Activities
                .Where(a => a.StartDate.Date == TodayDate)
                .Take(3)
                .ToList();

            var viewModel = new ActivityViewModel
            {
                UpcomingActivities = upcoming,
                PastActivities = past,
                ActivitiesToday = today
            };

            return View(viewModel);
        }

        // /activities/{id}
        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
            var activity = _db.Activities.FirstOrDefault(a => a.Id == id);
            if (activity == null)
                return NotFound();

            return View(activity);
        }

        // GET /activities/create
        [HttpGet("create")]
        public IActionResult Create()
        {
            var vm = new ActivityCreateViewModel
            {
                Supplies = _db.Supplies
                    .Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.ItemName })
                    .ToList(),
                Users = _db.Users
                    .Select(u => new SelectListItem { Value = u.Id.ToString(), Text = u.Name })
                    .ToList()
            };

            return View(vm);
        }

        // POST /activities/create
        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ActivityCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var activity = new Activity
                {
                    Name = vm.Name,
                    Venue = vm.Venue,
                    StartDate = vm.StartDate,
                    StartTime = vm.StartTime,
                    Details = vm.Details
                };

                // Handle supplies
                foreach (var supplyID in vm.SelectedSupplyIds)
                {
                    var supply = _db.Supplies.FirstOrDefault(s => s.Id == supplyID);
                    if (supply == null)
                    {
                        supply = new Supply { Id = supplyID };
                        _db.Supplies.Add(supply);
                        _db.SaveChanges();
                    }

                    activity.NeededSupplies.Add(new NeededSupply { Id = supply.Id });
                }

                // Handle invitees
                foreach (var userId in vm.SelectedUserIds)
                {
                    var user = _db.Users.FirstOrDefault(u => u.Id == userId);
                    if (user == null)
                        continue; // Skip missing users

                    activity.ActivityUsers.Add(new ActivityUser { UserId = user.Id });
                }

                _db.Activities.Add(activity);
                _db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            // If model invalid, reload select lists
            vm.Supplies = _db.Supplies
                .Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.ItemName })
                .ToList();
            vm.Users = _db.Users
                .Select(u => new SelectListItem { Value = u.Id.ToString(), Text = u.Name })
                .ToList();

            return View(vm);

        }
    }
}
