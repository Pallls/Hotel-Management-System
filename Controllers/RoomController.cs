using HotelManagementSystem.Data;
using HotelManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSytemControllers
{
    public class RoomController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RoomController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var today = DateTime.Today;

            var rooms = _context.Rooms
                .Include(r => r.Bookings)
                .ToList();

            return View(rooms);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]   
        public IActionResult Create(Room room)
        {

            bool exists = _context.Rooms.Any(r => r.RoomNumber == room.RoomNumber);

            if(exists)
            {
                ModelState.AddModelError("RoomNumber", "Room number already exists. Please choose a different number.");
            }

            if(ModelState.IsValid)
            {
                _context.Rooms.Add(room);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(room);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            return NotFound();

            var room = _context.Rooms.Find(id);
            if (room == null)
                return NotFound();

            return View(room);
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Room room)
        {
            if (id != room.Id)
                return NotFound();

            if(ModelState.IsValid)
            {
                try
                {
                    _context.Update(room);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Unable to save changes. ");    
                }
                return RedirectToAction(nameof(Index));

            }
            return View(room);
        }


        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

                var room = _context.Rooms.Find(id);
                if(room == null)
                    return NotFound();

                return View(room);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var room = _context.Rooms.Find(id);
            if (room != null)
            {
                _context.Rooms.Remove(room);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}