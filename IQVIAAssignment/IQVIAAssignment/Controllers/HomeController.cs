using IQVIAAssignment.Models;
using IQVIAAssignment.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace IQVIAAssignment.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }
        

        [HttpPost]
        public async Task<ActionResult> Index(FormCollection data)
        {
            var fromdate = data["txtFromDate"];
            var enddate = data["txtToDate"];
            TempData["StartDate"] = fromdate;
            TempData["EndDate"] = enddate;
            
            return RedirectToAction("GetTweet");
        }

        public async Task<ActionResult> GetTweet()
        {
            List<Tweet> list = new List<Tweet>();
            List<TweetDetailsViewModel> tweets = new List<TweetDetailsViewModel>();
            try
            {
                string Startdate = TempData["StartDate"].ToString();
                string Enddate = TempData["EndDate"].ToString();

                TempData.Keep();

                
                
                string Baseurl = System.Configuration.ConfigurationManager.AppSettings["APIUrl"];
                using (var client = new HttpClient())
                {
                    //Passing service base url  
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //Sending request to find web api REST service resource GetTweets using HttpClient  
                    HttpResponseMessage Res = await client.GetAsync("api/v1/Tweets?startDate=" + Startdate + "&endDate=" + Enddate);

                    //Checking the response is successful or not which is sent using HttpClient  
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;

                        //Deserializing the response recieved from web api and storing into the Employee list  
                        list = JsonConvert.DeserializeObject<List<Tweet>>(EmpResponse);
                        list = list.OrderBy(o => o.stamp).ToList();
                        //var result = list.Distinct(new ItemEqualityComparer());
                        var result = list.Distinct();
                        foreach (var item in result)
                        {
                            TweetDetailsViewModel objTweet = new TweetDetailsViewModel();
                            objTweet.Year = DateTime.Parse(item.stamp.ToString()).Year;
                            objTweet.Month = DateTime.Parse(item.stamp.ToString()).Month;
                            objTweet.Day = DateTime.Parse(item.stamp.ToString()).Day;
                            objTweet.Description = item.text;
                            tweets.Add(objTweet);
                        }
                    }

                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("",ex.Message);
            }
            return View(tweets);
        }
       
    }
}