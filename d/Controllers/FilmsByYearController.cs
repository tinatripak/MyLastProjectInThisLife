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
    public class FilmsByYearController : ControllerBase
    {

        [HttpGet("{year}")]
        public async Task<Titles[]> GetFilm(int year)
        {
            JObject search = JObject.Parse(await Get($"https://api.themoviedb.org/3/discover/movie?api_key=1490553eadc577108121742dfafd35b2&language=en-US&sort_by=popularity.desc&include_adult=false&include_video=false&page=1&year={year}"));
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