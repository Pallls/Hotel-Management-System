using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagementSystem.Models
{

    public enum RoomCategory
    {
        Single = 1,
        Double = 2,
        Suite = 3
    }

    public enum BookingStatus
    {
        Pending = 1,
        Confirmed = 2,
        CheckedIn = 3,
        CheckedOut = 4,
        Cancelled = 5
    }
    public class Booking
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int RoomId { get; set; }

        public DateTime CheckinDate { get; set; }

        public DateTime CheckoutDate { get; set; }

        [Range(0.01, 10000, ErrorMessage = "Price must be greater than zero.")]
        public decimal TotalPrice { get; set; }

        public BookingStatus Status { get; set; } = BookingStatus.Pending;

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BookingDate { get; set; } = DateTime.Now;

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; }
    }
}