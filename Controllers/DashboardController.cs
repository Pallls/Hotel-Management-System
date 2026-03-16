using HotelManagementSystem.Data;
using HotelManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

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
            var dashboardData = new Dictionary<string, object>
            {
                { "TotalRooms", _context.Rooms.Count() },
                { "AvailableRooms", _context.Rooms.Where(r => r.IsAvailable).Count() },
                { "TotalCustomers", _context.Customers.Count() },
                { "TotalBookings", _context.Bookings.Count() },
                { "ConfirmedBookings", _context.Bookings.Where(b => b.Status == BookingStatus.Confirmed).Count() },
                { "PendingBookings", _context.Bookings.Where(b => b.Status == BookingStatus.Pending).Count() },
                { "TotalRevenue", _context.Bookings.Where(b => b.Status == BookingStatus.CheckedOut || b.Status == BookingStatus.Confirmed).Sum(b => b.TotalPrice) }
                
            };
            return View(dashboardData);

        }
    }
}
