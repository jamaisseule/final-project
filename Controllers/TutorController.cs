using System.Drawing;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Final.Models;
using Final.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;

namespace Final.Controllers
{
    [Authorize(Roles = "Tutor")]
    public class TutorController : Controller
    {
        private readonly LumosTutorIdentityDbContext _context;

        public TutorController(LumosTutorIdentityDbContext context)
        {
            _context = context;
        }
        private string Layout = "TutorLayout";
        public IActionResult Index()
        {

            ViewBag.Layout = Layout;
            return View();
        }
       

    }
}