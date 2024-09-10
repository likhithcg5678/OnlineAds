using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using OnlineAdsManagementSystemWebAPI.Models;
using System.Xml.Linq;

namespace OnlineAdsManagementSystemWebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OnlineAdsAPIController : ControllerBase
	{
		private readonly NewOnlineAdsDbContext context;

		public OnlineAdsAPIController(NewOnlineAdsDbContext context)
		{
			this.context = context;
		}
        //[HttpGet]
        //public async Task<ActionResult<List<AdDTO>>> GetAllAds()
        //{
        //	var data = await context.Ads.ToListAsync();
        //          var temp=new List<AdDTO>();

        //          foreach (var item in data)
        //          {
        //              var mcInfo = await context.MainCategories.FindAsync(item.Mcid);
        //              var scInfo = await context.SubCategories.FindAsync(item.Scid);
        //              var adDto = new AdDTO
        //              {
        //                  AdId = item.AdId,
        //                  UserId = item.UserId,
        //                  Mcname = mcInfo.Mcname,
        //                  Scname = scInfo.Scname,
        //                  AdTitle = item.AdTitle,
        //                  AdDescription = item.AdDescription,
        //                  ContactName = item.ContactName,
        //                  ContactEmail = item.ContactEmail,
        //                  PhoneNumber = item.PhoneNumber,
        //                  Status = item.Status,

        //              };

        //              temp.Add(adDto);
        //          }
        //	return Ok(temp);
        //}



        [HttpGet]
        public async Task<ActionResult<List<AdDTO>>> GetAllAds()
        {
            var ads = await context.Ads.ToListAsync();
            var adDtos = new List<AdDTO>();

            foreach (var ad in ads)
            {
                var mcInfo = await context.MainCategories.FindAsync(ad.Mcid);
                var scInfo = await context.SubCategories.FindAsync(ad.Scid);

                // Fetch the most recent image for this ad
                var latestImage = await context.AdImages
                    .Where(ai => ai.AdId == ad.AdId)
                    .OrderByDescending(ai => ai.ImageId) // Assuming you have an UploadDate or similar field
                    .Select(ai => ai.ImageUrl)
                    .FirstOrDefaultAsync();

                var adDto = new AdDTO
                {
                    AdId = ad.AdId,
                    UserId = ad.UserId,
                    Mcname = mcInfo.Mcname,
                    Scname = scInfo.Scname,
                    AdTitle = ad.AdTitle,
                    AdDescription = ad.AdDescription,
                    ContactName = ad.ContactName,
                    ContactEmail = ad.ContactEmail,
                    PhoneNumber = ad.PhoneNumber,
                    Status = ad.Status,
                    ImageUrl = latestImage // Set the image URL
                };

                adDtos.Add(adDto);
            }

            return Ok(adDtos);
        }



        //[HttpGet("{id}")]---error bcs collision with [HttpGet("{userid}")]
        [HttpGet("ads/{id}")]

		public async Task<ActionResult<AdDTO>> GetAdsById(int id)
		{
			var data = await context.Ads.FindAsync(id);
			var mcId = data.Mcid;
            var scId = data.Scid;
            var mcInfo = await context.MainCategories.FindAsync(mcId);
            var scInfo = await context.SubCategories.FindAsync(scId);

            if (data == null)
			{
				return NotFound();
			}
			var adDto = new AdDTO
			{
				AdId = data.AdId,
				UserId = data.UserId,
				Mcname = mcInfo.Mcname,
				Scname = scInfo.Scname,
				AdTitle = data.AdTitle,
				AdDescription = data.AdDescription,
				ContactName = data.ContactName,
				ContactEmail = data.ContactEmail,
				PhoneNumber = data.PhoneNumber,
				Status = data.Status,
			};

			return Ok(adDto);

		}

		//[HttpGet("{userid}")]---- error bcs collision with [HttpGet("{id}")]
		//    [HttpGet("user/{userid}")]
		//    public async Task<ActionResult<IEnumerable<AdDTO>>> GetMyAds(int userid)
		//    {
		//        var ads = await context.Ads
		//                       .Where(a => a.UserId == userid)
		//                       .ToListAsync();

		//        if (ads == null || !ads.Any())
		//        {
		//            return NotFound();
		//        }

		//        var myAds = new List<AdDTO>();

		//        foreach (var ad in ads)
		//        {
		//            var mcInfo = await context.MainCategories.FindAsync(ad.Mcid);
		//            var scInfo = await context.SubCategories.FindAsync(ad.Scid);

		//            var adDto = new AdDTO
		//            {
		//                AdId = ad.AdId,
		//                UserId = ad.UserId,
		//                Mcname = mcInfo.Mcname,
		//                Scname = scInfo.Scname,
		//                AdTitle = ad.AdTitle,
		//                AdDescription = ad.AdDescription,
		//                ContactName = ad.ContactName,
		//                ContactEmail = ad.ContactEmail,
		//                PhoneNumber = ad.PhoneNumber,
		//                Status = ad.Status,
		//	ImageUrl = ad.ImageUrl
		//};

		//            myAds.Add(adDto);
		//        }

		//        return Ok(myAds);
		//    }

		[HttpGet("user/{userid}")]
		public async Task<ActionResult<IEnumerable<AdDTO>>> GetMyAds(int userid)
		{
			var ads = await context.Ads
						   .Where(a => a.UserId == userid)
						   .ToListAsync();

			if (ads == null || !ads.Any())
			{
				return NotFound();
			}

			var myAds = new List<AdDTO>();

			foreach (var ad in ads)
			{
				var mcInfo = await context.MainCategories.FindAsync(ad.Mcid);
				var scInfo = await context.SubCategories.FindAsync(ad.Scid);

				// Get the most recent image for the ad
				var adImage = await context.AdImages
									.Where(ai => ai.AdId == ad.AdId)
									.OrderByDescending(ai => ai.ImageId) // Assuming there's an Id or timestamp column
									.Select(ai => ai.ImageUrl)
									.FirstOrDefaultAsync();

				var adDto = new AdDTO
				{
					AdId = ad.AdId,
					UserId = ad.UserId,
					Mcname = mcInfo?.Mcname,
					Scname = scInfo?.Scname,
					AdTitle = ad.AdTitle,
					AdDescription = ad.AdDescription,
					ContactName = ad.ContactName,
					ContactEmail = ad.ContactEmail,
					PhoneNumber = ad.PhoneNumber,
					Status = ad.Status,
					ImageUrl = adImage // Include the most recent image URL
				};

				myAds.Add(adDto);
			}

			return Ok(myAds);
		}






        //[HttpGet("viewmyinterests/{userid}")]
        //      public async Task<ActionResult<InterestDTO>> GetMyInterests(int userid)
        //      {
        //          var ads = await context.Interests
        //                         .Where(a => a.UserId == userid)
        //                         .ToListAsync();

        //          if (ads == null || !ads.Any())
        //          {
        //              return NotFound();
        //          }

        //          var myinterestsAdsFetched = ads.Select(ad => new InterestDTO
        //          {
        //              InterestId = ad.InterestId,
        //              AdId = ad.AdId,
        //              UserId = ad.UserId,
        //              InterestMessage = ad.InterestMessage,
        //              Timestamp = ad.Timestamp

        //          }).ToList();

        //          return Ok(myinterestsAdsFetched);



        //      }



        [HttpGet("viewmyinterests/{userid}")]
        public async Task<ActionResult<List<InterestDTO>>> GetMyInterests(int userid)
        {
            var ads = await context.Interests
                           .Where(a => a.UserId == userid)
                           .ToListAsync();

            if (ads == null || !ads.Any())
            {
                return NotFound();
            }

            var myinterestsAdsFetched = new List<InterestDTO>();

            foreach (var ad in ads)
            {
                var adImage = await context.AdImages
                                    .Where(ai => ai.AdId == ad.AdId)
                                    .OrderByDescending(ai => ai.ImageId) // Assuming there's an Id or timestamp column
                                    .Select(ai => ai.ImageUrl)
                                    .FirstOrDefaultAsync();

                myinterestsAdsFetched.Add(new InterestDTO
                {
                    InterestId = ad.InterestId,
                    AdId = ad.AdId,
                    UserId = ad.UserId,
                    InterestMessage = ad.InterestMessage,
                    ImageUrl = adImage,
                    Timestamp = ad.Timestamp
                });
            }

            return Ok(myinterestsAdsFetched);
        }




        [HttpPost("CreateAd")]
        public async Task<ActionResult<AdDTOWithNoImageUrl>> CreateAd(AdDTOWithNoImageUrl adDto)
        {
            if (adDto == null)
            {
                return BadRequest("Ad data is null");
            }

            // Fetch the main category ID based on the name
            var mainCategory = await context.MainCategories
                                             .FirstOrDefaultAsync(mc => mc.Mcname == adDto.Mcname);
            if (mainCategory == null)
            {
                return BadRequest($"Main category '{adDto.Mcname}' not found");
            }

            // Fetch the subcategory ID based on the name
            var subCategory = await context.SubCategories
                                           .FirstOrDefaultAsync(sc => sc.Scname == adDto.Scname);
            if (subCategory == null)
            {
                return BadRequest($"Subcategory '{adDto.Scname}' not found");
            }

            // Create the Ad entity and assign the fetched IDs
            var ad = new Ad
            {
                UserId = adDto.UserId,
                Mcid = mainCategory.Mcid,
                Scid = subCategory.Scid,
                AdTitle = adDto.AdTitle,
                AdDescription = adDto.AdDescription,
                ContactName = adDto.ContactName,
                ContactEmail = adDto.ContactEmail,
                PhoneNumber = adDto.PhoneNumber,
                Status = adDto.Status
            };

            // Add the new Ad to the context and save changes
            await context.Ads.AddAsync(ad);
            await context.SaveChangesAsync();

            // Return the created Ad
            return Ok(ad);
        }




        [HttpPut("UpdateAd/{id}")]
        public async Task<ActionResult<AdDTOWithNoImageUrl>> UpdateAd(int id, AdDTOWithNoImageUrl adDto)
        {
            if (id != adDto.AdId)
            {
                return BadRequest();
            }

            var existingAd = await context.Ads.FindAsync(id);



            if (existingAd == null)
            {
                return NotFound();
            }

            var mainCategory = await context.MainCategories
                                             .FirstOrDefaultAsync(mc => mc.Mcname == adDto.Mcname);
            if (mainCategory == null)
            {
                return BadRequest($"Main category '{adDto.Mcname}' not found");
            }

            // Fetch the subcategory ID based on the name
            var subCategory = await context.SubCategories
                                           .FirstOrDefaultAsync(sc => sc.Scname == adDto.Scname);
            if (subCategory == null)
            {
                return BadRequest($"Subcategory '{adDto.Scname}' not found");
            }
            // Map the properties from the DTO to the entity
            existingAd.UserId = adDto.UserId;
            existingAd.Mcid = mainCategory.Mcid;
            existingAd.Scid = subCategory.Scid;
            existingAd.AdTitle = adDto.AdTitle;
            existingAd.AdDescription = adDto.AdDescription;
            existingAd.ContactName = adDto.ContactName;
            existingAd.ContactEmail = adDto.ContactEmail;
            existingAd.PhoneNumber = adDto.PhoneNumber;
            existingAd.Status = adDto.Status;
            context.Entry(existingAd).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();
            return Ok(existingAd);

        }

        [HttpDelete("DeleteAd/{id}")]
        public async Task<ActionResult<AdDTO>> DeleteAd(int id)
        {
            var delAd = await context.Ads.FindAsync(id);
            if (delAd == null)
            {
                return NotFound();
            }

            context.Ads.Remove(delAd);
            await context.SaveChangesAsync();
            return Ok();

        }



        ////[HttpPost("sendinterest")]
        ////public async Task<ActionResult<InterestDTO>> SendInterest(InterestDTO iDto)
        ////{
        ////    if (iDto == null)
        ////    {
        ////        return BadRequest("Interest data is null");
        ////    }
        ////    var interest = new Interest
        ////    {

        ////        AdId = iDto.AdId,
        ////        UserId = iDto.UserId,
        ////        InterestMessage = iDto.InterestMessage,

        ////    };
        ////    await context.Interests.AddAsync(interest);
        ////    await context.SaveChangesAsync();
        ////    return Ok(interest);


        ////}


        [HttpPost("AppendImage")]
        public async Task<ActionResult<AdImageDTO>> AppendImage(AdImageDTO adImg)
        {
            if (adImg == null)
            {
                return BadRequest("Ad Image data is null");
            }

           
           

            
            var change = new AdImage
            {
                AdId=adImg.AdId,
                ImageUrl=adImg.ImageUrl,
            };

            
            await context.AdImages.AddAsync(change);
            await context.SaveChangesAsync();

            // Return the created Ad
            return Ok(change);
        }



        [HttpPost("sendinterest")]
        public async Task<ActionResult<InterestDTO2>> SendInterest(InterestDTO2 iDto)
        {
            if (iDto == null)
            {
                return BadRequest("Interest data is null");
            }
            var interest = new Interest
            {

                AdId = iDto.AdId,
                UserId = iDto.UserId,
                InterestMessage = iDto.InterestMessage,
                Timestamp = DateTime.Now

            };
            await context.Interests.AddAsync(interest);
            await context.SaveChangesAsync();
            return Ok(interest);


        }

        [HttpGet("viewinteresttomyads/{aid}")]
        public async Task<ActionResult<InterestDTO>> ViewOthersInterest(int aid)
        {
            var interests = await context.Interests.Where(x => x.AdId == aid).ToListAsync();
            if (interests == null)
            {
                return NotFound();
            }
            var finalinterests = interests.Select(choose => new InterestDTO
            {
                AdId = choose.AdId,
                UserId = choose.UserId,
                InterestMessage = choose.InterestMessage,
                Timestamp = choose.Timestamp

            }).ToList();
            return Ok(finalinterests);

        }

		////[HttpPost("SearchAdsByMainCategory")]
		////public async Task<ActionResult<OnlyMainCategoryAllOutput>> SearchAdsByMainCategory(OnlyMainCategoryInput mc)
		////{
		////    var ads = await context.Ads
		////                   .Where(a => a.Mcname == mc.MCname)
		////                   .ToListAsync();

		////    if (ads == null || !ads.Any())
		////    {
		////        return NotFound();
		////    }

		////    var mcads = ads.Select(ad => new OnlyMainCategoryAllOutput
		////    {

		////        UserId = ad.UserId,
		////        MCname = ad.Mcname,
		////        SCname = ad.Scname,
		////        AdTitle = ad.AdTitle,
		////        AdDescription = ad.AdDescription,
		////        ContactName = ad.ContactName,
		////        ContactEmail = ad.ContactEmail,
		////        PhoneNumber = ad.PhoneNumber,
		////        Status = ad.Status

		////    }).ToList();

		////    return Ok(mcads);

		////}



		////-------------------working---------------------------

		////[HttpGet("SearchAdsByMainCategory/{mc}")]
		////public async Task<ActionResult<OnlyMainCategoryAllOutput>> SearchAdsByMainCategory(string mc)
		////{
		////    var ads = await context.Ads
		////                   .Where(a => a.Mcname == mc)
		////                   .ToListAsync();

		////    if (ads == null || !ads.Any())  // IF YOU GET THE DOUBT WHY I USED OnlyMainCategoryAllOutput CLASS AND OnlyMainCategoryInput REFER THE ABOVE METHOD WHICH I HAVE COMMENTED AND READ THE NOTE FILE ONLINEADSMANAGEMENTSYSTEM.TXT WHERE YOU HAVE WRITTEN THE THINGS YOU LEARNT IN THIS PROJECT
		////    {
		////        return NotFound();
		////    }

		////    var mcads = ads.Select(ad => new OnlyMainCategoryAllOutput
		////    {

		////        UserId = ad.UserId,
		////        MCname = ad.Mcname,
		////        SCname = ad.Scname,
		////        AdTitle = ad.AdTitle,
		////        AdDescription = ad.AdDescription,
		////        ContactName = ad.ContactName,
		////        ContactEmail = ad.ContactEmail,
		////        PhoneNumber = ad.PhoneNumber,
		////        Status = ad.Status

		////    }).ToList();

		////    return Ok(mcads);

		////}

		////---------------above code working----------------------


		//[HttpPost("FilterMainCategoryAds")]
		//public async Task<ActionResult<OnlyMainCategoryAllOutput>> FilterMainCategoryAds(OnlyMainCategoryInput mci)
		//{
		//    var mainCategory = await context.MainCategories
		//                                    .FirstOrDefaultAsync(mc => mc.Mcname == mci.MCname);
		//    if (mainCategory == null)
		//    {
		//        return BadRequest($"Main category '{mci.MCname}' not found");
		//    }

		//    var subCategory = await context.SubCategories
		//                                   .FirstOrDefaultAsync(sc => sc.Mcid == mainCategory.Mcid);
		//    if (subCategory == null)
		//    {
		//        return BadRequest();
		//    }

		//    var ads = await context.Ads
		//                   .Where(a => a.Mcid == mainCategory.Mcid)
		//                   .ToListAsync();

		//    if (ads == null || !ads.Any())
		//    {
		//        return NotFound();
		//    }

		//    var mcads = ads.Select(ad => new OnlyMainCategoryAllOutput
		//    {
		//        AdId = ad.AdId,
		//        UserId = ad.UserId,
		//        MCname = mainCategory.Mcname,
		//        SCname = subCategory.Scname,
		//        AdTitle = ad.AdTitle,
		//        AdDescription = ad.AdDescription,
		//        ContactName = ad.ContactName,
		//        ContactEmail = ad.ContactEmail,
		//        PhoneNumber = ad.PhoneNumber,
		//        Status = ad.Status,
		//        ImageUrl=


		//    }).ToList();

		//    return Ok(mcads);

		//}




		[HttpPost("FilterMainCategoryAds")]
		public async Task<ActionResult<List<OnlyMainCategoryAllOutput>>> FilterMainCategoryAds(OnlyMainCategoryInput mci)
		{
			// Fetch the main category based on the name
			var mainCategory = await context.MainCategories
											.FirstOrDefaultAsync(mc => mc.Mcname == mci.MCname);
			if (mainCategory == null)
			{
				return BadRequest($"Main category '{mci.MCname}' not found");
			}

			// Fetch the first subcategory based on the main category ID
			var subCategory = await context.SubCategories
										   .FirstOrDefaultAsync(sc => sc.Mcid == mainCategory.Mcid);
			if (subCategory == null)
			{
				return BadRequest();
			}

			// Fetch the ads associated with the main category
			var ads = await context.Ads
								   .Where(a => a.Mcid == mainCategory.Mcid)
								   .ToListAsync();

			if (ads == null || !ads.Any())
			{
				return NotFound();
			}

			var mcads = new List<OnlyMainCategoryAllOutput>();

			// Iterate over the ads and fetch the corresponding image URLs
			foreach (var ad in ads)
			{
				var adImage = await context.AdImages
										   .Where(ai => ai.AdId == ad.AdId)
										   .OrderByDescending(ai => ai.ImageId) // Assuming there's an Id or timestamp column
										   .Select(ai => ai.ImageUrl)
										   .FirstOrDefaultAsync();

				mcads.Add(new OnlyMainCategoryAllOutput
				{
					AdId = ad.AdId,
					UserId = ad.UserId,
					MCname = mainCategory.Mcname,
					SCname = subCategory.Scname,
					AdTitle = ad.AdTitle,
					AdDescription = ad.AdDescription,
					ContactName = ad.ContactName,
					ContactEmail = ad.ContactEmail,
					PhoneNumber = ad.PhoneNumber,
					Status = ad.Status,
					ImageUrl = adImage // Assign the fetched image URL
				});
			}

			return Ok(mcads);
		}







		//// ADMIN CONTROL CODE FROM BELOW

		[HttpPost("addNewMainCategories")]
        public async Task<ActionResult<MainCategoryDTO>> AddNewMainCategories(MainCategoryDTO addNewMainCat)
        {
            if (addNewMainCat == null)
            {
                return BadRequest("New Category Data is Null");
            }

            var normalizedCategoryName = addNewMainCat.Mcname.Replace(" ", "").ToLower();
            var existingCategory = await context.MainCategories.Where(c => c.Mcname.Replace(" ", "").ToLower() == normalizedCategoryName).FirstOrDefaultAsync();
            //    var existingCategory = await context.MainCategories
            //.FirstOrDefaultAsync(c => c.Mcname == addNewMainCat.Mcname);


            if (existingCategory != null)
            {
                return Conflict("Category already exists");
            }
            var adder = new MainCategory
            {
                Mcname = addNewMainCat.Mcname
            };
            await context.MainCategories.AddAsync(adder);
            await context.SaveChangesAsync();
            return Ok(addNewMainCat);
        }



        [HttpPost("addNewSubCategories")]
        public async Task<ActionResult<SubCategoryDTO>> AddNewSubCategories(SubCategoryDTO addNewSubCat)
        {
            if (addNewSubCat == null)
            {
                return BadRequest("New Sub Category Data is Null");
            }

            var normalizedMainCategoryName = addNewSubCat.MCName.Replace(" ", "").ToLower();
            var existingMainCategory = await context.MainCategories.Where(c => c.Mcname.Replace(" ", "").ToLower() == normalizedMainCategoryName).FirstOrDefaultAsync();
            //    var existingCategory = await context.MainCategories
            //.FirstOrDefaultAsync(c => c.Mcname == addNewMainCat.Mcname);


            if (existingMainCategory == null)
            {
                return Conflict("MainCategory doesnt exists");
            }

            var normalizedSubCategoryName = addNewSubCat.Scname.Replace(" ", "").ToLower();
            var existingSubCategory = await context.SubCategories.Where(x => x.Scname.Replace(" ", "").ToLower() == normalizedSubCategoryName && x.Mcid == existingMainCategory.Mcid).FirstOrDefaultAsync();


            if (existingSubCategory != null)
            {
                return Conflict("Sub Category already exists");
            }


            var adder = new SubCategory
            {
                Mcid = existingMainCategory.Mcid,
                Scname = addNewSubCat.Scname

            };
            await context.SubCategories.AddAsync(adder);
            await context.SaveChangesAsync();
            return Ok(addNewSubCat);
        }

        [HttpGet("ViewAllMainCategories")]
        public async Task<ActionResult<List<MainCategoryDTO>>> ViewMainCategories()
        {
            var data = await context.MainCategories.ToListAsync();
            return Ok(data);
        }

        [HttpGet("GetMainCategory/{id}")]
        public async Task<ActionResult<MainCategoryDTO>> GetMainCategory(int id)
        {
            var collector = await context.MainCategories.FindAsync(id);
            if (collector == null)
            {
                return NotFound();
            }

            var editinfocarrier = new MainCategoryDTO
            {
                Mcid = collector.Mcid,
                Mcname = collector.Mcname
            };
            return Ok(editinfocarrier);

        }

        [HttpGet("ShowAllAdsForAdmin")]
        public async Task<ActionResult<List<AdDTO>>> ShowAllAdsForAdmin()
        {
            var data = await context.Ads.ToListAsync();
            return Ok(data);
        }


        

        
        [HttpPut("editMainCategory/{id}")]
		public async Task<ActionResult<MainCategoryDTO>> EditMainCategory(int id, MainCategoryDTO mcDto)
		{
			if (id != mcDto.Mcid)
			{
				return BadRequest();
			}

			var existingMc = await context.MainCategories.FindAsync(id);

			if (existingMc == null)
			{
				return NotFound();
			}

			//var dependentAds = await context.Ads.Where(ad => ad.Mcname == existingMc.Mcname).ToListAsync();
			//await context.SaveChangesAsync();
			//var dependentSubCategories = await context.SubCategories.Where(Sc => Sc.Mcid == id).ToListAsync();
			//await context.SaveChangesAsync();
			// Map the properties from the DTO to the entity

			existingMc.Mcname = mcDto.Mcname;
			context.Entry(existingMc).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
			await context.SaveChangesAsync();
			return Ok(existingMc);

		}



		[HttpDelete("DeleteMainCategory/{id}")]
		public async Task<ActionResult<MainCategoryDTO>> DeleteMainCategory(int id)
		{
			var Ad = await context.Ads.ToListAsync();
			foreach (var ad in Ad)
			{
				if (ad.Mcid == id)
				{
					context.Ads.Remove(ad);
				}
			}
			var delMc = await context.MainCategories.FindAsync(id);
			if (delMc == null)
			{
				return NotFound();
			}

			context.MainCategories.Remove(delMc);
			
			await context.SaveChangesAsync();
			return Ok();

		}







        //[HttpPost("SearchAds")]
        //public async Task<ActionResult<IEnumerable<AdDTO>>> SearchAds([FromBody] TitleDTO titleDto)
        //{
        //    if (string.IsNullOrEmpty(titleDto?.Title))
        //    {
        //        return BadRequest("Title must not be empty.");
        //    }

        //    // Fetch the ads from the database
        //    var ads = await context.Ads
        //                          .Where(ad => EF.Functions.Like(ad.AdTitle, $"%{titleDto.Title}%")).ToListAsync();

        //    var adholder = new List<AdDTO>();
        //    foreach (var ad in ads)
        //    {
        //        var mainCatInfo = await context.MainCategories.FindAsync(ad.Mcid);
        //        var SubCatInfo = await context.SubCategories.FindAsync(ad.Scid);

        //        var temp = new AdDTO
        //        {
        //            AdId = ad.AdId,
        //            UserId = ad.UserId,
        //            Mcname = mainCatInfo?.Mcname,
        //            Scname = SubCatInfo?.Scname,
        //            AdTitle = ad.AdTitle,
        //            AdDescription = ad.AdDescription,
        //            ContactName = ad.ContactName,
        //            ContactEmail = ad.ContactEmail,
        //            PhoneNumber = ad.PhoneNumber,
        //            Status = ad.Status
        //        };
        //        adholder.Add(temp);
        //    }

        //    if (adholder == null || adholder.Count == 0)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(adholder);
        //}

        //public class TitleDTO
        //{
        //    public string Title { get; set; }
        //}


        [HttpPost("SearchAds")]
        public async Task<ActionResult<IEnumerable<AdDTO>>> SearchAds(TitleDTO titleDto)
        {
            if (titleDto.Title == null)
            {
                return BadRequest("Title must not be empty.");
            }

            // Fetch the ads from the database based on the title search
            var ads = await context.Ads
                                  .Where(ad => EF.Functions.Like(ad.AdTitle, $"%{titleDto.Title}%"))
                                  .ToListAsync();

            var adholder = new List<AdDTO>();

            foreach (var ad in ads)
            {
                var mainCatInfo = await context.MainCategories.FindAsync(ad.Mcid);
                var subCatInfo = await context.SubCategories.FindAsync(ad.Scid);

                // Fetch the latest image URL for the current ad
                var adImage = await context.AdImages
                                           .Where(ai => ai.AdId == ad.AdId)
                                           .OrderByDescending(ai => ai.ImageId) // Assuming there's an Id or timestamp column
                                           .Select(ai => ai.ImageUrl)
                                           .FirstOrDefaultAsync();

                var temp = new AdDTO
                {
                    AdId = ad.AdId,
                    UserId = ad.UserId,
                    Mcname = mainCatInfo?.Mcname,
                    Scname = subCatInfo?.Scname,
                    AdTitle = ad.AdTitle,
                    AdDescription = ad.AdDescription,
                    ContactName = ad.ContactName,
                    ContactEmail = ad.ContactEmail,
                    PhoneNumber = ad.PhoneNumber,
                    Status = ad.Status,
                    ImageUrl = adImage // Assign the fetched image URL
                };

                adholder.Add(temp);
            }

            if (!adholder.Any())
            {
                return NotFound();
            }

            return Ok(adholder);
        }



        public class TitleDTO
        {
            public string Title { get; set; }
        }


        [HttpPut("ChangeStatus/{id}")]
        public async Task<ActionResult<AdDTO>> ChangeStatus(int id, ChangeStatusDTO statusDto)
        {
            if (id != statusDto.AdId)
            {
                return BadRequest();
            }

            var existingAd = await context.Ads.FindAsync(id);

            if (existingAd == null)
            {
                return NotFound();
            }

            // Map the properties from the DTO to the entity

            existingAd.Status = statusDto.Status;
            context.Entry(existingAd).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();
            return Ok(existingAd);

        }

        [HttpGet("GetUserDetails/{id}")]
        public async Task<ActionResult<UserDTO>> GetUserDetails(int id)
        {
            var user = await context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            var userdetails = new UserDTO
            {
                UserId = user.UserId,
                FullName = user.FullName,
                Dob = user.Dob,
                Gender = user.Gender,
                EmailId = user.EmailId,
                City = user.City,
                State = user.State,
                PhoneNo = user.PhoneNo,
                Password = user.Password,
                UserType = user.UserType

            };

            return Ok(userdetails);

        }










        //[HttpPost("RegisterNewUser")]
        //public async Task<ActionResult<UserDTO>> Register(UserDTO userdetails)
        //{
        //    if (userdetails == null)
        //    {
        //        return BadRequest();
        //    }
        //    var checkuserexist = context.Users.Where(x => x.EmailId == userdetails.EmailId).FirstOrDefault();
        //    if (checkuserexist == null)
        //    {
        //        var adduserdetails = new User
        //        {
        //            FullName = userdetails.FullName,
        //            Dob = userdetails.Dob,
        //            Gender = userdetails.Gender,
        //            EmailId = userdetails.EmailId,
        //            City = userdetails.City,
        //            State = userdetails.State,
        //            PhoneNo = userdetails.PhoneNo,
        //            Password = userdetails.Password

        //        };
        //        if (ModelState.IsValid)
        //        {
        //            await context.Users.AddAsync(adduserdetails);
        //            await context.SaveChangesAsync();
        //            return Ok(userdetails);
        //        }
        //    }
        //    return BadRequest();
            
        //}





    }
}







