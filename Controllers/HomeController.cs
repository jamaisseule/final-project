using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LUMOSBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using LUMOSBook.Utils;
using LUMOSBook.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using LUMOSBook.Service;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using OfficeOpenXml.Style;
using System.Net;
using Stripe.Checkout;

namespace LUMOSBook.Controllers;

public class HomeController : Controller
{
    private readonly LUMOSBookIdentityDbContext _context;
    private readonly UserManager<BookUser> _userManager;
    private readonly IVnPayService _vnPayservice;

    public HomeController(LUMOSBookIdentityDbContext context, UserManager<BookUser> userManager, IVnPayService vnPayService)
    {
        _context = context;
        _userManager = userManager;
        _vnPayservice = vnPayService;
    }

    // public HomeController(ILogger<HomeController> logger)
    // {
    //     _logger = logger;
    // }

    // public IActionResult Index()
    // {
    //     return View();
    // }


    public async Task<IActionResult> Index(string searchString)
    {
        var lUMOSBookContext = from m in _context.Book.Include(a => a.Category)
                                                    .Include(b => b.Author)
                                                    .Include(c => c.Publisher)
                               select m;

        if (!String.IsNullOrEmpty(searchString))
        {
            lUMOSBookContext = lUMOSBookContext.Where(s => s.Title!.Contains(searchString));
        }

        return View(await lUMOSBookContext.ToListAsync());
    }


    public async Task<IActionResult> Detail(int? id)
    {
        if (id == null || _context.Book == null)
        {
            return NotFound();
        }

        var book = await _context.Book
            .Include(b => b.Category)
            .Include(b => b.Author)
            .Include(b => b.Publisher)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (book == null)
        {
            return NotFound();
        }

        return View(book);
    }

    public IActionResult Help()
    {
        return View();
    }
    public IActionResult About()
    {
        return View();
    }
    public IActionResult Login()
    {
        return View();
    }
    public IActionResult Register()
    {
        return View();
    }
    // public IActionResult Detail()
    // {
    //     return View();
    // }
    [Authorize(Roles = "Customer, StoreOwner, Admin")]
    public IActionResult Cart()
    {
        return View();
    }
    [Authorize(Roles = "Customer, StoreOwner, Admin")]
    public IActionResult Profile()
    {
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    [Authorize(Roles = "Customer, StoreOwner, Admin")]
    [HttpPost]
    public IActionResult AddBook(int id, string name, string poster, string author, decimal price, int quantity)
    {
        ShoppingCart myCart;
        // If the cart is not in the session, create one and put it there
        // Otherwise, get it from the session
        if (HttpContext.Session.GetObject<ShoppingCart>("cart") == null)
        {
            myCart = new ShoppingCart();
            HttpContext.Session.SetObject("cart", myCart);
        }
        myCart = (ShoppingCart)HttpContext.Session.GetObject<ShoppingCart>("cart");
        var newItem = myCart.AddItem(id, name, poster, author, price, quantity);
        HttpContext.Session.SetObject("cart", myCart);
        ViewData["newItem"] = newItem;
        return View();
    }

    [Authorize(Roles = "Customer, StoreOwner, Admin")]
    public async Task<IActionResult> CheckOut()
    {
        ShoppingCart cart = (ShoppingCart)HttpContext.Session.GetObject<ShoppingCart>("cart");
        ViewData["uid"] = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var users = await _userManager.Users.Where(u => u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier)).ToListAsync();
        var userRolesViewModel = new List<UserRolesViewModel>();
        foreach (BookUser user in users)
        {
            var thisViewModel = new UserRolesViewModel();
            thisViewModel.UserId = user.Id;
            thisViewModel.Name = user.Name;
            thisViewModel.PhoneNumber = user.PhoneNumber;
            thisViewModel.Address = user.Address;
            userRolesViewModel.Add(thisViewModel);
        }

        if (cart != null)
        {
            ViewData["myItems"] = cart.Items;
            return View(userRolesViewModel);
        }
        else
        {
            return View("EmptyCart");
        }
    }
    [Authorize]
    public IActionResult OrderConfirmation()
    {
        var service = new SessionService();
        Session session = service.Get(TempData["Session"].ToString());

        if(session.PaymentStatus == "paid")
        {
            var transaction = session.PaymentIntentId.ToString();
            return View("Success");
        }
        return View("Fail");

    }
    [Authorize]
    public IActionResult Success()
    {
        return View();
    }
    [Authorize]
    public IActionResult Fail()
    {
        return View();
    }

