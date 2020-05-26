using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace botmaybe
{
    class Program
    {
        private static readonly TelegramBotClient Bot = new TelegramBotClient("1167376090:AAFPCu1mtNTHbEQ8F_tTOyqHFdKw05TYRPg");
    
        public static List<string> names = new List<string>();
        public static InlineKeyboardMarkup genres = new InlineKeyboardMarkup(new[]
        {
            new [] { InlineKeyboardButton.WithCallbackData("Action", "28"),
            InlineKeyboardButton.WithCallbackData("Adventure", "12"),
            InlineKeyboardButton.WithCallbackData("Animation", "16")},
            new [] { InlineKeyboardButton.WithCallbackData("Comedy", "35"),
            InlineKeyboardButton.WithCallbackData("Crime", "80"),
            InlineKeyboardButton.WithCallbackData("Documentary", "99")},
            new [] { InlineKeyboardButton.WithCallbackData("Drama", "18"),
            InlineKeyboardButton.WithCallbackData("Family", "10751"),
            InlineKeyboardButton.WithCallbackData("Fantasy", "14") },
            new [] { InlineKeyboardButton.WithCallbackData("History", "36"),
            InlineKeyboardButton.WithCallbackData("Horror", "27"),
            InlineKeyboardButton.WithCallbackData("Music", "10402") },
            new [] { InlineKeyboardButton.WithCallbackData("Mystery", "9648"),
            InlineKeyboardButton.WithCallbackData("Romance", "10749"),
            InlineKeyboardButton.WithCallbackData("Science Fiction", "878") },
            new [] { InlineKeyboardButton.WithCallbackData("TV Movie", "10770"),
            InlineKeyboardButton.WithCallbackData("Thriller", "53"),
            InlineKeyboardButton.WithCallbackData("War", "10752"),
            InlineKeyboardButton.WithCallbackData("Western", "37")}
        });


        public static bool here = false;
        public static string AllFilms = string.Empty;

        public static InlineKeyboardMarkup sites = new InlineKeyboardMarkup(new[]
        {
            new [] { InlineKeyboardButton.WithUrl("youtube", "https://www.youtube.com"),},
            new [] { InlineKeyboardButton.WithUrl("popcornflix", "https://www.popcornflix.com"),},
            new [] { InlineKeyboardButton.WithUrl("archive", "https://archive.org/details/moviesandfilms"),},
            new [] { InlineKeyboardButton.WithUrl("imdb", "https://www.imdb.com/tv/"),}
        });
        static void Main(string[] args)
        {

            Console.OutputEncoding = Encoding.UTF8;
            Bot.OnMessage += Bot_OnMessage;
            Bot.OnCallbackQuery += Bot_OnCallbackQuery;
            Bot.OnMessageEdited += Bot_OnMessage;
            Bot.StartReceiving();
            Console.ReadLine();
        }
        private static async void Bot_OnCallbackQuery(object sender, CallbackQueryEventArgs e)
        {
            string json1 = new WebClient().DownloadString($"https://filmassistantapi.azurewebsites.net/filmsbygenre/{e.CallbackQuery.Data}");
            List<Films> filmsofday = JsonConvert.DeserializeObject<List<Films>>(json1);
            string AllFilms = string.Empty;
            foreach (var film in filmsofday)
            {
                AllFilms += film.Title + "\n";

            }
            if (AllFilms != null)
            {
                await Bot.SendTextMessageAsync(e.CallbackQuery.From.Id, AllFilms);
            }
            else
            {
                await Bot.SendTextMessageAsync(e.CallbackQuery.From.Id, "No movies for this request");
            }
                
            
        }
        private static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {

           
            Console.WriteLine($"{DateTime.Now} the {e.Message.Chat.Username}({e.Message.Chat.Id}) Write: {e.Message.Text}");
            
                    switch (e.Message.Text)
                    {

                        case "/start":
                            await Bot.SendTextMessageAsync(e.Message.Chat.Id, "🌸Hi, please select one function that you need🌸\n" + "/moviehistory\n" + "/freesites\n" + "/filmsbygenre\n" + "/filmsbyyear\n" + "/popularmoviesofyear\n" + "/popularmoviesofday\n" + "/addserial\n" + "/deleteserial\n" + "/showserials\n" + "/addfilm\n" + "/deletefilm\n" + "/showfilms\n" + "/getasite\n" + "/infoaboutfilm");
                            break;

                        case "/moviehistory":
                            await Bot.SendTextMessageAsync(e.Message.Chat.Id, "There is some information about movies 🌸🌸🌸 ");
                            await Bot.SendTextMessageAsync(e.Message.Chat.Id, "Movies, also known as films, are a type of visual communication which uses moving pictures and sound to tell stories or teach people something.\n" + "People in every part of the world watch movies as a type of entertainment, a way to have fun.\n" + "For some people, fun movies can mean movies that make them laugh, while for others it can mean movies that make them cry, or feel afraid.\n" + "Most movies are made so that they can be shown on big screens at movie theatres and at home.\n" + "After movies are shown on movie screens for a period of weeks or months, they may be marketed through several other media. \n" + "They are shown on pay television or cable television, and sold or rented on DVD disks or videocassette tapes, so that people can watch the movies at home. You can also download or stream movies.\n" + "Older movies are shown on television broadcasting stations. ");
                            await Bot.SendTextMessageAsync(e.Message.Chat.Id, "The first film, which was widely distributed, was shot in 1895 - “Arrival of a Train at La Ciotat”. If you want to read more about it, visit this website : https://www.thevintagenews.com/2016/08/08/in-1895-the-arrival-of-the-train-was-one-of-the-first-films-shown-to-the-public-it-nearly-caused-panic/ ");
                            break;

                        case "/freesites":
                            await Bot.SendTextMessageAsync(e.Message.Chat.Id, "I advice you some sites where you can find movies yourself :) ", replyMarkup: sites);
                            break;

                        case "/popularmoviesofyear":
                            string json = new WebClient().DownloadString("https://filmassistantapi.azurewebsites.net/topfilmsofyear");
                            List<Films> filmsofyear = JsonConvert.DeserializeObject<List<Films>>(json);
                            AllFilms = string.Empty;
                            foreach (var film in filmsofyear)
                            {
                                AllFilms += film.Title + "\n";
                            }
                            if (AllFilms != string.Empty)
                            {
                                await Bot.SendTextMessageAsync(e.Message.Chat.Id, AllFilms);
                            }
                            else
                            {
                                await Bot.SendTextMessageAsync(e.Message.Chat.Id, "No movies for this request :(");
                            }
                            break;

                        case "/popularmoviesofday":
                            string json1 = new WebClient().DownloadString("https://filmassistantapi.azurewebsites.net/topfilmsofday");
                            List<Films> filmsofday = JsonConvert.DeserializeObject<List<Films>>(json1);
                            AllFilms = string.Empty;
                            foreach (var film in filmsofday)
                            {
                                AllFilms += film.Title + "\n";
                            }
                            if (AllFilms != string.Empty)
                            {
                                await Bot.SendTextMessageAsync(e.Message.Chat.Id, AllFilms);
                            }
                            else
                            {
                                await Bot.SendTextMessageAsync(e.Message.Chat.Id, "No movies for this request :(");
                            }
                            break;

                        case "/filmsbygenre":
                            await Bot.SendTextMessageAsync(e.Message.Chat.Id, "Select a genre :) ", replyMarkup: genres);
                            break;

                        case "/filmsbyyear":
                            await Bot.SendTextMessageAsync(e.Message.Chat.Id, "Reply to this message, please :) \n" + "Please, write a year))", replyMarkup: new ForceReplyMarkup { Selective = true });
                            break;
                        case "/addserial":
                            await Bot.SendTextMessageAsync(e.Message.Chat.Id, "Reply to this message, please :) \n" + "Please, write a title of serial, which you want to add :)", replyMarkup: new ForceReplyMarkup { Selective = true });
                            break;

                        case "/deleteserial":
                            await Bot.SendTextMessageAsync(e.Message.Chat.Id, "Reply to this message, please :) \n" + "Please, write a title of serial, which you want to delete :)", replyMarkup: new ForceReplyMarkup { Selective = true });
                            break;

                        case "/showserials":
                            if (names.Count != 0)
                            {
                                await Bot.SendTextMessageAsync(e.Message.Chat.Id, " This 🌸 list 🌸 is yours :");
                            }
                            else
                            {
                                await Bot.SendTextMessageAsync(e.Message.Chat.Id, " This 🌸 list 🌸 is empty :(");
                            }
                            foreach (var item in names)
                            {
                                await Bot.SendTextMessageAsync(e.Message.Chat.Id, item);
                            }
                            break;

                        case "/getasite":
                            await Bot.SendTextMessageAsync(e.Message.Chat.Id, "Reply to this message, please :) \n" + "Please write a title of movie :)", replyMarkup: new ForceReplyMarkup { Selective = true });
                            break;

                        case "/infoaboutfilm":
                            await Bot.SendTextMessageAsync(e.Message.Chat.Id, "Reply to this message, please :) \n" + "Please write a title of movie to find some information about it :)", replyMarkup: new ForceReplyMarkup { Selective = true });
                            break;
                        case "/addfilm":
                            await Bot.SendTextMessageAsync(e.Message.Chat.Id, "Reply to this message, please :) \n" + "Please, write a title of film, which you want to add :)", replyMarkup: new ForceReplyMarkup { Selective = true });
                            break;
                        case "/deletefilm":
                            await Bot.SendTextMessageAsync(e.Message.Chat.Id, "Reply to this message, please :) \n" + "Please write the movie number from your list that you want to delete:)", replyMarkup: new ForceReplyMarkup { Selective = true });
                            break;
                        case "/showfilms":
                            await Bot.SendTextMessageAsync(e.Message.Chat.Id, "This 🌸 list 🌸 is yours :\n");
                            string url1 = "https://WebApplication820200525234253.azurewebsites.net/api/list";
                            HttpClient client = new HttpClient();
                            var result = await client.GetStringAsync(url1);
                            List[] jsi = JsonConvert.DeserializeObject<List[]>(result);
                            foreach (var item in jsi)
                            {
                                await Bot.SendTextMessageAsync(e.Message.Chat.Id, item.Id + ". " + item.Name);
                            }
                            break;
                    }
                    if (e.Message.ReplyToMessage != null && e.Message.ReplyToMessage.Text.Contains("Reply to this message, please :) \n" + "Please, write a title of film, which you want to add :)"))
                    {
                        var film = new Film();
                        film.Name = e.Message.Text;
                        var json = JsonConvert.SerializeObject(film);
                        var data = new StringContent(json, Encoding.UTF8, "application/json");

                        string urladd = "https://WebApplication820200525234253.azurewebsites.net/api/list";
                        HttpClient client = new HttpClient();
                        var result1 = await client.PostAsync(urladd, data);
                        await Bot.SendTextMessageAsync(e.Message.Chat.Id, "Your film is added");
                    }
                    if (e.Message.ReplyToMessage != null && e.Message.ReplyToMessage.Text.Contains("Reply to this message, please :) \n" + "Please write the movie number from your list that you want to delete:)"))
                    {
                        string url1 = "https://WebApplication820200525234253.azurewebsites.net/api/list/" + e.Message.Text;
                        if (Regex.IsMatch(e.Message.Text, @"^[0-9]+$"))
                        {
                            HttpClient client = new HttpClient();
                            var result1 = await client.DeleteAsync(url1);

                        }
                        else
                        {
                            await Bot.SendTextMessageAsync(e.Message.Chat.Id, "Your id isn't a number");

                        }
                    }
                    if (e.Message.ReplyToMessage != null && e.Message.ReplyToMessage.Text.Contains("Reply to this message, please :) \n" + "Please write a title of movie to find some information about it :)"))
                    {
                        try
                        {
                            string url2 = "https://filmassistantapi.azurewebsites.net/api/omdb/" + e.Message.Text;
                            HttpClient client = new HttpClient();
                            var title = await client.GetStringAsync(url2);
                            Info df = JsonConvert.DeserializeObject<Info>(title);
                            string plot = string.Empty;
                            foreach (var film in df.Plot)
                            {
                                plot += film;
                            }
                            if (plot != "N/A")
                            {
                                await Bot.SendTextMessageAsync(e.Message.Chat.Id, "🌸Plot🌸\n" + plot);
                            }
                            string language = string.Empty;
                            foreach (var film in df.Language)
                            {
                                language += film;
                            }
                            if (language != "N/A")
                            {
                                await Bot.SendTextMessageAsync(e.Message.Chat.Id, "🌸Language🌸\n" + language);
                            }
                            string writer = string.Empty;
                            foreach (var film in df.Writer)
                            {
                                writer += film;
                            }
                            if (writer != "N/A")
                            {
                                await Bot.SendTextMessageAsync(e.Message.Chat.Id, "🌸Writer🌸\n" + writer);
                            }
                            string genre = string.Empty;
                            foreach (var film in df.Genre)
                            {
                                genre += film;
                            }
                            if (genre != "N/A")
                            {
                                await Bot.SendTextMessageAsync(e.Message.Chat.Id, "🌸Genre🌸\n" + genre);
                            }
                            string director = string.Empty;
                            foreach (var film in df.Director)
                            {
                                director += film;
                            }
                            if (director != "N/A")
                            {
                                await Bot.SendTextMessageAsync(e.Message.Chat.Id, "🌸Director🌸\n" + director);
                            }
                            string actors = string.Empty;
                            foreach (var film in df.Actors)
                            {
                                actors += film;
                            }
                            if (actors != "N/A")
                            {
                                await Bot.SendTextMessageAsync(e.Message.Chat.Id, "🌸Actors🌸\n" + actors);
                            }
                            await Bot.SendTextMessageAsync(e.Message.Chat.Id, "🌸Release data🌸\n" + df.Year);

                        }
                        catch
                        {
                            try
                            {
                                string url1 = "https://filmassistantapi.azurewebsites.net/infoaboutfilm/" + e.Message.Text;

                                HttpClient client = new HttpClient();
                                var result = await client.GetStringAsync(url1);
                                InfoOther jsi = JsonConvert.DeserializeObject<InfoOther>(result);
                                string plot = string.Empty;
                                foreach (var film in jsi.Overview)
                                {
                                    plot += film;
                                }
                                await Bot.SendTextMessageAsync(e.Message.Chat.Id, "🌸Plot🌸\n" + plot);
                                string language = string.Empty;
                                foreach (var film in jsi.OriginalLanguage)
                                {
                                    language += film;
                                }
                                await Bot.SendTextMessageAsync(e.Message.Chat.Id, "🌸Language🌸\n" + language);
                                await Bot.SendTextMessageAsync(e.Message.Chat.Id, "🌸Release data🌸\n" + jsi.ReleaseDate);
                            }


                            catch { await Bot.SendTextMessageAsync(e.Message.Chat.Id, "No information for this request, please try again :("); }
                        }
                    }
                    if (e.Message.ReplyToMessage != null && e.Message.ReplyToMessage.Text.Contains("Reply to this message, please :) \n" + "Please write a title of movie :)"))
                    {

                        try
                        {
                            string url1 = "https://filmassistantapi.azurewebsites.net/api/id/" + e.Message.Text;

                            HttpClient client = new HttpClient();
                            var result = await client.GetStringAsync(url1);
                            ID jsi = JsonConvert.DeserializeObject<ID>(result);
                            AllFilms = string.Empty;
                            foreach (var film in jsi.Id)
                            {
                                AllFilms += film;
                            }
                            //await Bot.SendTextMessageAsync(e.Message.Chat.Id, AllFilms);
                            string url2 = "https://filmassistantapi.azurewebsites.net/api/site/" + AllFilms;
                            var site = await client.GetStringAsync(url2);
                            Site df = JsonConvert.DeserializeObject<Site>(site);
                            AllFilms = string.Empty;
                            foreach (var film in df.Homepage)
                            {

                                AllFilms += film;

                            }
                            if (AllFilms != string.Empty)
                            {
                                await Bot.SendTextMessageAsync(e.Message.Chat.Id, AllFilms);
                            }
                            else
                            {
                                await Bot.SendTextMessageAsync(e.Message.Chat.Id, "No sites for this request, please try again :(");
                            }
                        }
                        catch
                        {
                            await Bot.SendTextMessageAsync(e.Message.Chat.Id, "A Title of film isn't right :(");
                        }

                    }
                    if (e.Message.ReplyToMessage != null && e.Message.ReplyToMessage.Text.Contains("Reply to this message, please :) \n" + "Please, write a title of serial, which you want to delete :)"))
                    {
                        names.Remove(e.Message.Text);
                    }
                    if (e.Message.ReplyToMessage != null && e.Message.ReplyToMessage.Text.Contains("Reply to this message, please :) \n" + "Please, write a title of serial, which you want to add :)"))
                    {
                        names.Add(e.Message.Text);
                    }
                    if (e.Message.ReplyToMessage != null && e.Message.ReplyToMessage.Text.Contains("Reply to this message, please :) \n" + "Please, write a year))"))
                    {
                        if (Regex.IsMatch(e.Message.Text, @"^[0-9]+$"))
                        {
                            string json2 = new WebClient().DownloadString("https://filmassistantapi.azurewebsites.net/filmsbyyear/" + e.Message.Text);
                            int number = Int32.Parse(e.Message.Text);
                            if (number >= 1883 && number < 2030)
                            {
                                List<Films> filmsofday1 = JsonConvert.DeserializeObject<List<Films>>(json2);
                                AllFilms = string.Empty;
                                foreach (var film in filmsofday1)
                                {
                                    AllFilms += film.Title + "\n";
                                }
                                if (AllFilms != string.Empty)
                                {
                                    await Bot.SendTextMessageAsync(e.Message.Chat.Id, AllFilms);
                                }
                                else
                                {
                                    await Bot.SendTextMessageAsync(e.Message.Chat.Id, ", please try again :(");
                                }
                            }
                            else
                            {
                                await Bot.SendTextMessageAsync(e.Message.Chat.Id, "Your year is less than 1883 and more 2030(it can't be true) , sorry :(");
                            }
                        }
                        else
                        {
                            await Bot.SendTextMessageAsync(e.Message.Chat.Id, "Please try again, it isn't numbers or this number is negative :(");
                        }
                    }


        }
    }

    [DataContract]
    class Films
    {
        [DataMember]
        [JsonProperty("title")]
        public string Title { get; set; }
    }
    [DataContract]
    class ID
    {
        [DataMember]
        [JsonProperty("id")]
        public string Id { get; set; }
    }
    [DataContract]
    class Site
    {
        [DataMember]
        [JsonProperty("homepage")]
        public string Homepage { get; set; }
    }
    [DataContract]
    public partial class Info
    {
        [DataMember]
        [JsonProperty("title")]
        public string Title { get; set; }
        [DataMember]
        [JsonProperty("language")]
        public string Language { get; set; }
        [DataMember]
        [JsonProperty("year")]
        public long Year { get; set; }
        [DataMember]
        [JsonProperty("genre")]
        public string Genre { get; set; }
        [DataMember]
        [JsonProperty("plot")]
        public string Plot { get; set; }
        [DataMember]
        [JsonProperty("director")]
        public string Director { get; set; }
        [DataMember]
        [JsonProperty("writer")]
        public string Writer { get; set; }
        [DataMember]
        [JsonProperty("actors")]
        public string Actors { get; set; }
        [DataMember]
        [JsonProperty("poster")]
        public Uri Poster { get; set; }
    }
    [DataContract]
    public partial class InfoOther
    {

        [DataMember]
        [JsonProperty("title")]
        public string Title { get; set; }
        [DataMember]
        [JsonProperty("original_language")]
        public string OriginalLanguage { get; set; }
        [DataMember]
        [JsonProperty("release_date")]
        public DateTimeOffset ReleaseDate { get; set; }
        [DataMember]
        [JsonProperty("overview")]
        public string Overview { get; set; }
    }
    class Film
    {
        public string Name { get; set; }
    }

    public partial class List
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }
    }
}