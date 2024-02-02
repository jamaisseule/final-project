using System.ComponentModel.DataAnnotations;
using Final.Areas.Identity.Data;

namespace Final.Models;
public class Booking
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Please provide a booking date and time.")]
    public DateTime BookingDateTime { get; set; }

    [Required(ErrorMessage = "Please provide a duration for the booking.")]
    public int DurationInMinutes { get; set; }

    // Assuming you have a reference to the Tutor and Student entities
    public int TutorId { get; set; }
    public Tutor? Tutor { get; set; }

    public int StudentId { get; set; }
    public Student? Student { get; set; }
}