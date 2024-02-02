namespace Final.Models;
using System.ComponentModel.DataAnnotations;
public class Tutor
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Please provide a name.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Please provide a description.")]
    public string Description { get; set; }

    [Range(1000, 30000000, ErrorMessage = "Price must be over 1000")]
    [Required(ErrorMessage = "Please provide a price.")]
    public double Price { get; set; }

    [Required(ErrorMessage = "Please provide an image URL.")]
    [DataType(DataType.ImageUrl)]
    public string? Image { get; set; }

    [DataType(DataType.Url)]
    public string? Video { get; set; }

    // [Required(ErrorMessage = "Please provide a schedule.")]
    // public required string Schedule { get; set; }

    public int LanguageID { get; set; }
    public Language? Language { get; set; }
}