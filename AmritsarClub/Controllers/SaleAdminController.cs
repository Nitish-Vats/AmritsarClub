using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AmritsarClub.Models;
using AmritsarClub.Filters;

namespace KwalityCRM.Controllers
{
    [CustomAuthenticationFilter]
    [Authorize]
    public class SaleAdminController : Controller
    {
        // GET: SaleAdmin
        public ActionResult AdminPage()
        {
            ViewBag.Message = "Admin";
            return View();
        }
        public ActionResult Quotation(string id = "")
        {
            ViewBag.Message = "Quotation";
            return View();
        }

        public async Task<ActionResult> ItemListQuotation(int page = 1, int pageSize = 2, string id = "")
        {
            var products = await GetProducts(id);

            ViewBag.patyEnqCode = id;

            if (page < 1)
                page = 1;

            var query = products.AsQueryable();
            int totalRecords = query.Count();
            var data = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            Pager pager = new Pager(totalRecords, page, pageSize, "SaleAdmin", "ItemListQuotation");
            ViewBag.Pager = pager;
            return View(data);

        }


        private async Task<List<ItemQuotation>> GetProducts(string id)
        {
            List<ItemQuotation> products = new List<ItemQuotation>();
            var Username = "SampleUser";
            var PasswordHash = "SamplePasswordHash";
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));

