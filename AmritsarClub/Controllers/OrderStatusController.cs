using AmritsarClub.Filters;
using AmritsarClub.Models;
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

namespace KwalityCRM_Application.Controllers
{
    [CustomAuthenticationFilter]
    [Authorize]
    public class OrderStatusController : Controller
    {
        // GET: OrderStatus
        public ActionResult Order_Status()
        {
            return View();
        }

        public async Task<ActionResult> GetByGO(string GroupOrder)
        {
            var Username = "SampleUser";
            var PasswordHash = "SamplePasswordHash";
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            var logedinUser = (CustomIdentity)Session["customIdentity"];
            List<GroupList> UnitLists = new List<GroupList>();

            using (var client = new HttpClient())
            {
                var apiUrl = "https://103.80.32.71/testapi/Demo/GetGroupSearchByPartyList?partycode=" + logedinUser.UserID + "&groupcode=" + GroupOrder;
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        var unitsjson = await response.Content.ReadAsStringAsync();
                        UnitLists = JsonConvert.DeserializeObject<List<GroupList>>(unitsjson);
                        return Json(UnitLists);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Deserialization error: " + ex.Message);
                        return Json(new { result = "failed", error = "Deserialization error: " + ex.Message });
                    }
                }
                return Json(new { result = "failed" });
            }

        }

        public async Task<ActionResult> GetByWO(string WorkOrder)
        {
            var Username = "SampleUser";
            var PasswordHash = "SamplePasswordHash";
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            var logedinUser = (CustomIdentity)Session["customIdentity"];
            List<workList> UnitLists = new List<workList>();

            using (var client = new HttpClient())
            {
                var apiUrl = "https://103.80.32.71/testapi/Demo/GetWorkOrderSearchByPartyList?partycodes=" + logedinUser.UserID + "&workorderno=" + WorkOrder;


                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;


                // Send the GET request to the API
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        var unitsjson = await response.Content.ReadAsStringAsync();
                        UnitLists = JsonConvert.DeserializeObject<List<workList>>(unitsjson);
                        return Json(UnitLists);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Deserialization error: " + ex.Message);
                        return Json(new { result = "failed", error = "Deserialization error: " + ex.Message });
                    }
                }
                return Json(new { result = "failed" });
            }

        }

        public async Task<ActionResult> GetByBatch(string batch)
        {
            var Username = "SampleUser";
            var PasswordHash = "SamplePasswordHash";
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            var logedinUser = (CustomIdentity)Session["customIdentity"];
            List<workList> UnitLists = new List<workList>();

            using (var client = new HttpClient())
            {
                var apiUrl = "https://103.80.32.71/testapi/Demo/GetBatchNumberByPartyList?partycod=" + logedinUser.UserID + "&batchno=" + batch;
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        var unitsjson = await response.Content.ReadAsStringAsync();
                        UnitLists = JsonConvert.DeserializeObject<List<workList>>(unitsjson);
                        return Json(UnitLists);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Deserialization error: " + ex.Message);
                        return Json(new { result = "failed", error = "Deserialization error: " + ex.Message });
                    }
                }
                return Json(new { result = "failed" });
            }

        }

        public async Task<ActionResult> GetByCombi(string combi)
        {
            var Username = "SampleUser";
            var PasswordHash = "SamplePasswordHash";
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            var logedinUser = (CustomIdentity)Session["customIdentity"];
            List<workList> UnitLists = new List<workList>();

            using (var client = new HttpClient())
            {
                var apiUrl = "https://103.80.32.71/testapi/Demo/GetCombiPartyList?Party=" + logedinUser.UserID + "&Combi=" + combi;
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        var unitsjson = await response.Content.ReadAsStringAsync();
                        UnitLists = JsonConvert.DeserializeObject<List<workList>>(unitsjson);
                        return Json(UnitLists);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Deserialization error: " + ex.Message);
                        return Json(new { result = "failed", error = "Deserialization error: " + ex.Message });
                    }
                }
                return Json(new { result = "failed" });
            }

        }

        public async Task<ActionResult> GetSectionList()
        {
            var Username = "SampleUser";
            var PasswordHash = "SamplePasswordHash";
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            List<SectionList> SectionList = new List<SectionList>();
            using (var client = new HttpClient())
            {
                var apiUrl = "https://103.80.32.71/testapi/Demo/GetSectionName";

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        var dosejson = await response.Content.ReadAsStringAsync();

                        SectionList = JsonConvert.DeserializeObject<List<SectionList>>(dosejson);

                        return Json(SectionList);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Deserialization error: " + ex.Message);
                        return Json(new { result = "failed", error = "Deserialization error: " + ex.Message });
                    }
                }
                return Json(new { result = "failed" });
            }
        }

        public async Task<ActionResult> GetItemList(string Section)
        {
            var Username = "SampleUser";
            var PasswordHash = "SamplePasswordHash";
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            List<ItemList> SectionList = new List<ItemList>();
            using (var client = new HttpClient())
            {
                var apiUrl = "https://103.80.32.71/testapi/Demo/GetItemName?drugDosageForm=" + Section;

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        var dosejson = await response.Content.ReadAsStringAsync();

                        SectionList = JsonConvert.DeserializeObject<List<ItemList>>(dosejson);

                        return Json(SectionList);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Deserialization error: " + ex.Message);
                        return Json(new { result = "failed", error = "Deserialization error: " + ex.Message });
                    }
                }
                return Json(new { result = "failed" });
            }
        }

        public async Task<ActionResult> GetBySectionParty(string dsgform)
        {
            var Username = "SampleUser";
            var PasswordHash = "SamplePasswordHash";
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            var logedinUser = (CustomIdentity)Session["customIdentity"];
            List<workList> UnitLists = new List<workList>();

            using (var client = new HttpClient())
            {
                var apiUrl = "https://103.80.32.71/testapi/Demo/Get_SectionPartyList?PartyCodes=" + logedinUser.UserID + "&DSG_Forms=" + dsgform;


                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;


                // Send the GET request to the API
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        var unitsjson = await response.Content.ReadAsStringAsync();
                        UnitLists = JsonConvert.DeserializeObject<List<workList>>(unitsjson);
                        return Json(UnitLists);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Deserialization error: " + ex.Message);
                        return Json(new { result = "failed", error = "Deserialization error: " + ex.Message });
                    }
                }
                return Json(new { result = "failed" });
            }

        }

        public async Task<ActionResult> GetBySectionItemParty(string corrgrn, string dsgform)
        {
            var Username = "SampleUser";
            var PasswordHash = "SamplePasswordHash";
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            var logedinUser = (CustomIdentity)Session["customIdentity"];
            List<workList> UnitLists = new List<workList>();

            using (var client = new HttpClient())
            {
                var apiUrl = "https://103.80.32.71/testapi/Demo/GetDataByPartywiseSectionItem?PartyCode=" + logedinUser.UserID + "&DSG_Form=" + dsgform + "&Cor_Gen_Name=" + corrgrn;


                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;


                // Send the GET request to the API
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        var unitsjson = await response.Content.ReadAsStringAsync();
                        UnitLists = JsonConvert.DeserializeObject<List<workList>>(unitsjson);
                        return Json(UnitLists);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Deserialization error: " + ex.Message);
                        return Json(new { result = "failed", error = "Deserialization error: " + ex.Message });
                    }
                }
                return Json(new { result = "failed" });
            }

        }

        public async Task<ActionResult> GetByDateWiseSearch(string FromDate, string ToDate)
        {
            var Username = "SampleUser";
            var PasswordHash = "SamplePasswordHash";
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            var logedinUser = (CustomIdentity)Session["customIdentity"];
            List<workList> UnitLists = new List<workList>();

            using (var client = new HttpClient())
            {
                var apiUrl = "https://103.80.32.71/testapi/Demo/GetDateDataByPartyWise?partycodes=" + logedinUser.UserID + "&FromDate=" + FromDate + "&ToDate=" + ToDate;
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        var unitsjson = await response.Content.ReadAsStringAsync();
                        UnitLists = JsonConvert.DeserializeObject<List<workList>>(unitsjson);
                        return Json(UnitLists);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Deserialization error: " + ex.Message);
                        return Json(new { result = "failed", error = "Deserialization error: " + ex.Message });
                    }
                }
                return Json(new { result = "failed" });
            }

        }

        public async Task<ActionResult> GetBySectionItemPartyDateWiseSearch(string section, string item, string FromDate, string ToDate)
        {
            var Username = "SampleUser";
            var PasswordHash = "SamplePasswordHash";
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            var logedinUser = (CustomIdentity)Session["customIdentity"];
            List<workList> UnitLists = new List<workList>();

            using (var client = new HttpClient())
            {
                var apiUrl = "https://103.80.32.71/testapi/Demo/Get_SectionItemDateByPartyList?PartyCode=" + logedinUser.UserID + "&Cor_Gen_Name=" + item + "&DSG_Form=" + section + "&Fromdate=" + FromDate + "&DSG_Form=" + ToDate;
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        var unitsjson = await response.Content.ReadAsStringAsync();
                        UnitLists = JsonConvert.DeserializeObject<List<workList>>(unitsjson);
                        return Json(UnitLists);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Deserialization error: " + ex.Message);
                        return Json(new { result = "failed", error = "Deserialization error: " + ex.Message });
                    }
                }
                return Json(new { result = "failed" });
            }

        }

        public async Task<ActionResult> RMStatus(string Work_order)
        {
            var Username = "SampleUser";
            var PasswordHash = "SamplePasswordHash";
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            var logedinUser = (CustomIdentity)Session["customIdentity"];
            List<RM> UnitLists = new List<RM>();

            using (var client = new HttpClient())
            {
                var apiUrl = "https://103.80.32.71/testapi/Demo/Get_RMStatusList?Work_order_number=" + Work_order;
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        var unitsjson = await response.Content.ReadAsStringAsync();
                        UnitLists = JsonConvert.DeserializeObject<List<RM>>(unitsjson);
                        return Json(UnitLists);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Deserialization error: " + ex.Message);
                        return Json(new { result = "failed", error = "Deserialization error: " + ex.Message });
                    }
                }
                return Json(new { result = "failed" });
            }

        }

        public async Task<ActionResult> PMStatus(string PMWorkorder)
        {
            var Username = "SampleUser";
            var PasswordHash = "SamplePasswordHash";
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            var logedinUser = (CustomIdentity)Session["customIdentity"];
            List<PM> UnitLists = new List<PM>();

            using (var client = new HttpClient())
            {
                var apiUrl = "https://192.168.5.40/testapi/Demo/Get_PMStatusList?Work_order_numbering=" + PMWorkorder;
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        var unitsjson = await response.Content.ReadAsStringAsync();
                        UnitLists = JsonConvert.DeserializeObject<List<PM>>(unitsjson);
                        return Json(UnitLists);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Deserialization error: " + ex.Message);
                        return Json(new { result = "failed", error = "Deserialization error: " + ex.Message });
                    }
                }
                return Json(new { result = "failed" });
            }

        }

        public async Task<ActionResult> ProdStatus(string ProdWorkorder)
        {
            var Username = "SampleUser";
            var PasswordHash = "SamplePasswordHash";
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            var logedinUser = (CustomIdentity)Session["customIdentity"];
            List<ProductionStatus> UnitLists = new List<ProductionStatus>();

            using (var client = new HttpClient())
            {
                var apiUrl = "https://192.168.5.40/testapi/Demo/Get_ProductionStatusList?Work_order_numbers=" + ProdWorkorder;
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        var unitsjson = await response.Content.ReadAsStringAsync();
                        UnitLists = JsonConvert.DeserializeObject<List<ProductionStatus>>(unitsjson);
                        return Json(UnitLists);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Deserialization error: " + ex.Message);
                        return Json(new { result = "failed", error = "Deserialization error: " + ex.Message });
                    }
                }
                return Json(new { result = "failed" });
            }

        }

        public async Task<ActionResult> PackingStatus(string PackSWorkorder)
        {
            var Username = "SampleUser";
            var PasswordHash = "SamplePasswordHash";
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            var logedinUser = (CustomIdentity)Session["customIdentity"];
            List<PackingS> UnitLists = new List<PackingS>();

            using (var client = new HttpClient())
            {
                var apiUrl = "https://192.168.5.40/testapi/Demo/Get_PackingStatusList?Work_order_numberd=" + PackSWorkorder;
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        var unitsjson = await response.Content.ReadAsStringAsync();
                        UnitLists = JsonConvert.DeserializeObject<List<PackingS>>(unitsjson);
                        return Json(UnitLists);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Deserialization error: " + ex.Message);
                        return Json(new { result = "failed", error = "Deserialization error: " + ex.Message });
                    }
                }
                return Json(new { result = "failed" });
            }

        }

        public async Task<ActionResult> InvoiceStatus(string InvoiceWorkorder)
        {
            var Username = "SampleUser";
            var PasswordHash = "SamplePasswordHash";
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            var logedinUser = (CustomIdentity)Session["customIdentity"];
            List<Invoice> UnitLists = new List<Invoice>();

            using (var client = new HttpClient())
            {
                var apiUrl = "https://192.168.5.40/testapi/Demo/Get_InvoiceStatusList?Work_order=" + InvoiceWorkorder;
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        var unitsjson = await response.Content.ReadAsStringAsync();
                        UnitLists = JsonConvert.DeserializeObject<List<Invoice>>(unitsjson);
                        return Json(UnitLists);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Deserialization error: " + ex.Message);
                        return Json(new { result = "failed", error = "Deserialization error: " + ex.Message });
                    }
                }
                return Json(new { result = "failed" });
            }

        }

        public async Task<ActionResult> PaymentStatus(string PaymentWorkorder)
        {
            var Username = "SampleUser";
            var PasswordHash = "SamplePasswordHash";
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            var logedinUser = (CustomIdentity)Session["customIdentity"];
            List<Invoice> UnitLists = new List<Invoice>();

            using (var client = new HttpClient())
            {
                var apiUrl = "https://192.168.5.40/testapi/Demo/Get_PaymentStatusList?Works_orders=" + PaymentWorkorder;
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        var unitsjson = await response.Content.ReadAsStringAsync();
                        UnitLists = JsonConvert.DeserializeObject<List<Invoice>>(unitsjson);
                        return Json(UnitLists);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Deserialization error: " + ex.Message);
                        return Json(new { result = "failed", error = "Deserialization error: " + ex.Message });
                    }
                }
                return Json(new { result = "failed" });
            }

        }

        public async Task<ActionResult> DisplayPartyName()
        {
            var Username = "SampleUser";
            var PasswordHash = "SamplePasswordHash";
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            var logedinUser = (CustomIdentity)Session["customIdentity"];
            List<UserDetails> UnitLists = new List<UserDetails>();

            using (var client = new HttpClient())
            {
                var apiUrl = "https://192.168.5.40/testapi/Demo/UserProfile?partycode=" + logedinUser.UserID;


                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;


                // Send the GET request to the API
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        var unitsjson = await response.Content.ReadAsStringAsync();
                        UnitLists = JsonConvert.DeserializeObject<List<UserDetails>>(unitsjson);
                        return Json(UnitLists);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Deserialization error: " + ex.Message);
                        return Json(new { result = "failed", error = "Deserialization error: " + ex.Message });
                    }
                }
                return Json(new { result = "failed" });
            }

        }



    }
}