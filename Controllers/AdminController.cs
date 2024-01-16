using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace Final.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        
        private string Layout ="AdminLayout"; 
        public IActionResult Index()
        {   
             ViewBag.Layout = Layout;
            return View();
        }

    }
}