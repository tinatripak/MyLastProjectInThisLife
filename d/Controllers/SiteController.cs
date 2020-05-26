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
    public class SiteController : ControllerBase
    {

        [HttpGet("{id}")]
        public async Task<Site> GetSite(int id)
        {
            //var id = GetId(name);
            JObject search = JObject.Parse(await Get($"https://api.themoviedb.org/3/movie/{id}?api_key=1490553eadc577108121742dfafd35b2&language=en-US"));
            Site site = JsonConvert.DeserializeObject<Site>(search.ToString());
            return site;
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