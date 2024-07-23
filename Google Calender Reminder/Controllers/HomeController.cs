using Google_Calender_Reminder.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using RestSharp;
using System.Diagnostics;
using System.Net;
using Event = Google_Calender_Reminder.Models.Event;

namespace Google_Calender_Reminder.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public ActionResult OauthRedirect()
        {
            var client_id = "814666712708-2rvi3f0e7t4dohvrkgbqjg2pjcog0dfh.apps.googleusercontent.com";
            var screte = "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";
            var redirectUrl = "https://accounts.google.com/o/oauth2/v2/auth?" +
                              "scope=https://www.googleapis.com/auth/calendar+https://www.googleapis.com/auth/calendar.events&" +
                              "access_type=online&" +
                              "include_granted_scopes=true&" +
                              "response_type=code&" +
                              "state=hellothere&" +
                              "redirect_uri=https://localhost:7058/oauth/callback&" +
                              "client_id=" + client_id;
            Console.WriteLine(redirectUrl);
            return Redirect(redirectUrl);


        }


        [HttpPost]
        public ActionResult AddEvent(Event CalenderEvent )
        {
            // getting the token details from json file and this is temporary setup
            var tokenFile = "C:\\Users\\ankit\\source\\repos\\Google Calender Reminder\\Files\\tokens.json";
            JObject? tokens = JObject.Parse(System.IO.File.ReadAllText(tokenFile));

            RestClient client = new RestClient("https://www.googleapis.com/calendar/v3/calendars/primary/events");
            RestRequest request = new RestRequest();

            

            CalenderEvent.Start.DateTime=DateTime.Parse(CalenderEvent.Start.DateTime).ToString("yyyy-MM-dd'T'HH:mm:ss.fff");
            CalenderEvent.End.DateTime = DateTime.Parse(CalenderEvent.End.DateTime).ToString("yyyy-MM-dd'T'HH:mm:ss.fff");

            var model = JsonConvert.SerializeObject(CalenderEvent, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
            //later we can put this key into appsetting.json file
            request.AddQueryParameter("key", "AIzaSyAkRPk0Da0B_3bY6lH36g_TP0o4fH5w9Uk");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + tokens["access_token"]);
            request.AddHeader("Accept", "application/json");
            request.AddParameter("application/json", model, ParameterType.RequestBody);
            var response = client.Post(request);

            if(response.StatusCode==HttpStatusCode.OK)
            {
                //here we can add logic to add details to database

                
                return RedirectToAction("EventDetail", "Home", new { Status = "success" });
            }


            return View("Index");
        }
    

public IActionResult EventDetail(string status)
        {
            if(status == "success")
            {
                return View("EventDetail");
            }
            return View("Error");
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
