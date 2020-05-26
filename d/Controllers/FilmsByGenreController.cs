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
    public class FilmsByGenreController : ControllerBase
    {
        [HttpGet("{genre}")]

        public async Task<Titles[]> GetFilm(int genre)
        {
            
            JObject search = JObject.Parse(await Get($"https://api.themoviedb.org/3/discover/movie?api_key=1490553eadc577108121742dfafd35b2&language=en-US&sort_by=popularity.desc&include_adult=false&include_video=false&page=1&with_genres={genre}"));
            ArrayTitle title = JsonConvert.DeserializeObject<ArrayTitle>(search.ToString());
            /*Console.WriteLine("Write 'id': 28, if 'name'is Action");
            Console.WriteLine("Write 'id': 12, if 'name'is Adventure");
            Console.WriteLine("Write 'id': 16, if 'name'is Animation");
            Console.WriteLine("Write 'id': 35, if 'name'is Comedy");
            Console.WriteLine("Write 'id': 80, if 'name'is Crime");
            Console.WriteLine("Write 'id': 99, if 'name'is Documentary");
            Console.WriteLine("Write 'id': 18, if 'name'is Drama");
            Console.WriteLine("Write 'id': 10751, if 'name'is Family");
            Console.WriteLine("Write 'id': 14, if 'name'is Fantasy");
            Console.WriteLine("Write 'id': 36, if 'name'is History");
            Console.WriteLine("Write 'id': 27, if 'name'is Horror");
            Console.WriteLine("Write 'id': 10402, if 'name'is Music");
            Console.WriteLine("Write 'id': 9648, if 'name'is Mystery");
            Console.WriteLine("Write 'id': 10749, if 'name'is Romane");
            Console.WriteLine("Write 'id': 878, if 'name'is Science Fiction");
            Console.WriteLine("Write 'id': 10770, if 'name'is TV Movie");
            Console.WriteLine("Write 'id': 53, if 'name'is Thriller");
            Console.WriteLine("Write 'id': 10752, if 'name'is War");
            Console.WriteLine("Write 'id': 37, if 'name'is Western");*/
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