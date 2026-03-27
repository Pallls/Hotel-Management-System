using HotelManagementSystem.Data;
using HotelManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementSystem.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            var today = DateTime.Today;

            var rooms = _context.Rooms.Include(r => r.Bookings).ToList();
            var dashboardData = new Dictionary<string, object>
            {
                { "TotalRooms", rooms.Count() },
                { "AvailableRooms", rooms.Where(r => !r.IsOccupiedToday).Count() },
                { "OccupiedRooms", rooms.Where(r => r.IsOccupiedToday).Count() },
                { "TotalCustomers", _context.Customers.Count() },
                { "TotalBookings", _context.Bookings.Count() },
                { "ConfirmedBookings", _context.Bookings.Where(b => b.Status == BookingStatus.Confirmed).Count() },
                { "PendingBookings", _context.Bookings.Where(b => b.Status == BookingStatus.Pending).Count() },
                { "CheckedInBookings", _context.Bookings.Where(b => b.Status == BookingStatus.CheckedIn).Count() },
                { "CheckedOutBookings", _context.Bookings.Where(b => b.Status == BookingStatus.CheckedOut).Count() },
                { "CancelledBookings", _context.Bookings.Where(b => b.Status == BookingStatus.Cancelled).Count() },
                { "TotalRevenue", _context.Bookings.Where(b => b.Status == BookingStatus.CheckedOut || b.Status == BookingStatus.Confirmed || b.Status == BookingStatus.CheckedIn).Sum(b => b.TotalPrice) }
                
            };
            return View(dashboardData);

        }

    }
}
