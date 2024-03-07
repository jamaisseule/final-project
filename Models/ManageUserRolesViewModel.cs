using System.ComponentModel.DataAnnotations;
namespace LUMOSBook.Models;

public class ManageUserRolesViewModel
{
    public string RoleId { get; set; }
    public string RoleName { get; set; }
    public bool Selected { get; set; }
}