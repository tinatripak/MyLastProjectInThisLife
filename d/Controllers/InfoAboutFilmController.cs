using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace d.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InfoAboutFilmController : ControllerBase
    {
        [HttpGet("{name}")]
        public async Task<Film> GetInfo(string name)
        {
            JObject search = JObject.Parse(await Get($"https://api.themoviedb.org/3/search/movie?api_key=1490553eadc577108121742dfafd35b2&query={name}"));
            Film film = JsonConvert.DeserializeObject<Film>(search["results"][0].ToString());
            return film;
        }



        public static async Task <string> Get(string link)
        {
            HttpClient httpclient = new HttpClient();
            HttpResponseMessage httpResponseMessage = await httpclient.GetAsync(link);
            HttpContent content = httpResponseMessage.Content;
            string result = await content.ReadAsStringAsync();
            return result;

        }
    }
}
