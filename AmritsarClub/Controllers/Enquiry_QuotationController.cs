using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AmritsarClub.Models;
using AmritsarClub.Filters;

namespace AmritsarClub.Controllers
{
    [CustomAuthenticationFilter]
    [Authorize]
    public class Enquiry_QuotationController : Controller
    {
        // GET: Enquiry_Quotation
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> Inquiry()
        {
            var Username = "SampleUser";
            var PasswordHash = "SamplePasswordHash";
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            var logedinUser = (CustomIdentity)Session["customIdentity"];
            
            string inq = "";
            using (var client = new HttpClient())
            {
                // Replace "your-api-url" with the actual API URL
             
                var apiUrl = "https://192.168.5.40/testapi/Demo/GenerateEnqCode?Party=" + logedinUser.UserID;

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;


                // Send the GET request to the API
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var InquiryData = await response.Content.ReadAsStringAsync();

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    inq = JsonConvert.DeserializeObject<string>(InquiryData);
                    ViewBag.PartyEnqCode = inq;

                }


            }
            List<CountryAirSea> country = new List<CountryAirSea>();
            using (var client = new HttpClient())
            {
                // Replace "your-api-url" with the actual API URL

                var apiUrl = "https://192.168.5.40/testapi/Demo/GetddlCountryList";

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;


                // Send the GET request to the API
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var CountryList = await response.Content.ReadAsStringAsync();

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    country = JsonConvert.DeserializeObject<List<CountryAirSea>>(CountryList);
                    ViewBag.country = country;

                }


            }




