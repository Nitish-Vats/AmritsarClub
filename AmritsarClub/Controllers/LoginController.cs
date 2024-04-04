using AmritsarClub.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AmritsarClub.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        //public ActionResult Login(LoginViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Authenticate the user
        //        string isAuthenticated = AuthenticateUser(model.UserId, model.Password).Result;

        //        if (isAuthenticated == "1" || isAuthenticated == "2" || isAuthenticated == "3" || isAuthenticated == "4")
        //        {
        //            ViewBag.LoginResult = isAuthenticated;
        //            return RedirectToAction("dashboard", "Dashboard");
        //        }

        //        return View();
        //    }
        //    return View();
        //}

        //private async Task<int> AuthenticateUser(string userId, string password)
        //{
        //    var Username = "SampleUser";
        //    var PasswordHash = "SamplePasswordHash";
        //    var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
        //    int loginResult = 0;

        //    using (var client = new HttpClient())
        //    {
        //        var apiUrl = "https://192.168.5.40/testapi/Demo/DashboardLogin";
        //        var data = new { UserName = userId, password = password };
        //        var json = JsonConvert.SerializeObject(data);
        //        HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

               
        //        // Replace "your-api-url" with the actual API URL 192.168.5.40
        //        // var apiUrl = "https://103.80.32.71/testapi/Demo/DashboardLogin?UserName=" + userId + "&password=" + password;

        //        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Credentials);
        //        ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;


        //        // Send the GET request to the API
        //       //var response = await client.GetAsync(apiUrl);
        //        var response = client.PostAsync(apiUrl, content).Result;

        //        if (response.IsSuccessStatusCode)
        //        {
        //            //Storing the response details recieved from web api   
        //            var loginUser = await response.Content.ReadAsStringAsync();

        //            //Deserializing the response recieved from web api and storing into the Employee list  
        //            loginResult = JsonConvert.DeserializeObject<int>(loginUser);
        //            var customIdentity = new CustomIdentity
        //            {
        //                UserID = userId,
        //                //Name = loggedInUsername,
        //                LoginResult = loginResult
        //            };
        //            HttpContext.Session.Add("customIdentity", customIdentity);
        //        }


        //    }
        //    return loginResult;




        //}
        private async Task<string> AuthenticateUser(string userId, string password)
        {
            var Username = "SampleUser";
            var PasswordHash = "SamplePasswordHash";
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            string loginResult = "";
            List<Inquiry> inquiries = new List<Inquiry>();
            using (var client = new HttpClient())
            {
                // Replace "your-api-url" with the actual API URL
                var apiUrl = "https://103.80.32.71/testapi/Demo/GetDataByPartyEnqCodeList?Partycodes=" + userId;

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;


                // Send the GET request to the API
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var InquiryData = await response.Content.ReadAsStringAsync();

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    inquiries = JsonConvert.DeserializeObject<List<Inquiry>>(InquiryData);

                }


            }
            using (var client = new HttpClient())
            {
              //  var apiUrl = "https://192.168.5.40/testapi/Demo/DashboardLogin";
                var apiUrl = "https://103.80.32.71/testapi/Demo/DashboardLogin?UserName=" + userId + "&password=" + password;
                var data = new { UserName = userId, password = password };
                var json = JsonConvert.SerializeObject(data);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

                var response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    var loginUser = await response.Content.ReadAsStringAsync();
                    loginResult = JsonConvert.DeserializeObject<string>(loginUser);

                    var customIdentity = new CustomIdentity
                    {
                        UserID = userId,
                        LoginResult = loginResult
                    };
                    HttpContext.Session.Add("customIdentity", customIdentity);
                }
            }

            return loginResult;
        }
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Authenticate the user
                //int isAuthenticated = AuthenticateUser(model.UserId, model.Password).Result;
                var Username = "SampleUser";
                var PasswordHash = "SamplePasswordHash";
                var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
                string loginResult = "";
                using (var client = new HttpClient())
                {
                    //  var apiUrl = "https://192.168.5.40/testapi/Demo/DashboardLogin";
                    var apiUrl = "https://192.168.5.40/testapi/Demo/DashboardLogin?UserName=" + model.UserId + "&password=" + model.Password;
                    var data = new { UserName = model.UserId, password = model.Password };
                    var json = JsonConvert.SerializeObject(data);
                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Credentials);
                    ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

                    var response = await client.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var loginUser = await response.Content.ReadAsStringAsync();
                        var logindata = JsonConvert.DeserializeObject<CustomIdentity>(loginUser);
                        loginResult = logindata.LoginResult;

                        var customIdentity = new CustomIdentity
                        {
                            UserID = model.UserId,
                            LoginResult = loginResult
                        };

                        Session["customIdentity"] = customIdentity;
                       // HttpContext.Session.Add("customIdentity", customIdentity);

                    }
                }
                if (loginResult == "1" || loginResult == "2" || loginResult == "3" || loginResult == "4")
                {
                    ViewBag.LoginResult = loginResult;
                    FormsAuthentication.SetAuthCookie(model.UserId, false);
                    //GenerateTicket(model.UserId);
                    return RedirectToAction("dashboard", "Dashboard");
                }
                ViewBag.Message = "Wrong UserId or Password";
                return View();
            }
            return View();
        }

        //private void GenerateTicket(string userId)
        //{
        //    var claims = new List<Claim>
        //   {
        //       new Claim(ClaimTypes.UserData,userId)
        //   };
        //    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        //    var principal = new ClaimsPrincipal(identity);
        //    var properties = new AuthenticationProperties
        //    {
        //        IsPersistent = true,
        //        ExpiresUtc = DateTime.UtcNow.AddMinutes(60)
        //    };
           
        //    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, properties);
        //}

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}