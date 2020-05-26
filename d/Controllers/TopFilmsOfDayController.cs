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
    [Route("[controller]")]
    [ApiController]
    public class TopFilmsOfDayController : ControllerBase
    {
        [HttpGet]
        public async Task<Titles[]> GetTitles()
        {
            JObject search = JObject.Parse(await Get($"https://api.themoviedb.org/3/trending/movie/day?api_key=1490553eadc577108121742dfafd35b2"));
            ArrayTitle title = JsonConvert.DeserializeObject<ArrayTitle>(search.ToString());
            return title.Results;
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