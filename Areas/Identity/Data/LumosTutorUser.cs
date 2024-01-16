using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Final.Models;

namespace Final.Areas.Identity.Data;

// Add profile data for application users by adding properties to the LumosTutorUser class
public class LumosTutorUser : IdentityUser
{
    public string Name { get; set; }
    public DateTime DOB { get; set; }
    public string Address {get;set;}
    public ICollection<Booking>? Bookings { get; set; }
}

