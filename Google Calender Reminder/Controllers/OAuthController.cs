using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RestSharp;


namespace Google_Calender_Reminder.Controllers
{
    public class OAuthController : Controller
    {
        public void Callback(string code, string error, string state)
        {
            if(string.IsNullOrWhiteSpace(error)) {
                this.GetTokens(code);
                
            }
        }
        public  ActionResult GetTokens(string code)
        {
            var tokenFile = "C:\\Users\\ankit\\source\\repos\\Google Calender Reminder\\Files\\tokens.json";
            var credentialsFile = "C:\\Users\\ankit\\source\\repos\\Google Calender Reminder\\Files\\credentials.json";
            JObject? credentials  = JObject.Parse(System.IO.File.ReadAllText(credentialsFile));
            string client_id = "814666712708-2rvi3f0e7t4dohvrkgbqjg2pjcog0dfh.apps.googleusercontent.com";
            string client_secret = "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";
            RestClient restClient = new RestClient("https://oauth2.googleapis.com/token");
            RestRequest request = new RestRequest();
            request.AddQueryParameter("client_id",client_id)
                   .AddQueryParameter("client_secret", client_secret)
                   .AddQueryParameter("code", code)
                   .AddQueryParameter("grant_type", "authorization_code")
                   .AddQueryParameter("redirect_uri", "https://localhost:7058/oauth/callback");
            var response = restClient.Post(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                System.IO.File.WriteAllText(tokenFile, response.Content);
               return RedirectToAction("Index", "Home");
            }



            return View("Error");
        }
    }
}
