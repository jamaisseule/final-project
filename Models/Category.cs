namespace LUMOSBook.Models;
using System.ComponentModel.DataAnnotations;
public class Category
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public ICollection<Book>? Books { get; set; }
    
}