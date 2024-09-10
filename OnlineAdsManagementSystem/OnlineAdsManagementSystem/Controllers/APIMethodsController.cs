using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OnlineAdsManagementSystem.Models;
using OnlineAdsManagementSystemWebAPI.Models;
using System;
using System.Diagnostics.SymbolStore;
using System.Security.Policy;
using System.Text;

namespace OnlineAdsManagementSystem.Controllers
{
    public class APIMethodsController : Controller
    {
        public APIMethodsController(NewOnlineAdsDbContext context, IWebHostEnvironment env)
        {
            this.context = context;
            this.env = env;
        }
        private string url = "https://localhost:7186/api/OnlineAdsAPI/";
        private HttpClient client = new HttpClient();
        private readonly NewOnlineAdsDbContext context;
        IWebHostEnvironment env;

        [HttpGet]
        public IActionResult ViewAllAds()
        {
            List<NewViewAllAds> DisplayAllAds = new List<NewViewAllAds>();
            HttpResponseMessage response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<List<NewViewAllAds>>(result);

                if (data != null)
                {
                    DisplayAllAds = data;
                }
            }
            return View(DisplayAllAds);
        }

        [HttpGet]
        public IActionResult PostYourAd()
        {
            List<SelectListItem> Status = new()
    {
        new SelectListItem { Value="Inactive", Text="Inactive"}
    };
            ViewBag.Status = Status;


            var mainCategories = context.MainCategories
        .Select(c => new SelectListItem
        {
            Value = c.Mcname,
            Text = c.Mcname,
            Group = new SelectListGroup { Name = c.Mcid.ToString() } // Use Group for storing mcid
        }).Distinct().ToList();


            //  var subCategories = context.SubCategories
            //.Select(c => new SelectListItem
            //{
            //    Value = c.Scid.ToString(),
            //    Text = c.Scname
            //}).Distinct().ToList();

            ViewBag.MainCategories = mainCategories;
            //ViewBag.SubCategories = subCategories;

            ViewBag.SubCategories = new List<SelectListItem>();

            return View();


        }

        [HttpGet]
        public IActionResult GetSubCategories(int mcid)
        {
            var subCategories = context.SubCategories
                .Where(sc => sc.Mcid == mcid)
                .Select(sc => new SelectListItem
                {
                    Value = sc.Scname,
                    Text = sc.Scname,
                    Group = new SelectListGroup { Name = sc.Scid.ToString() } // Use Group for storing scid
                }).ToList();

            return Json(subCategories);
        }


