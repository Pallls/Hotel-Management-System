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
            var currentYear = today.Year;
            
            var rooms = _context.Rooms.Include(r => r.Bookings).ToList();


            var monthlyData = _context.Bookings
                .Where(b => b.BookingDate.Year == currentYear)
                .GroupBy(b => b.BookingDate.Month)
                .Select(g => new { Month = g.Key, Total = g.Sum(x => x.TotalPrice) })
                .ToList();

            var labels = new string[12];
            var counts = new int[12];

            for (int i = 1; i <= 12; i++)
                {
                    labels[i - 1] = new DateTime(currentYear, i, 1).ToString("MMM");
                    
                    // Use (int) to cast the decimal Total to an integer
                    counts[i - 1] = (int)(monthlyData.FirstOrDefault(m => m.Month == i)?.Total ?? 0m);
                } 

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
                { "TotalRevenue", _context.Bookings.Where(b => b.Status == BookingStatus.CheckedOut || b.Status == BookingStatus.Confirmed || b.Status == BookingStatus.CheckedIn).Sum(b => b.TotalPrice) },
                { "ChartLabels", labels },
                { "ChartData", counts }
            };
            return View(dashboardData);

        }

    }
}
