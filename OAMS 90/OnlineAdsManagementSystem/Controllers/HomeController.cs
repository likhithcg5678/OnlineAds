//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
////using OnlineAdsManagementSystem.Models;
//using System.Diagnostics;

//namespace OnlineAdsManagementSystem.Controllers
//{
//    public class HomeController : Controller
//    {
//        //private readonly OnlineAdsDbContext context;

//        //public HomeController( OnlineAdsDbContext context)
//        //{
//        //    this.context = context;
//        //}

//        public IActionResult Index()
//        {
//            return View();
//        }
//        public IActionResult Login()
//        {
//            if (HttpContext.Session.GetString("AdminSession") != null )
//            {
//                return RedirectToAction("AdminDashboard");
//            }
//            else if(HttpContext.Session.GetString("UserSession") != null)
//            {
//                return RedirectToAction("UserDashboard");
//            }
//            else
//            {
//                return View();
//            }
            
            
//        }
//        [HttpPost]
//        public IActionResult Login(User user)
//        {
//            var myUser = context.Users.Where(x => x.UserType == user.UserType && x.UserId == user.UserId && x.Password == user.Password).FirstOrDefault();

//            if (myUser != null && myUser.UserType == "A")
//            {
               
//                HttpContext.Session.SetString("AdminSession", myUser.UserId.ToString());
//                return RedirectToAction("AdminDashboard");
//            }
//            else if (myUser != null)
//            {
//                HttpContext.Session.SetString("UserSession", myUser.UserId.ToString());
//                TempData["UserName"]=myUser.FullName;
//                return RedirectToAction("UserDashboard");
//            }
//            else
//            {
//                ViewBag.Message = "Login Failed...";
//                return View();
//            }
            
//        }
       
//        public IActionResult AdminDashboard()
//        {
//            if (HttpContext.Session.GetString("AdminSession") != null)
//            {
//                ViewBag.MySession = HttpContext.Session.GetString("AdminSession").ToString();

//            }
//            else
//            {
//                return RedirectToAction("Login");
//            }
//            return View();
//        }
//        public IActionResult UserDashboard()
//        {
//            if (HttpContext.Session.GetString("UserSession") != null)
//            {
//                ViewBag.MySession = HttpContext.Session.GetString("UserSession").ToString();

//            }
//            else
//            {
//                return RedirectToAction("Login");
//            }
//            return View();
//        }

//        public IActionResult Logout()
//        {
//            if (HttpContext.Session.GetString("AdminSession") != null )
//            {
//                HttpContext.Session.Remove("AdminSession");
//                return RedirectToAction("Login");
//            }
//            if (HttpContext.Session.GetString("UserSession") != null)
//            {
//                HttpContext.Session.Remove("UserSession");
//                return RedirectToAction("Login");
//            }

//            return View();
//        }

//        public IActionResult Register()
//        {
//            return View();
//        }
//        //[HttpPost]
//        //public async Task<IActionResult> Register(User user)    ----MODELSTATE.ISVALID CONDITION NOT PASSED THIS SHOWS WHAT ARE THE VALIDATIONS THAT ARE NOT PASSED
//        //{
//        //    TempData["ValidationError"] = null;
//        //    if (!ModelState.IsValid)
//        //    {
//        //        foreach (var modelStateEntry in ModelState.Values)
//        //        {
//        //            foreach (var error in modelStateEntry.Errors)
//        //            {
//        //                var errorMessage = error.ErrorMessage;
//        //                TempData["ValidationError"]+= errorMessage; // Store error message in TempData
//        //            }
//        //        }
//        //        return View(); // Return the view to display validation errors
//        //    }

//        //    // Your existing logic to add the user to the database
//        //    // await context.Users.AddAsync(user);
//        //    // await context.SaveChangesAsync();
//        //    // TempData["Success"] = "Registration Successful";
//        //    // TempData["NewUserId"]=user.UserId;

//        //    return RedirectToAction("Register"); // Redirect to success page or appropriate action
//        //}
//        [HttpPost]
//        public async Task<IActionResult> Register(User user)
//        {
//            ModelState.Remove("UserType");
//            ModelState.Remove("Ads");
//            ModelState.Remove("Interests");
//            if (ModelState.IsValid)
//            {
//                await context.Users.AddAsync(user);
//                await context.SaveChangesAsync();
//                TempData["Success"] = "Registration Successful!!!";
//                TempData["NewUserId"] = user.UserId;
//                return RedirectToAction("Register");
//            }



//            return View();
//        }


//        public IActionResult PostAd()
//        {
//            return View();
//        }
//        public IActionResult Privacy()
//        {
//            return View();
//        }

//        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//        public IActionResult Error()
//        {
//            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
//        }
//    }
//}
