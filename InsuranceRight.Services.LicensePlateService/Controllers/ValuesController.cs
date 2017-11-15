using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using InsuranceRight.Services.LicensePlateService.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace InsuranceRight.Services.LicensePlateService.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public string Get()
        {
            return "Add license plate number behind url";
        }

        // GET api/values/5
        [HttpGet("{kenteken}")]
        public async Task<string> Get(string kenteken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var url = $"https://opendata.rdw.nl/resource/m9d7-ebf2.json?kenteken={kenteken}";

            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                
                Car car = JsonConvert.DeserializeObject<Car>(json);
                var jObject = JObject.Parse(json);
            };

            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
