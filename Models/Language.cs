namespace Final.Models;
using System.ComponentModel.DataAnnotations;

public class Language {
    public int Id { get; set; }
    [Required(ErrorMessage = "Please type name of language!")]
    public string? Name { get; set; }
     public int Status { get; set; }
    public ICollection<Tutor>? Tutors { get; set; }
}