            return View();
        }
        public async Task<ActionResult> InquiryList(string SearchText = "", int page = 1, int pageSize = 4, string userID = "")
        {
            var Username = "SampleUser";
            var PasswordHash = "SamplePasswordHash";
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            var logedinUser = (CustomIdentity)Session["customIdentity"];
            if (userID != "")
            {
                Session["UserID"] = userID;
                ViewBag.UserID = userID;
            }
            else
            {
                userID = logedinUser.UserID;
            }



            if (page < 1)
                page = 1;

            List<Inquiry> inquiries = new List<Inquiry>();
            using (var client = new HttpClient())
            {
                // Replace "your-api-url" with the actual API URL
                var apiUrl = "https://192.168.5.40/testapi/Demo/GetDataByPartyEnqCodeList?Partycodes=" + userID;

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
            Pager pager = new Pager(totalRecords, page, pageSize, "Enquiry_Quotation", "InquiryList");
            ViewBag.Pager = pager;
            return View(data);
        }


        public async Task<ActionResult> InquiryItemList(/*string SearchText = "", int page = 1, int pageSize = 4, */string id = "")
        {
            var products = await GetProducts(id);

            ViewBag.patyEnqCode = id;
            //var query = products.AsQueryable();
            return View(products);
        }

        private async Task<List<Product>> GetProducts(string id)
        {

            List<Product> products = new List<Product>();
            var Username = "SampleUser";
            var PasswordHash = "SamplePasswordHash";
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));

            using (var client = new HttpClient())
            {
                // Replace "your-api-url" with the actual API URL
                var apiUrl = "https://192.168.5.40/testapi/Demo/GetdatabtPartyEnqList?PartyEnq=" + id;

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;


                // Send the GET request to the API
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var ProductData = await response.Content.ReadAsStringAsync();

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    products = JsonConvert.DeserializeObject<List<Product>>(ProductData);
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


        public async Task<ActionResult> ExportToExcel(string id)
        {
            // Get your table data here (for example, from a database)
            var tableData = await GetProducts(id);

            // Create a new Excel package
            using (var package = new ExcelPackage())
            {
                // Add a new worksheet to the package
                var worksheet = package.Workbook.Worksheets.Add("Sheet1");

                // Set the column headers
                worksheet.Cells["A1"].Value = "S/N";
                worksheet.Cells["B1"].Value = "Date";

                worksheet.Cells["C1"].Value = "Inquiry Status";
                worksheet.Cells["D1"].Value = "Item Name";

                worksheet.Cells["E1"].Value = "Dose Form";
                worksheet.Cells["F1"].Value = "Unit";

                worksheet.Cells["G1"].Value = "Size";
                worksheet.Cells["H1"].Value = "Qty. Per Pack";
                worksheet.Cells["I1"].Value = "No of Pack";
                worksheet.Cells["J1"].Value = "Total Unit";
                worksheet.Cells["K1"].Value = "Image";


                var hborder = worksheet.Cells[1, 1, 1, 11].Style.Border;
                hborder.Top.Style = hborder.Left.Style = hborder.Bottom.Style = hborder.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                // Add more columns if needed

                // Add the table data to the worksheet
                //foreach (var item in tableData)
                //{
                //    worksheet.Cells[row, col++].Value = item.Property1;
                //    worksheet.Cells[row, col++].Value = item.Property2;
                //    // ... add other properties

                //    row++;
                //    col = 1; // Reset column for next row
                //}


                for (int i = 0; i < tableData.Count(); i++)
                {

                    worksheet.Cells[i + 2, 1].Value = i + 1;
                    worksheet.Cells[i + 2, 2].Value = tableData[i].date;
                    worksheet.Cells[i + 2, 2].Style.Numberformat.Format = "yyyy-mm-dd hh:mm:ss";
                    worksheet.Cells[i + 2, 3].Value = tableData[i].Enquiry_Status;

                    worksheet.Cells[i + 2, 4].Value = tableData[i].Party_item_name;
                    worksheet.Cells[i + 2, 5].Value = tableData[i].dsg_form;

                    worksheet.Cells[i + 2, 6].Value = tableData[i].unit;
                    worksheet.Cells[i + 2, 7].Value = tableData[i].Size;

                    worksheet.Cells[i + 2, 8].Value = tableData[i].Qtydosg_unit_perpack;
                    worksheet.Cells[i + 2, 9].Value = tableData[i].No_of_pack;

                    worksheet.Cells[i + 2, 10].Value = tableData[i].Total_unit;
                    // worksheet.Cells[i + 2, 11].Value = tableData[i].Imagedata;


                    var imageCell = worksheet.Cells[i + 2, 11];
                    imageCell.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    imageCell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.White); // Set background color to white
                    imageCell.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin); // Add border
                    imageCell.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    imageCell.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    if (!string.IsNullOrEmpty(tableData[i].Imagedata))
                    {
                        byte[] imageBytes = Convert.FromBase64String(tableData[i].Imagedata);
                        var image = Image.FromStream(new MemoryStream(imageBytes));

                        //worksheet.Row(i + 2).Height = image.Height / 15;
                        var picture = worksheet.Drawings.AddPicture($"image_{i}", new MemoryStream(imageBytes));
                        picture.SetSize(100, 100);
                        picture.SetPosition(i + 1, 0, 10, 0);
                    }
                    //worksheet.Cells[i + 2, 1, i + 2, 11].Style.WrapText = true;
                    worksheet.Row(i + 2).Height = 90;
                    var border = worksheet.Cells[i + 2, 1, i + 2, 11].Style.Border;
                    border.Top.Style = border.Left.Style = border.Bottom.Style = border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                    worksheet.Cells[i + 2, 1, i + 2, 11].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    worksheet.Cells[i + 2, 1, i + 2, 11].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    // Add more columns if needed
                }

                // Apply wrap text to all cells in the table



                // Auto fit columns for better readability
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                // Save the Excel file
                var memoryStream = new MemoryStream();
                package.SaveAs(memoryStream);
                memoryStream.Position = 0;

                // Set the response content type
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                // Set the content disposition header
                Response.AddHeader("content-disposition", "attachment;  filename=Inquiry_Item_List.xlsx");

                // Write the Excel file to the response
                memoryStream.CopyTo(Response.OutputStream);

                // Return the result
                return new EmptyResult();
            }
        }

        public async Task<ActionResult> GetAirPort(string country)
        {
            var Username = "SampleUser";
            var PasswordHash = "SamplePasswordHash";
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            List<CountryAirSea> AirPortList = new List<CountryAirSea>();
            using (var client = new HttpClient())
            {
                var apiUrl = "https://192.168.5.40/testapi/Demo/ddlChangedAirportList?coun_code=" + country;

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;


                // Send the GET request to the API
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var AirList = await response.Content.ReadAsStringAsync();

                    //Deserializing the response recieved from web api and storing into the Employee list  
                   AirPortList = JsonConvert.DeserializeObject<List<CountryAirSea>>(AirList);
                   // ViewBag.country = AirPortList;
                    return Json(AirPortList);
                }
                return Json(new { result = "failed"});
            }
        }



        public async Task<ActionResult> GetSeaPort(string country)
        {
            var Username = "SampleUser";
            var PasswordHash = "SamplePasswordHash";
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            List<CountryAirSea> SeaPortList = new List<CountryAirSea>();
            using (var client = new HttpClient())
            {
                var apiUrl = "https://192.168.5.40/testapi/Demo/GetddlChangedSeaportList?coun_code=" + country;

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;


                // Send the GET request to the API
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var SeaList = await response.Content.ReadAsStringAsync();

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    SeaPortList = JsonConvert.DeserializeObject<List<CountryAirSea>>(SeaList);
                    // ViewBag.country = AirPortList;
                    return Json(SeaPortList);
                }
                return Json(new { result = "failed" });
            }
        }


        public async Task<ActionResult> GetEnqCode()
        {
            var Username = "SampleUser";
            var PasswordHash = "SamplePasswordHash";
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            var logedinUser = (CustomIdentity)Session["customIdentity"];
            //List<CountryAirSea> SeaPortList = new List<CountryAirSea>();
            string EnqCode = "";
            using (var client = new HttpClient())
            {
                var apiUrl = "https://192.168.5.40/testapi/Demo/EnqCode?Party=" + logedinUser.UserID;

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;


                // Send the GET request to the API
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var SeaList = await response.Content.ReadAsStringAsync();

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    EnqCode = JsonConvert.DeserializeObject<string>(SeaList);
                    // ViewBag.country = AirPortList;
                    return Json(EnqCode);
                }
                return Json(new { result = "failed" });
            }
        }


        public async Task<ActionResult> GetUnits()
        {
            var Username = "SampleUser";
            var PasswordHash = "SamplePasswordHash";
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            //var logedinUser = (CustomIdentity)Session["customIdentity"];
            List<UnitsViewModel> UnitList = new List<UnitsViewModel>();
            //string EnqCode = "";
            using (var client = new HttpClient())
            {
                var apiUrl = "https://192.168.5.40/testapi/Demo/unit_masterList";

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;


                // Send the GET request to the API
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var unitsjson = await response.Content.ReadAsStringAsync();

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    UnitList = JsonConvert.DeserializeObject<List<UnitsViewModel>>(unitsjson);
                    // ViewBag.country = AirPortList;
                    return Json(UnitList);
                }
                return Json(new { result = "failed" });
            }
        }

        public async Task<ActionResult> GetDoseForm()
        {
            var Username = "SampleUser";
            var PasswordHash = "SamplePasswordHash";
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            //var logedinUser = (CustomIdentity)Session["customIdentity"];
            List<DossageForm> DossageList = new List<DossageForm>();
            //string EnqCode = "";
            using (var client = new HttpClient())
            {
                var apiUrl = "https://192.168.5.40/testapi/Demo/GetSectionName";

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;


                // Send the GET request to the API
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var dosejson = await response.Content.ReadAsStringAsync();

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    DossageList = JsonConvert.DeserializeObject<List<DossageForm>>(dosejson);
                    // ViewBag.country = AirPortList;
                    return Json(DossageList);
                }
                return Json(new { result = "failed" });
            }
        }


        [HttpPost]
        public async Task<ActionResult> GenerateInquiryFirst(GenInqFirst model)
        {
            var Username = "SampleUser";
            var PasswordHash = "SamplePasswordHash";
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            string json = JsonConvert.SerializeObject(model);

            using (var client = new HttpClient())
            {
                var apiUrl = "https://192.168.5.40/testapi/Demo/Insertenquiry_quatation";

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

                var response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    // Data sent successfully
                    return Json("Success");
                }
                else
                {
                    // Handle error
                    return Json("Failure");
                }
            }
        }

        public async Task<ActionResult> GenerateInquirySecond(List<PlaceInquiry> model)
        {
            var Username = "SampleUser";
            var PasswordHash = "SamplePasswordHash";
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            string json = JsonConvert.SerializeObject(model);
            int result = 0;

            using (var client = new HttpClient())
            {
                var apiUrl = "https://192.168.5.40/testapi/Demo/InsertMaster";

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;

                var response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    var dosejson = await response.Content.ReadAsStringAsync();

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    result = JsonConvert.DeserializeObject<int>(dosejson);
                    // ViewBag.country = AirPortList;
                    return Json(result);
                    //return Json("Success");
                }
                else
                {
                    // Handle error
                    return Json("Failure");
                }
            }
        }




        public async Task<ActionResult> GetEnqCodeList()
        {
            var Username = "SampleUser";
            var PasswordHash = "SamplePasswordHash";
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            var logedinUser = (CustomIdentity)Session["customIdentity"];
            List<Inquiry> DossageList = new List<Inquiry>();
            //string EnqCode = "";
            using (var client = new HttpClient())
            {
            
                var apiUrl = "https://192.168.5.40/testapi/Demo/GetEnqByPartydatalistdata?party=" + logedinUser.UserID;

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;


                // Send the GET request to the API
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var dosejson = await response.Content.ReadAsStringAsync();

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    DossageList = JsonConvert.DeserializeObject<List<Inquiry>>(dosejson);
                    // ViewBag.country = AirPortList;
                    return Json(DossageList);
                }
                return Json(new { result = "failed" });
            }
        }



        public async Task<ActionResult> OldInquiryList()
        {
            var Username = "SampleUser";
            var PasswordHash = "SamplePasswordHash";
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            var logedinUser = (CustomIdentity)Session["customIdentity"];

            List<OldInquiry> inquiries = new List<OldInquiry>();
            using (var client = new HttpClient())
            {
                // Replace "your-api-url" with the actual API URL
                var apiUrl = "https://192.168.5.40/testapi/Demo/Get_AllEnquiryList?Party_code=" + logedinUser.UserID;

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;


                // Send the GET request to the API
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var InquiryData = await response.Content.ReadAsStringAsync();

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    inquiries = JsonConvert.DeserializeObject<List<OldInquiry>>(InquiryData);

                }


            }

            var query = inquiries.AsQueryable();
            return Json(query);
        }

        public async Task<ActionResult> OldInquiryPendingList()
        {
            var Username = "SampleUser";
            var PasswordHash = "SamplePasswordHash";
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            var logedinUser = (CustomIdentity)Session["customIdentity"];

            List<OldInquiry> inquiries = new List<OldInquiry>();
            using (var client = new HttpClient())
            {
                // Replace "your-api-url" with the actual API URL
                var apiUrl = "https://192.168.5.40/testapi/Demo/Get_PendingInquiry?PartyCode=" + logedinUser.UserID;

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;


                // Send the GET request to the API
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var InquiryData = await response.Content.ReadAsStringAsync();

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    inquiries = JsonConvert.DeserializeObject<List<OldInquiry>>(InquiryData);

                }


            }

            var query = inquiries.AsQueryable();
            return Json(query);
        }

        public async Task<ActionResult> OldInquiryApprovedList()
        {
            var Username = "SampleUser";
            var PasswordHash = "SamplePasswordHash";
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            var logedinUser = (CustomIdentity)Session["customIdentity"];

            List<OldInquiry> inquiries = new List<OldInquiry>();
            using (var client = new HttpClient())
            {
                // Replace "your-api-url" with the actual API URL
                var apiUrl = "https://192.168.5.40/testapi/Demo/Get_ApprovedEnquiryList?Party_code=" + logedinUser.UserID;

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;


                // Send the GET request to the API
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var InquiryData = await response.Content.ReadAsStringAsync();

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    inquiries = JsonConvert.DeserializeObject<List<OldInquiry>>(InquiryData);

                }


            }

            var query = inquiries.AsQueryable();
            return Json(query);
        }


        public async Task<ActionResult> OldInquiryCodeWiseList(string inquiryCode)
        {
            var Username = "SampleUser";
            var PasswordHash = "SamplePasswordHash";
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            var logedinUser = (CustomIdentity)Session["customIdentity"];

            List<OldInquiry> inquiries = new List<OldInquiry>();
            using (var client = new HttpClient())
            {
                // Replace "your-api-url" with the actual API URL
                var apiUrl = "https://192.168.5.40/testapi/Demo/GetEnqCodesByDateList?EnquiryCode=" + inquiryCode;

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;


                // Send the GET request to the API
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var InquiryData = await response.Content.ReadAsStringAsync();

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    inquiries = JsonConvert.DeserializeObject<List<OldInquiry>>(InquiryData);

                }


            }

            var query = inquiries.AsQueryable();
            return Json(query);
        }

        public async Task<ActionResult> OldInquiryDosformWiseList(string dosageForm)
        {
            var Username = "SampleUser";
            var PasswordHash = "SamplePasswordHash";
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            var logedinUser = (CustomIdentity)Session["customIdentity"];

            List<OldInquiry> inquiries = new List<OldInquiry>();
            using (var client = new HttpClient())
            {
                // Replace "your-api-url" with the actual API URL
                var apiUrl = "https://192.168.5.40/testapi/Demo/GetDSGBYPARTYLIST?DSGCODES=" + dosageForm + "&PARTYCODES=" + logedinUser.UserID;

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;


                // Send the GET request to the API
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var InquiryData = await response.Content.ReadAsStringAsync();

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    inquiries = JsonConvert.DeserializeObject<List<OldInquiry>>(InquiryData);

                }


            }

            var query = inquiries.AsQueryable();
            return Json(query);
        }

        public async Task<ActionResult> OldInquiryDateWiseList(string startDate,string endDate)
        {
            var Username = "SampleUser";
            var PasswordHash = "SamplePasswordHash";
            var base64Credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{PasswordHash}"));
            var logedinUser = (CustomIdentity)Session["customIdentity"];

            List<OldInquiry> inquiries = new List<OldInquiry>();
            using (var client = new HttpClient())
            {
      
                // Replace "your-api-url" with the actual API URL
                var apiUrl = "https://192.168.5.40/testapi/Demo/GetEnqCodesByDate?PartyCodes=" + logedinUser.UserID + "&StartDate=" + startDate + "&EndDate=" + endDate;

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64Credentials);
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;


                // Send the GET request to the API
                var response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var InquiryData = await response.Content.ReadAsStringAsync();

                    //Deserializing the response recieved from web api and storing into the Employee list  
                    inquiries = JsonConvert.DeserializeObject<List<OldInquiry>>(InquiryData);

                }


            }

            var query = inquiries.AsQueryable();
            return Json(query);
        }


        public ActionResult AddPartyDetails()
        {
            return View();
        }




        public ActionResult InquiryHistory()
        {
            return View();
        }
        public ActionResult HomePage()
        {
            ViewBag.Message = "Home Page.";
            return View();
        }




        public ActionResult Quotation()
        {
            ViewBag.Message = "Quotation";
            return View();
        }

        public ActionResult Description()
        {
            ViewBag.Message = "Description";
            return View();
        }
    }
}