    [Authorize(Roles = "Customer, StoreOwner, Admin")]
    public IActionResult PlaceOrder(decimal total, string fullname, string address, string phone, string cusid)
    {
        ShoppingCart cart = HttpContext.Session.GetObject<ShoppingCart>("cart");

        // Ensure items exist in the cart
        if (cart == null || cart.Items.Count == 0)
        {
            // Handle empty cart scenario
            return RedirectToAction("Index", "Cart");
        }

        // Create an order
        Order myOrder = new Order
        {
            OrderTime = DateTime.Now,
            Total = total,
            Fullname = fullname,
            Address = address,
            UserID = cusid,
            Phone = phone,
            State = "In Process"
        };

        _context.Order.Add(myOrder);
        _context.SaveChanges(); // Generates the Id for Order

        // Create order items
        foreach (var item in cart.Items)
        {
            OrderItem myOrderItem = new OrderItem
            {
                BookID = item.ID,
                Quantity = item.Quantity,
                // OrderID = myOrder.Id // Id of saved order above
            };

            _context.OrderItem.Add(myOrderItem);
        }

        _context.SaveChanges();

        // Clear shopping cart
        HttpContext.Session.Remove("cart");

        // Create Stripe Checkout session
        var domain = "https://localhost:7219/";

        var options = new SessionCreateOptions
        {
            SuccessUrl = domain + "home/checkout/OrderConfirmation",
            CancelUrl = domain + "home/checkout/Fail",
            PaymentMethodTypes = new List<string> { "card" },
            Mode = "payment",
            LineItems = new List<SessionLineItemOptions>()
        };

        foreach (var item in cart.Items)
        {
            var sessionListItem = new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmount = (long)(item.Price * 100), // Price per unit in cents
                    Currency = "usd",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = item.Title
                    }
                },
                Quantity = item.Quantity
            };

            options.LineItems.Add(sessionListItem);
        }

        var service = new SessionService();
        Session session = service.Create(options);

        TempData["Session"] = session.Id;

        return Redirect(session.Url);
    }

    

    [Authorize]
    public IActionResult PaymentSuccess()
    {
        return View("Success");
    }

    [HttpPost]
    public RedirectToActionResult EditOrder(int id, int quantity)
    {
        ShoppingCart cart = (ShoppingCart)HttpContext.Session.GetObject<ShoppingCart>("cart");
        cart.EditItem(id, quantity);
        HttpContext.Session.SetObject("cart", cart);

        return RedirectToAction("CheckOut", "Home");
    }
    [HttpPost]
    public RedirectToActionResult RemoveOrderItem(int id)
    {
        ShoppingCart cart = (ShoppingCart)HttpContext.Session.GetObject<ShoppingCart>("cart");
        cart.RemoveItem(id);
        HttpContext.Session.SetObject("cart", cart);

        return RedirectToAction("CheckOut", "Home");
    }

    [Authorize(Roles = "Customer, StoreOwner, Admin")]
    public async Task<IActionResult> Record()
    {
        var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);

        return _context.Order != null ?
                    View(await _context.Order
                    .Where(o => o.UserID == userID)
                    .ToListAsync()) :
                    Problem("Entity set 'LUMOSBookContext.Order'  is null.");
    }
    [Authorize(Roles = "Customer, StoreOwner, Admin")]
    public async Task<IActionResult> OrderDetail(int id)
    {
        var lUMOSBookContext = _context.OrderItem.Where(e => e.Order.Id == id).Include(b => b.Book).Include(o => o.Order).Include(c => c.Book.Author);
        return View(await lUMOSBookContext.ToListAsync());
    }
    [Authorize]
    public IActionResult PaymentFail()
    {
        return View();
    }

    [Authorize]
    public IActionResult PaymentCallBack()
    {
        var response = _vnPayservice.PaymentExecute(Request.Query);

        if (response == null || response.VnPayResponseCode != "00")
        {
            TempData["Message"] = $"Error Payment VN Pay: {response.VnPayResponseCode}";
            return RedirectToAction("PaymentFail");
        }


        // Lưu đơn hàng vô database

        TempData["Message"] = $"Payment by VnPay Success!";
        return RedirectToAction("PaymentSuccess");
    }


}