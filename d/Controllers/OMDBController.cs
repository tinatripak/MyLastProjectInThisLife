using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace d.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OMDBController : ControllerBase
    {
        [HttpGet("{name}")]
        public async Task<omdbinfo> GetInfo(string name)
        {
            JObject search = JObject.Parse(await Get($"http://www.omdbapi.com/?t={name}&y=2020&apikey=6bc61750"));
            omdbinfo film = JsonConvert.DeserializeObject<omdbinfo>(search.ToString());
            return film;
        }

        public static async Task<string> Get(string link)
        {
            HttpClient httpclient = new HttpClient();
            HttpResponseMessage httpResponseMessage = await httpclient.GetAsync(link);
            HttpContent content = httpResponseMessage.Content;
            string result = await content.ReadAsStringAsync();
            return result;

        }
    }
}