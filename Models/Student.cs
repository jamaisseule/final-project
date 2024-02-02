using System.ComponentModel.DataAnnotations;
using Final.Models;

public class Student
{
    private const string V = "Invalid email address.";

    public int Id { get; set; }

    [Required(ErrorMessage = "Please provide a name.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Please provide an email address.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string? Email { get; set; }

    // Additional properties based on your requirements
    // Examples include hobbies, preferences, past tutors, etc.
    
    // Assuming you have a collection of bookings made by the student
    public required ICollection<Booking> Bookings { get; set; }
}