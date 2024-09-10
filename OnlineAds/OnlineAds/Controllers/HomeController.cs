using Microsoft.AspNetCore.Mvc;
using OnlineAds.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace OnlineAds.Controllers
{
    public class HomeController : Controller
    {
        private readonly OnlinedbContext context;

        public HomeController(OnlinedbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("UserSession") != null )
            {
                return RedirectToAction("Dashboard");
            }
            if ( HttpContext.Session.GetString("AdminSession") != null)
            {
                return RedirectToAction("AdminDashboard");
            }
            return View();
        }
        [HttpPost]
        public IActionResult Login(UserTbl user)
        {
            var myUser = context.UserTbls.Where(x => x.Email == user.Email && x.Password == user.Password).FirstOrDefault();
            if (myUser != null)
            {
                HttpContext.Session.SetString("UserSession", myUser.Email);
                return RedirectToAction("Dashboard");
            }
            else
            {
                ViewBag.Message = "Login Failed...";
            }
            return View();
        }

        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                ViewBag.MySession = HttpContext.Session.GetString("UserSession").ToString();

            }
            else
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                HttpContext.Session.Remove("UserSession");
                return RedirectToAction("Login");
            }
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserTbl user)
        {
            if (ModelState.IsValid)
            {
                await context.UserTbls.AddAsync(user);
                await context.SaveChangesAsync();
                TempData["Success"] = "Registration Successful!!!";
                TempData["UserId"]=user.UserId;
                //return RedirectToAction("Login");
            }

            return View();
        }

        public IActionResult AdminLogin()
        {
            if (HttpContext.Session.GetString("AdminSession")!=null)
            {
                return RedirectToAction("AdminDashboard");
            }
            return View();
        }
        [HttpPost]
        public IActionResult AdminLogin(UserTbl user)
        {
            var adminUser= context.UserTbls.Where(x => x.Email == user.Email && x.Password == user.Password).FirstOrDefault();
            if (adminUser!=null)
            {
                HttpContext.Session.SetString("AdminSession", adminUser.Email);
                return RedirectToAction("AdminDashboard");
            }
            else
            {
                ViewBag.AdminMessage = "Admin Login Failed";
            }
            return View();
        }
        public IActionResult AdminDashboard()
        {
            if (HttpContext.Session.GetString("AdminSession")!=null)
            {
                ViewBag.MySession = HttpContext.Session.GetString("AdminSession").ToString();
            }
            else
            {
                return RedirectToAction("AdminLogin");
            }


            return View();
        }

        public IActionResult AdminLogout()
        {
            if (HttpContext.Session.GetString("AdminSession")!=null)
            {
                HttpContext.Session.Remove("AdminSession");
                return RedirectToAction("AdminLogin");
            }
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