        [HttpPost]
        public IActionResult PostYourAd(Ad ad)
        {
            TempData["RecordId"] = ad.UserId;

            string data = JsonConvert.SerializeObject(ad);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(url+ "CreateAd", content).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var createdAd = JsonConvert.DeserializeObject<Ad>(result); // Deserialize the response to get the created ad

                TempData["Insert Ad"] = "Ad Posted Successfully";


                //	var getAdFromDB = context.Ads
                //.Where(x => x.UserId == ad.UserId && x.AdTitle == ad.AdTitle)
                //.OrderByDescending(x => x.AdId)
                //.FirstOrDefault();
                //	TempData["NewAdId"] = getAdFromDB.AdId;

                TempData["NewAdId"] = createdAd.AdId;


                return RedirectToAction("ViewMyAds", new { userid = ad.UserId });
            }
            return View();
        }

        [HttpGet]
        public IActionResult ViewMyAds(int userid)
        {

            List<NewViewMyAdsViewModel> DisplayMyAds = new List<NewViewMyAdsViewModel>();
            HttpResponseMessage response = client.GetAsync(url + "user/" + userid).Result;
            if (response.IsSuccessStatusCode)
            {

                string result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<List<NewViewMyAdsViewModel>>(result);
                if (data != null)
                {
                    DisplayMyAds = data;
                }
            }
            return View(DisplayMyAds);
        }


        [HttpGet]
        public IActionResult ViewMyInterests(int userid)
        {

            List<FilteredPropertiesOfInterest> DisplayMyInterests = new List<FilteredPropertiesOfInterest>();
            HttpResponseMessage response = client.GetAsync(url + "viewmyinterests/" + userid).Result;
            if (response.IsSuccessStatusCode)
            {

                string result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<List<FilteredPropertiesOfInterest>>(result);
                if (data != null)
                {
                    DisplayMyInterests = data;
                }
            }
            return View(DisplayMyInterests);
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {

            List<SelectListItem> Status = new()
    {
        new SelectListItem { Value="Inactive", Text="Inactive"}
    };
            ViewBag.Status = Status;

            List<SelectListItem> ForAdminStatus = new()
    {
        new SelectListItem { Value="Active", Text="Active"},
        new SelectListItem { Value="Inactive", Text="Inactive"}
    };
            ViewBag.ForAdminStatus = ForAdminStatus;


            var mainCategories = context.MainCategories
        .Select(c => new SelectListItem
        {
            Value = c.Mcname,
            Text = c.Mcname,
            Group = new SelectListGroup { Name = c.Mcid.ToString() } // Use Group for storing mcid
        }).Distinct().ToList();

            ViewBag.MainCategories = mainCategories;
            ViewBag.SubCategories = new List<SelectListItem>();


            NewViewMyAds editAd = new NewViewMyAds();
            HttpResponseMessage response = client.GetAsync(url + "ads/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<NewViewMyAds>(result);
                if (data != null)
                {
                    editAd = data;
                }
            }
            return View(editAd);
        }

        [HttpPost]
        public IActionResult Edit(Ad ad)
        {

            string data = JsonConvert.SerializeObject(ad);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync(url + "UpdateAd/"+ad.AdId, content).Result;
            if (response.IsSuccessStatusCode)
            {
                TempData["Update Ad"] = "Ad Updated Successfully";

                return RedirectToAction("ViewAllAds");

            }
            return View();
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            NewViewMyAds adDetails = new NewViewMyAds();

            HttpResponseMessage response = client.GetAsync(url + "ads/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<NewViewMyAds>(result);
                if (data != null)
                {
                    adDetails = data;
                }
            }
            return View(adDetails);
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            NewViewMyAds adDetails = new NewViewMyAds();

            HttpResponseMessage response = client.GetAsync(url + "ads/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<NewViewMyAds>(result);
                if (data != null)
                {
                    adDetails = data;
                }
            }
            return View(adDetails);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            HttpResponseMessage response = client.DeleteAsync(url + "DeleteAd/" + id).Result;
            if (response.IsSuccessStatusCode)
            {


                TempData["Delete Ad"] = "Ad Deleted Successfully";

                return RedirectToAction("ViewMyAds", new { userid = TempData["RecordId"] });

            }
            return View();
        }





        [HttpGet]
        public IActionResult SendInt(int adId, int userId)
        {
            var model = new Interest
            {
                AdId = adId,
                UserId = userId
            };
            return View(model);
        }

        public IActionResult InterestWarning()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SendInt(Interest sendInterest)
        {

            var interestexist = context.Interests.Where(x => x.AdId == sendInterest.AdId && x.UserId == sendInterest.UserId).FirstOrDefault();
            if (interestexist != null)
            {
                TempData["InterestIdExist"] = "You have already sent Interest.";
                return RedirectToAction("InterestWarning");
            }
            string data = JsonConvert.SerializeObject(sendInterest);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(url + "sendinterest", content).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var sendInterestsuccess = JsonConvert.DeserializeObject<Interest>(result); // Deserialize the response to get the created ad
                HttpContext.Session.SetString("InterestRequest", sendInterestsuccess.InterestId.ToString());
                TempData["SentInterest"] = "Successfully Sent Interested request ";
                return RedirectToAction("ViewAllAds");
            }
            return View();
        }


		[HttpGet]
		public IActionResult AddImage(int adId, int uid)
		{
			ViewBag.AdId = adId;
			ViewBag.UserId = uid;
			return View();
		}

		[HttpPost]
		public IActionResult AddImage(AdImageNewDataType img, int uid)
		{
			if (img.photoUrl != null)
			{
				string folder = Path.Combine(env.WebRootPath, "images");
				string fileName = Guid.NewGuid().ToString() + "_" + img.photoUrl.FileName;
				string filePath = Path.Combine(folder, fileName);

				// Save the new image file
				using (var fileStream = new FileStream(filePath, FileMode.Create))
				{
					img.photoUrl.CopyTo(fileStream);
				}

				// Check if there's already an image for the given ad
				var existingImage = context.AdImages.FirstOrDefault(ai => ai.AdId == img.AdId);

				if (existingImage != null)
				{
					// Delete the old image file if it exists
					string oldImagePath = Path.Combine(folder, existingImage.ImageUrl);
					if (System.IO.File.Exists(oldImagePath))
					{
						System.IO.File.Delete(oldImagePath);
					}

					// Update the existing record with the new image path
					existingImage.ImageUrl = fileName;
					context.AdImages.Update(existingImage);
				}
				else
				{
					// If no image exists, create a new record
					var newImage = new AdImage
					{
						AdId = img.AdId,
						ImageUrl = fileName
					};
					context.AdImages.Add(newImage);
				}

				// Save changes to the database
				context.SaveChanges();
			}

			TempData["RecordId"] = uid;

			return RedirectToAction("ViewMyAds", new { userid = uid });
		}




		[HttpGet]
        public IActionResult ShowInterestForMyAds(int adId)
        {
            List<FilteredPropertiesOfInterest> othersInterest = new List<FilteredPropertiesOfInterest>();

            HttpResponseMessage response = client.GetAsync(url + "viewinteresttomyads/" + adId).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<List<FilteredPropertiesOfInterest>>(result);
                if (data != null)
                {
                    othersInterest = data;
                }
            }
            return View(othersInterest);
        }

        //--------------------working---------------------

        //public IActionResult SearchAdsBasedOnCategory()
        //{

        //    return View();
        //}

        //public IActionResult ViewAdsBasedOnCategory()
        //{
        //    if (TempData["CategoryAds"] != null)
        //    {
        //        var json = TempData["CategoryAds"].ToString();
        //        var listCatAds = JsonConvert.DeserializeObject<List<NewViewAllAds>>(json);
        //        return View(listCatAds);
        //    }

        //    return View(new List<NewViewAllAds>());
        //}



        //[HttpGet]
        //public IActionResult SearchAdsBasedOnCategory(string category)
        //{
        //    List<NewViewAllAds> listCatAds = new List<NewViewAllAds>();

        //    HttpResponseMessage response = client.GetAsync(url + "SearchAdsByMainCategory/" + category).Result;
        //    if (response.IsSuccessStatusCode)
        //    {
        //        string result = response.Content.ReadAsStringAsync().Result;
        //        var catspecificads = JsonConvert.DeserializeObject<List<NewViewAllAds>>(result); // Deserialize the response to get the created ad
        //        TempData["CategoryAds"] = JsonConvert.SerializeObject(catspecificads); // Store list as JSON string in TempData
        //        return RedirectToAction("ViewAdsBasedOnCategory");
        //    }
        //    return View();
        //}

        //----------------------------upper code working-----------------
        [HttpGet]
        public IActionResult SearchCategoryWise()
        {
            var mainCategories = context.MainCategories
        .Select(c => new SelectListItem
        {
            Value = c.Mcname,
            Text = c.Mcname,
            Group = new SelectListGroup { Name = c.Mcid.ToString() } // Use Group for storing mcid
        }).Distinct().ToList();

            ViewBag.MainCategories = mainCategories;

            return View();
        }




        [HttpPost]
        public IActionResult SearchCategoryWise(OnlyMC mc)
        {
            List<NewViewAllAds> mclist = new List<NewViewAllAds>();
            string data = JsonConvert.SerializeObject(mc);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(url + "FilterMainCategoryAds", content).Result;

            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var listofads = JsonConvert.DeserializeObject<List<NewViewAllAds>>(result); // Deserialize the response to get the created ad
                return View("SearchCategoryWiseList", listofads);
            }
            else
            {
                TempData["NoCategory"] = "No Ads Under This Category";
                return View();    // fail the if condition and see the result
            }



        }

        [HttpGet]
        public IActionResult AddNewMainCategories()
        {

            return View();
        }


        [HttpPost]
        public IActionResult AddNewMainCategories(FilteredPropertiesOfMainCategory addNewMainCatg)
        {
            string data = JsonConvert.SerializeObject(addNewMainCatg);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(url + "addNewMainCategories", content).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var addNewMainCatSuccess = JsonConvert.DeserializeObject<FilteredPropertiesOfMainCategory>(result);
                TempData["addNewMCDone"] = " New Main Category Added Successfully";
                return RedirectToAction("AddNewMainCategories");
            }
            if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                // Handle the case where the category already exists
                TempData["addNewMCError"] = "Category already exists";
            }
            return View();
        }

        [HttpGet]
        public IActionResult AddNewSubCategories()
        {
            var mainCategories = context.MainCategories
        .Select(c => new SelectListItem
        {
            Value = c.Mcname,
            Text = c.Mcname,
            Group = new SelectListGroup { Name = c.Mcid.ToString() } // Use Group for storing mcid
        }).Distinct().ToList();

            ViewBag.MainCategories = mainCategories;
            return View();
        }

        [HttpPost]
        public IActionResult AddNewSubCategories(FilteredPropertiesOfSubCategory addNewSubCatg)
        {
            string data = JsonConvert.SerializeObject(addNewSubCatg);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(url + "addNewSubCategories", content).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var addNewSubCatSuccess = JsonConvert.DeserializeObject<FilteredPropertiesOfSubCategory>(result);
                TempData["addNewSCDone"] = "New Sub Category Added Successfully";
                return RedirectToAction("AddNewSubCategories");
            }
            if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                TempData["addNewSCError"] = "Sub Category already exist";
            }
            return View();
        }

        [HttpGet]
        public IActionResult ViewMainCategories()
        {
            List<FilteredPropertiesOfMainCategory> mc = new List<FilteredPropertiesOfMainCategory>();
            HttpResponseMessage response = client.GetAsync(url + "ViewAllMainCategories").Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<List<FilteredPropertiesOfMainCategory>>(result);
                if (data != null)
                {
                    mc = data;
                }
            }

            return View(mc);
        }


        [HttpGet]
        public IActionResult EditMainCategories(int id)
        {
            FilteredPropertiesOfMainCategory editinfomc = new FilteredPropertiesOfMainCategory();
            HttpResponseMessage response = client.GetAsync(url + "GetMainCategory/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<FilteredPropertiesOfMainCategory>(result);
                if (data != null)
                {
                    editinfomc = data;
                }

            }
            return View(editinfomc);

        }



        [HttpPost]
        public IActionResult EditMainCategories(FilteredPropertiesOfMainCategory editmc)
        {
            string data = JsonConvert.SerializeObject(editmc);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync(url + "editMainCategory/" + editmc.mcid, content).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var info = JsonConvert.DeserializeObject<FilteredPropertiesOfSubCategory>(result);
                TempData["EditMcSuccess"] = "MainCategory edited Successfully";
                return RedirectToAction("ViewMainCategories");
            }
            if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                TempData["addNewSCError"] = "Sub Category already exist";
            }
            return View();
        }


        [HttpGet]
        public IActionResult DeleteMc(int id)
        {
            DelMainCategoryDTO mcDetails = new DelMainCategoryDTO();

            HttpResponseMessage response = client.GetAsync(url + "GetMainCategory/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<DelMainCategoryDTO>(result);
                if (data != null)
                {
                    mcDetails = data;
                }
            }
            return View(mcDetails);
        }

        [HttpPost, ActionName("DeleteMc")]
        public IActionResult DeleteMcConfirm(int id)
        {
            HttpResponseMessage response = client.DeleteAsync(url + "DeleteMainCategory/" + id).Result;
            if (response.IsSuccessStatusCode)
            {


                TempData["DeleteMc"] = "MainCategory Deleted Successfully";

                return RedirectToAction("ViewMainCategories");

            }
            return View();
        }


        public IActionResult SearchAds()
        {
            return View();
        }

        public IActionResult SearchAdsResult(List<NewViewAllAds> data)
        {
            return View(data);
        }




        [HttpPost]
        public async Task<IActionResult> SearchAds(string title)
        {
            List<NewViewAllAds> ads = new List<NewViewAllAds>();
            if (!string.IsNullOrEmpty(title))
            {
                var payload = new { title = title };
                string data = JsonConvert.SerializeObject(payload);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(url + "SearchAds", content);
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    var info = JsonConvert.DeserializeObject<List<NewViewAllAds>>(result);
                    if (info != null)
                    {
                        ads = info;
                    }

                    if (ads == null || !ads.Any())
                    {
                        TempData["NoAdsFound"] = "No ads matched your search criteria.";
                    }

                    return View("SearchAdsResult", ads);
                }
                else
                {
                    // Handle error response
                    string error = await response.Content.ReadAsStringAsync();
                    ViewBag.ErrorTitle = error;
                    // Consider logging the error
                }
            }

            return View();
        }





        //[HttpGet]
        //public async Task<IActionResult> ViewUserDetails(int userid)
        //{
        //    ViewUserDetailsDTO userinfo = new ViewUserDetailsDTO();
        //    HttpResponseMessage response = client.GetAsync(url + "GetUserDetails/" + userid).Result;
        //    if (response.IsSuccessStatusCode)
        //    {
        //        string result = response.Content.ReadAsStringAsync().Result;
        //        var data = JsonConvert.DeserializeObject<ViewUserDetailsDTO>(result);
        //        if (data != null)
        //        {
        //            userinfo = data;
        //        }
        //    }
        //    return View(userinfo);
        //}

        [HttpGet]
        public IActionResult ViewUserDetails(int userid)
        {
            if (userid != null)
            {
                ViewUserDetailsDTO userinfo = new ViewUserDetailsDTO();
                HttpResponseMessage response = client.GetAsync(url + "GetUserDetails/" + userid).Result;
                if (response.IsSuccessStatusCode)
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<ViewUserDetailsDTO>(result);

                    if (data != null)
                    {
                        userinfo = data;
                    }
                }
                return View(userinfo);
            }
            return View();

        }






    }
}



