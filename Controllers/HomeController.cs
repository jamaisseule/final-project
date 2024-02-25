using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Final.Models;
using Final.Controllers;
using Microsoft.Extensions.Caching.Memory;
using Final.Areas.Identity.Data;

namespace Final.Controllers{

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly LumosTutorIdentityDbContext _context;

    public HomeController(ILogger<HomeController> logger, LumosTutorIdentityDbContext context)
    {
        _logger = logger;
        _context = context;
    }


        public async Task<IActionResult> Index()
        {
            // Example: Retrieve data from the database
            // var data = await _context.Tutor.ToListAsync();

            // return View(data);
            return View();
        
        }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}
}