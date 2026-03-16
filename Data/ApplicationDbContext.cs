using Microsoft.EntityFrameworkCore;
using HotelManagementSystem.Models;

namespace HotelManagementSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Customer)
                .WithMany(c => c.Bookings)
                .HasForeignKey(b => b.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Room)
                .WithMany(r => r.Bookings)
                .HasForeignKey(b => b.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

            
            modelBuilder.Entity<Booking>()
                .Property(p => p.TotalPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Room>()
                .Property(p => p.PricePerNight)
                .HasColumnType("decimal(18,2)");

            // =============== SAMPLE DATA ===============
            // Add sample rooms
            modelBuilder.Entity<Room>().HasData(
                new Room { Id = 1, RoomNumber = "101", RoomType = RoomCategory.Single, PricePerNight = 50, IsAvailable = true },
                new Room { Id = 2, RoomNumber = "102", RoomType = RoomCategory.Double, PricePerNight = 80, IsAvailable = true },
                new Room { Id = 3, RoomNumber = "103", RoomType = RoomCategory.Suite, PricePerNight = 150, IsAvailable = false },
                new Room { Id = 4, RoomNumber = "201", RoomType = RoomCategory.Single, PricePerNight = 50, IsAvailable = true },
                new Room { Id = 5, RoomNumber = "202", RoomType = RoomCategory.Double, PricePerNight = 80, IsAvailable = false }
            );

            // Add sample customers
            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, Name = "John Doe", Email = "john@example.com", PhoneNumber = "1234567890", Address = "123 Main St, City" },
                new Customer { Id = 2, Name = "Jane Smith", Email = "jane@example.com", PhoneNumber = "9876543210", Address = "456 Oak Ave, Town" },
                new Customer { Id = 3, Name = "Mike Johnson", Email = "mike@example.com", PhoneNumber = "5555555555", Address = "789 Pine Rd, Village" },
                new Customer { Id = 4, Name = "Ginting Michael", Email = "ginting@example.com", PhoneNumber = "5551234567", Address = "321 Elm St, City" }
            );

            // Add sample bookings
            modelBuilder.Entity<Booking>().HasData(
                new Booking 
                { 
                    Id = 1, 
                    CustomerId = 1, 
                    RoomId = 1, 
                    CheckinDate = new DateTime(2026, 3, 15, 14, 0, 0), 
                    CheckoutDate = new DateTime(2026, 3, 17, 11, 0, 0), 
                    TotalPrice = 100, 
                    Status = BookingStatus.Confirmed,
                    BookingDate = new DateTime(2026, 3, 13, 10, 0, 0)
                },
                new Booking 
                { 
                    Id = 2, 
                    CustomerId = 2, 
                    RoomId = 3, 
                    CheckinDate = new DateTime(2026, 3, 18, 14, 0, 0), 
                    CheckoutDate = new DateTime(2026, 3, 20, 11, 0, 0), 
                    TotalPrice = 300, 
                    Status = BookingStatus.Pending,
                    BookingDate = new DateTime(2026, 3, 13, 10, 0, 0)
                },
                new Booking 
                { 
                    Id = 3, 
                    CustomerId = 3, 
                    RoomId = 3, 
                    CheckinDate = new DateTime(2026, 3, 18, 14, 0, 0), 
                    CheckoutDate = new DateTime(2026, 3, 20, 11, 0, 0), 
                    TotalPrice = 500, 
                    Status = BookingStatus.Pending,
                    BookingDate = new DateTime(2026, 3, 13, 10, 0, 0)
                }
            );
        }
    }
}