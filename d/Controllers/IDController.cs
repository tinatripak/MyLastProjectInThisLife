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
    public class IDController : ControllerBase
    {
        [HttpGet("{name}")]
        public async Task<IdOfFilm> GetId(string name)
        {
            JObject search = JObject.Parse(await Get($"https://api.themoviedb.org/3/search/movie?api_key=1490553eadc577108121742dfafd35b2&query={name}"));
            IdOfFilm id = JsonConvert.DeserializeObject<IdOfFilm>(search["results"][0].ToString());
            return id;
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