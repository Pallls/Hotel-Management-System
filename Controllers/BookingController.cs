using Microsoft.AspNetCore.Mvc;
using HotelManagementSystem.Data;
using HotelManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HotelManagementSystem.Controllers
{
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        public BookingController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var bookings = _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Room)
                .OrderByDescending(b => b.BookingDate)
                .ToList();
            return View(bookings);
        }

        public IActionResult Create()
        {
            ViewBag.Customers = _context.Customers.ToList();
            ViewBag.Rooms = _context.Rooms.ToList();
            return View();
        }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Create(string customerId, string roomId, string CheckinDate, string CheckoutDate, string status)
            {
                try
                {
                    // Parse and validate all inputs manually
                    if (!int.TryParse(customerId, out int customerIdInt) || customerIdInt <= 0)
                    {
                        ModelState.AddModelError("CustomerId", "The Customer field is required.");
                    }

                    if (!int.TryParse(roomId, out int roomIdInt) || roomIdInt <= 0)
                    {
                        ModelState.AddModelError("RoomId", "The Room field is required.");
                    }

                    DateTime checkinDate = DateTime.MinValue;
                    if (!string.IsNullOrEmpty(CheckinDate))
                    {
                        if (!DateTime.TryParseExact(CheckinDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out checkinDate))
                        {
                            ModelState.AddModelError("CheckinDate", "Invalid check-in date format.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("CheckinDate", "Check-in date is required.");
                    }

                    DateTime checkoutDate = DateTime.MinValue;
                    if (!string.IsNullOrEmpty(CheckoutDate))
                    {
                        if (!DateTime.TryParseExact(CheckoutDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out checkoutDate))
                        {
                            ModelState.AddModelError("CheckoutDate", "Invalid check-out date format.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("CheckoutDate", "Check-out date is required.");
                    }

                    // Additional validation
                    if (ModelState.IsValid)
                    {
                        var customer = _context.Customers.Find(customerIdInt);
                        if (customer == null)
                        {
                            ModelState.AddModelError("CustomerId", "Selected customer does not exist.");
                        }

                        var room = _context.Rooms.Find(roomIdInt);
                        if (room == null)
                        {
                            ModelState.AddModelError("RoomId", "Selected room does not exist.");
                        }

                        if (checkinDate >= checkoutDate)
                        {
                            ModelState.AddModelError("CheckoutDate", "Checkout date must be after check-in date.");
                        }
                    }

                    // If all validation passes, save
                    if (ModelState.IsValid)
                    {
                        var room = _context.Rooms.Find(roomIdInt);
                        var nights = (checkoutDate - checkinDate).Days;
                        
                        var booking = new Booking
                        {
                            CustomerId = customerIdInt,
                            RoomId = roomIdInt,
                            CheckinDate = checkinDate,
                            CheckoutDate = checkoutDate,
                            TotalPrice = room.PricePerNight * nights,
                            Status = (BookingStatus)(int.Parse(status ?? "1")),
                            BookingDate = DateTime.Now
                        };

                        _context.Bookings.Add(booking);
                        _context.SaveChanges();
                        
                        return RedirectToAction("Index");
                    }

                    // If validation failed, reload form with data
                    ViewBag.Customers = _context.Customers.ToList();
                    ViewBag.Rooms = _context.Rooms.ToList();
                    return View();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error: {ex.Message}");
                    ViewBag.Customers = _context.Customers.ToList();
                    ViewBag.Rooms = _context.Rooms.ToList();
                    return View();
                }
            }

            [HttpGet]
                public IActionResult Edit(int id)
                {
                    var booking = _context.Bookings
                        .Include(b => b.Customer)
                        .Include(b => b.Room)
                        .FirstOrDefault(b => b.Id == id);
                    
                    if (booking == null)
                        return NotFound();

                    ViewBag.Customers = _context.Customers.ToList();
                    ViewBag.Rooms = _context.Rooms.ToList();
                    return View(booking);
                }
            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Edit(int id, string customerId, string roomId, string CheckinDate, string CheckoutDate, string status)
            {
                var existingBooking = _context.Bookings.Find(id);
                if (existingBooking == null)
                    return NotFound();

                try
                {
                    // Parse and validate all inputs manually
                    if (!int.TryParse(customerId, out int customerIdInt) || customerIdInt <= 0)
                    {
                        ModelState.AddModelError("CustomerId", "The Customer field is required.");
                    }

                    if (!int.TryParse(roomId, out int roomIdInt) || roomIdInt <= 0)
                    {
                        ModelState.AddModelError("RoomId", "The Room field is required.");
                    }

                    DateTime checkinDate = DateTime.MinValue;
                    if (!string.IsNullOrEmpty(CheckinDate))
                    {
                        if (!DateTime.TryParseExact(CheckinDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out checkinDate))
                        {
                            ModelState.AddModelError("CheckinDate", "Invalid check-in date format.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("CheckinDate", "Check-in date is required.");
                    }

                    DateTime checkoutDate = DateTime.MinValue;
                    if (!string.IsNullOrEmpty(CheckoutDate))
                    {
                        if (!DateTime.TryParseExact(CheckoutDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out checkoutDate))
                        {
                            ModelState.AddModelError("CheckoutDate", "Invalid check-out date format.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("CheckoutDate", "Check-out date is required.");
                    }

                    // Additional validation
                    if (ModelState.IsValid)
                    {
                        var customer = _context.Customers.Find(customerIdInt);
                        if (customer == null)
                        {
                            ModelState.AddModelError("CustomerId", "Selected customer does not exist.");
                        }

                        var room = _context.Rooms.Find(roomIdInt);
                        if (room == null)
                        {
                            ModelState.AddModelError("RoomId", "Selected room does not exist.");
                        }

                        if (checkinDate >= checkoutDate)
                        {
                            ModelState.AddModelError("CheckoutDate", "Checkout date must be after check-in date.");
                        }
                    }

                    // If all validation passes, save
                    if (ModelState.IsValid)
                    {
                        var room = _context.Rooms.Find(roomIdInt);
                        var nights = (checkoutDate - checkinDate).Days;
                        
                        existingBooking.CustomerId = customerIdInt;
                        existingBooking.RoomId = roomIdInt;
                        existingBooking.CheckinDate = checkinDate;
                        existingBooking.CheckoutDate = checkoutDate;
                        existingBooking.TotalPrice = room.PricePerNight * nights;
                        existingBooking.Status = (BookingStatus)(int.Parse(status ?? "1"));

                        _context.SaveChanges();
                        return RedirectToAction("Index");
                    }

                    // If validation failed, reload form with data
                    ViewBag.Customers = _context.Customers.ToList();
                    ViewBag.Rooms = _context.Rooms.ToList();
                    return View(existingBooking);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error: {ex.Message}");
                    ViewBag.Customers = _context.Customers.ToList();
                    ViewBag.Rooms = _context.Rooms.ToList();
                    return View(existingBooking);
                }
            }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var booking = _context.Bookings.Find(id);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult GetAvailableRooms(string checkinDate, string checkoutDate)
        {
            try
            {
                if (string.IsNullOrEmpty(checkinDate) || string.IsNullOrEmpty(checkoutDate))
                    return Json(new { rooms = _context.Rooms.ToList() });

                if (!DateTime.TryParseExact(checkinDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out var checkIn))
                    return Json(new { rooms = _context.Rooms.ToList() });

                if (!DateTime.TryParseExact(checkoutDate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out var checkOut))
                    return Json(new { rooms = _context.Rooms.ToList() });

                // Get rooms that DON'T have confirmed bookings overlapping these dates
                var bookedRoomIds = _context.Bookings
                    .Where(b => b.Status == BookingStatus.Confirmed &&
                                b.CheckinDate < checkOut &&
                                b.CheckoutDate > checkIn)
                    .Select(b => b.RoomId)
                    .ToList();

                var availableRooms = _context.Rooms
                    .Where(r => !bookedRoomIds.Contains(r.Id))
                    .ToList();

                return Json(new { rooms = availableRooms });
            }
            catch
            {
                return Json(new { rooms = _context.Rooms.ToList() });
            }
        }
    }    
}