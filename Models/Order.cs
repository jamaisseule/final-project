using System.ComponentModel.DataAnnotations;
using LUMOSBook.Areas.Identity.Data;
namespace LUMOSBook.Models;
public class Order
{
        public long Id { get; set; }       
        [DataType(DataType.Date)]
        public DateTime OrderTime { get; set; }
        public decimal? Total { get; set; }
        public string? State{ get; set; }
        public string? UserID { get; set; }
        public string? Fullname { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public ICollection<OrderItem>? OrderItem { get; set; }
        public BookUser? BookUser { get; set; }
}
