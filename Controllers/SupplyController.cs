using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Outdoors.ly.Data;
using Outdoors.ly.Models;

namespace Outdoors.ly.Controllers
{
    [Route("supplies")]
    public class SupplyController : Controller
    {
		private readonly ApplicationDbContext _db;
		public SupplyController(ApplicationDbContext db)
		{
			_db = db;
		}
        // GET: SupplyController
        [HttpGet("")]
        //[HttpGet("index")]
		public IActionResult Index()
		{
			var suppliesList = _db.Supplies
				.Include(s => s.User) // <-- This loads each supply’s associated User
				.ToList();

			return View(suppliesList);
		}

        // GET: SupplyController/Details/5
        [HttpGet("{id}")]
        public IActionResult Details(int id)
        {
            var supply = _db.Supplies.FirstOrDefault(s => s.Id == id);
            if (supply == null)
                return NotFound();

            return View(supply);
        }

        // GET: SupplyController/Create
        [HttpGet("create")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: SupplyController/Create
        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SupplyController/Edit/5

        //public ActionResult Edit(int id)
        //{
        //    var supply = _db.Supplies.FirstOrDefault(s => s.Id == id);
        //    if (supply == null)
        //        return NotFound();

        //    return View(supply);
        //}

        // POST: SupplyController/Edit/5
        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, SupplyEditViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm); // redisplay form with validation messages

            var supply = _db.Supplies.FirstOrDefault(s => s.Id == id);
            if (supply == null)
                return NotFound();

            supply.ItemName = vm.Name;
            supply.Quantity = vm.Quantity;

            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // GET: SupplyController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SupplyController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
