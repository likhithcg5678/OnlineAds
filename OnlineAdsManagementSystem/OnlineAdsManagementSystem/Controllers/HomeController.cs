using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineAdsManagementSystem.Models;
using System.Diagnostics;

namespace OnlineAdsManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly NewOnlineAdsDbContext context;

        public HomeController(NewOnlineAdsDbContext context)
        {
            this.context = context;
        }


       
        public IActionResult Index()
        {
            return View();
        }

        
        public IActionResult Login()
        {
            List<SelectListItem> Usertype = new()
            {
                new SelectListItem { Value = "U", Text = "U" },
                new SelectListItem { Value = "A", Text = "A" },
            };
            ViewBag.Usertype = Usertype;

            if (HttpContext.Session.GetString("AdminSession") != null)
            {
                return RedirectToAction("AdminDashboard");
            }
            else if (HttpContext.Session.GetString("UserSession") != null)
            {
                return RedirectToAction("UserDashboard");
            }
            else
            {
                return View();
            }


        }
        [HttpPost]
        public IActionResult Login(User user)
        {
            var myUser = context.Users.Where(x => x.UserType == user.UserType && x.UserId == user.UserId && x.Password == user.Password).FirstOrDefault();

            if (myUser != null && myUser.UserType == "A")
            {

                HttpContext.Session.SetString("AdminSession", myUser.UserId.ToString());
                HttpContext.Session.SetString("frontpageAdminname", myUser.FullName.ToString());
                return RedirectToAction("AdminDashboard");
            }
            else if (myUser != null)
            {
                HttpContext.Session.SetString("UserSession", myUser.UserId.ToString());
				HttpContext.Session.SetString("frontpagename", myUser.FullName.ToString());
				
                return RedirectToAction("UserDashboard");
            }
            else
            {
                ViewBag.Message = "Login Failed...";
                return View();
            }

        }

        public IActionResult AdminDashboard()
        {
            if (HttpContext.Session.GetString("AdminSession") != null)
            {
                ViewBag.MySession = HttpContext.Session.GetString("AdminSession").ToString();
                ViewBag.WelcomeAdmin = HttpContext.Session.GetString("frontpageAdminname").ToString();


            }
            else
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        public IActionResult UserDashboard()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                ViewBag.MySession = HttpContext.Session.GetString("UserSession").ToString();
                ViewBag.Try= HttpContext.Session.GetString("frontpagename").ToString();
                
                
				TempData["PassId"] = ViewBag.MySession;
                TempData["Passuserid"] = ViewBag.MySession;
                TempData["Passuseridfordeletion"] = ViewBag.MySession;
                TempData["PassuseridforIntest"] = ViewBag.MySession;


            }
            else
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        

        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("AdminSession") != null)
            {
                HttpContext.Session.Remove("AdminSession");
                return RedirectToAction("Login");
            }
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
        //[HttpPost]
        //public async Task<IActionResult> Register(User user)    ----MODELSTATE.ISVALID CONDITION NOT PASSED THIS SHOWS WHAT ARE THE VALIDATIONS THAT ARE NOT PASSED
        //{
        //    TempData["ValidationError"] = null;
        //    if (!ModelState.IsValid)
        //    {
        //        foreach (var modelStateEntry in ModelState.Values)
        //        {
        //            foreach (var error in modelStateEntry.Errors)
        //            {
        //                var errorMessage = error.ErrorMessage;
        //                TempData["ValidationError"]+= errorMessage; // Store error message in TempData
        //            }
        //        }
        //        return View(); // Return the view to display validation errors
        //    }

        //    // Your existing logic to add the user to the database
        //    // await context.Users.AddAsync(user);
        //    // await context.SaveChangesAsync();
        //    // TempData["Success"] = "Registration Successful";
        //    // TempData["NewUserId"]=user.UserId;

        //    return RedirectToAction("Register"); // Redirect to success page or appropriate action
        //}

        //[HttpPost]
        //public async Task<IActionResult> Register(User user)
        //{
        //    var checkuserexist=context.Users.Where(x=> x.EmailId == user.EmailId).FirstOrDefault();
        //    if (checkuserexist == null)
        //    {
        //        ModelState.Remove("UserType");
        //        ModelState.Remove("Ads");
        //        ModelState.Remove("Interests");
        //        if (ModelState.IsValid)
        //        {
        //            await context.Users.AddAsync(user);
        //            await context.SaveChangesAsync();
        //            TempData["Success"] = "Registration Successful!!!";
        //            TempData["NewUserId"] = user.UserId;
        //            return RedirectToAction("Register");
        //        }
        //    }

        //    TempData["UserExists"] = "User Already Exist ";


        //    return View();
        //}


        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            var checkuserexist = context.Users.Where(x => x.EmailId == user.EmailId).FirstOrDefault();
            if (checkuserexist == null)
            {
                ModelState.Remove("UserType");
                ModelState.Remove("Ads");
                ModelState.Remove("Interests");
                if (ModelState.IsValid)
                {
                    await context.Users.AddAsync(user);
                    await context.SaveChangesAsync();
                    TempData["Success"] = "Registration Successful!!!";
                    TempData["NewUserId"] = user.UserId;
                    return RedirectToAction("Register");
                }
            }

            TempData["UserExists"] = "User Already Exist ";
            return View();
        }





        public IActionResult PostAd()
        {
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
