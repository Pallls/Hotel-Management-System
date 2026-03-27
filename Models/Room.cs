using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Models
{
    public class Room
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Room number is required.")]
        [StringLength(10)]
        public string RoomNumber {get; set; }

        [Required(ErrorMessage = "Room type is required.")]
        public RoomCategory RoomType {get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, 10000, ErrorMessage = "Price must be greater than zero.")]
        public decimal PricePerNight {get; set; }

        [Required]
        public bool IsAvailable {get; set; } = true;

        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

        public bool IsOccupiedToday => Bookings.Any(b => 
            b.CheckinDate <= DateTime.Today && b.CheckoutDate >= DateTime.Today && 
            (b.Status == BookingStatus.Confirmed || b.Status == BookingStatus.CheckedIn));
        
    }
}