            using (var client = new HttpClient())
            {
                // Replace "your-api-url" with the actual API URL
                var apiUrl = "https://192.168.5.40/testapi/Demo/Get_DataByPartyEnqList?PartyEnqCode=" + id;

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;


                // Send the GET request to the API
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var ProductData = await response.Content.ReadAsStringAsync();

                    //Deserializing the response recieved from web api and storing into the Product list  
                    products = JsonConvert.DeserializeObject<List<ItemQuotation>>(ProductData);
                }
            }
            foreach (var item in products)
            {
                if (item.Product_Image != Array.Empty<byte>())
                {
                    item.Imagedata = Convert.ToBase64String(item.Product_Image);
                }
            }

            return products;
        }
        public async Task<ActionResult> Inquiry_Quotation(string SearchText = "", int page = 1, int pageSize = 4, string selectedValue = "")
        {
            var Username = "SampleUser";
            var PasswordHash = "SamplePasswordHash";
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));

            //if (userID != null)
            //{
            //    Session["UserID"] = userID;
            //    ViewBag.UserID = userID;
            //}



            if (page < 1)
                page = 1;

            List<Inquiry> inquiries = new List<Inquiry>();
            using (var client = new HttpClient())
            {
                var apiUrl = "";
                if (selectedValue == "All" || selectedValue == "")
                {
                    apiUrl = "https://192.168.5.40/testapi/Demo/AllEnquryList";
                }
                else if (selectedValue == "Approved")
                {
                    apiUrl = "https://192.168.5.40/testapi/Demo/ApprovedList";
                }
                else if (selectedValue == "Pending")
                {
                    apiUrl = "https://192.168.5.40/testapi/Demo/PendingList";
                }



                // Replace "your-api-url" with the actual API URL
                //var apiUrl = "https://103.80.32.71/testapi/Demo/GetDataByPartyEnqCodeList?Partycodes=" + userID;

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

            var query = inquiries.AsQueryable();
            if (!string.IsNullOrEmpty(SearchText))
            {
                query = inquiries.Where(p => p.Party_Enq_Code.Contains(SearchText)).AsQueryable();
            }
            int totalRecords = query.Count();
            var data = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            Pager pager = new Pager(totalRecords, page, pageSize, "SaleAdmin", "Inquiry_Quotation");
            ViewBag.Pager = pager;
            return View(data);
        }

        public async Task<ActionResult> GetCorGenName(string DosForm)
        {
            var Username = "SampleUser";
            var PasswordHash = "SamplePasswordHash";
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            //var logedinUser = (CustomIdentity)Session["customIdentity"];
            List<CorGenName> CorGenNameList = new List<CorGenName>();
            //string EnqCode = "";
            using (var client = new HttpClient())
            {
                var apiUrl = "https://192.168.5.40/testapi/Demo/GetDataByCorGenNameList?CorGenName="+ DosForm;

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;


                // Send the GET request to the API
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var dosejson = await response.Content.ReadAsStringAsync();

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    CorGenNameList = JsonConvert.DeserializeObject<List<CorGenName>>(dosejson);
                    // ViewBag.country = AirPortList;
                    return Json(CorGenNameList);
                }
                return Json(new { result = "failed" });
            }
        }

        public async Task<ActionResult> GetPackType(string DosForm)
        {
            var Username = "SampleUser";
            var PasswordHash = "SamplePasswordHash";
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            //var logedinUser = (CustomIdentity)Session["customIdentity"];
            List<PackType> PackList = new List<PackType>();
            //string EnqCode = "";
            using (var client = new HttpClient())
            {
                var apiUrl = "https://192.168.5.40/testapi/Demo/GetDataByPackingTypeList?DsgForm=" + DosForm;

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;


                // Send the GET request to the API
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var dosejson = await response.Content.ReadAsStringAsync();

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    PackList = JsonConvert.DeserializeObject<List<PackType>>(dosejson);
                    // ViewBag.country = AirPortList;
                    return Json(PackList);
                }
                return Json(new { result = "failed" });
            }
        }

        public async Task<ActionResult> GetDescription(string WorkOrder)
        {
            var Username = "SampleUser";
            var PasswordHash = "SamplePasswordHash";
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            //var logedinUser = (CustomIdentity)Session["customIdentity"];
            AssignDescription description = new AssignDescription();
            //string EnqCode = "";
            using (var client = new HttpClient())
            {
                var apiUrl = "https://192.168.5.40/testapi/Demo/PackingListByWorkOrder?Work=" + WorkOrder;

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;


                // Send the GET request to the API
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var cost = await response.Content.ReadAsStringAsync();

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    description = JsonConvert.DeserializeObject<AssignDescription>(cost);
                    // ViewBag.country = AirPortList;
                    return Json(description);
                }
                return Json(new { result = "failed" });
            }
        }



        public async Task<ActionResult> GetPartyListDescription(string GenCode)
        {
            var Username = "SampleUser";
            var PasswordHash = "SamplePasswordHash";
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            //var logedinUser = (CustomIdentity)Session["customIdentity"];
            List<PartyListforDescription> description = new List<PartyListforDescription>();
            //string EnqCode = "";
            using (var client = new HttpClient())
            {
                var apiUrl = "https://192.168.5.40/testapi/Demo/GetDataBygencodes?gencode=" + GenCode;

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;


                // Send the GET request to the API
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var cost = await response.Content.ReadAsStringAsync();

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    description = JsonConvert.DeserializeObject<List<PartyListforDescription>>(cost);
                    // ViewBag.country = AirPortList;
                    return Json(description);
                }
                return Json(new { result = "failed" });
            }
        }


        public async Task<ActionResult> GetWOListtoAssign(string party_code,string dsg_form,string gen_code)
        {
            var Username = "SampleUser";
            var PasswordHash = "SamplePasswordHash";
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            //var logedinUser = (CustomIdentity)Session["customIdentity"];
            List<DescriptionWO> description = new List<DescriptionWO>();
            //string EnqCode = "";
            using (var client = new HttpClient())
            {
                var apiUrl = "https://192.168.5.40/testapi/Demo/Get_DataByGenCodeLIst?party_code=" + party_code + "&dsg_form=" + dsg_form + "&gen_code=" + gen_code;

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;


                // Send the GET request to the API
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var cost = await response.Content.ReadAsStringAsync();

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    description = JsonConvert.DeserializeObject<List<DescriptionWO>>(cost);
                    // ViewBag.country = AirPortList;
                    return Json(description);
                }
                return Json(new { result = "failed" });
            }
        }



        public async Task<ActionResult> GetCosting(string WorkOrder)
        {
            var Username = "SampleUser";
            var PasswordHash = "SamplePasswordHash";
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            //var logedinUser = (CustomIdentity)Session["customIdentity"];
            CostingResult costing= new CostingResult();
            //string EnqCode = "";
            using (var client = new HttpClient())
            {
                var apiUrl = "https://103.80.32.71/testapi/Demo/Costing?WorkOrder=" + WorkOrder;

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;


                // Send the GET request to the API
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var cost = await response.Content.ReadAsStringAsync();

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    costing = JsonConvert.DeserializeObject<CostingResult>(cost);
                    // ViewBag.country = AirPortList;
                    return Json(costing);
                }
                return Json(new { result = "failed" });
            }
        }


        public async Task<ActionResult> UpdateItem(ItemQuotation model)
        {
            var Username = "SampleUser";
            var PasswordHash = "SamplePasswordHash";
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            string json = JsonConvert.SerializeObject(model);

            using (var client = new HttpClient())
            {
                var apiUrl = "https://192.168.5.40/testapi/Demo/UpdateInquiryItem";

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

                var response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    // Data sent successfully
                    return Json(new { success = true, message = "Row updated successfully" });
                }
                else
                {
                    // Handle error
                    return Json("Failure");
                }
            }

           
        }


    }
}
