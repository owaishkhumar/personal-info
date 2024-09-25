using FrontEndPersonalInfo.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FrontEndPersonalInfo.Controllers
{
    public class PersonalInfoController : Controller
    {
        Uri baseAddress = new Uri("http://localhost:5120/api");
        private readonly HttpClient _client = new HttpClient();

        public PersonalInfoController()
        {


            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<PersonalInfoViewModel> personalInfolist = new List<PersonalInfoViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/PersonalInfo").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                personalInfolist = JsonConvert.DeserializeObject<List<PersonalInfoViewModel>>(data);
            }


            return View(personalInfolist);
        }


        [HttpGet]
        public IActionResult AddDetails()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddDetails(InsertPersonalDetailsViewModel insertDetails)
        {
            if (ModelState.IsValid)
            {
                using (var details = new MultipartFormDataContent())
                {
                    // Add other fields
                    details.Add(new StringContent(insertDetails.Name), "Name");
                    details.Add(new StringContent(insertDetails.DateOfBirth), "DateOfBirth");
                    details.Add(new StringContent(insertDetails.ResidentialAddress), "ResidentialAddress");
                    details.Add(new StringContent(insertDetails.PermanentAddress), "PermanentAddress");
                    details.Add(new StringContent(insertDetails.MobileNumber), "MobileNumber");
                    details.Add(new StringContent(insertDetails.EmailAddress), "EmailAddress");
                    details.Add(new StringContent(insertDetails.MaritalStatus), "MaritalStatus");
                    details.Add(new StringContent(insertDetails.Gender), "Gender");
                    details.Add(new StringContent(insertDetails.Occupation), "Occupation");
                    details.Add(new StringContent(insertDetails.AadharCardNumber), "AadharCardNumber");
                    details.Add(new StringContent(insertDetails.PANNumber), "PANNumber");

                    // Add the file
                    if (insertDetails.ImageFile != null && insertDetails.ImageFile.Length > 0)
                    {
                        var fileContent = new StreamContent(insertDetails.ImageFile.OpenReadStream());
                        fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(insertDetails.ImageFile.ContentType);
                        details.Add(fileContent, "ImageFile", insertDetails.ImageFile.FileName);
                    }

                    // Send to the API
                    var response = await _client.PostAsync($"{baseAddress}/PersonalInfo", details);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "An error occurred while uploading the details.");
                    }
                }
            }

            return View();
        }


        []
    }
}
