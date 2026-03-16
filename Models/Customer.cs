using System.ComponentModel.DataAnnotations;

namespace HotelManagementSystem.Models
{
 public class Customer
 {
    public int Id {get; set; }

    [Required(ErrorMessage = "Name is Required.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters.")]
    public string Name {get; set; }

    [Required(ErrorMessage = "Email is Required")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string Email {get; set; }

    [Required(ErrorMessage = "Phone number is Required.")]
    [StringLength(20)]
    [Phone]
    public string PhoneNumber {get; set; }

    [StringLength(500)]
    public string Address {get; set; }

    public DateTime RegisteredDate {get; set; } = DateTime.Now;

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>(); 
 }
}