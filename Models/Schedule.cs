using System.ComponentModel.DataAnnotations;
using Final.Models;
public class Schedule
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Please provide a day of the week.")]
    public DayOfWeek DayOfWeek { get; set; }

    [Required(ErrorMessage = "Please provide a start time.")]
    public TimeSpan StartTime { get; set; }

    [Required(ErrorMessage = "Please provide an end time.")]
    public TimeSpan EndTime { get; set; }

    // Assuming you have a reference to the Tutor entity
    public int TutorId { get; set; }
    public Tutor? Tutor { get; set; }
}
