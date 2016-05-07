using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Api.Models;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Api.Controllers
{
    [RoutePrefix("api/music")]
    public class MusicController : ApiController
    {
        private readonly string connectionString = ConfigurationManager.AppSettings["mongoConnectionString"];
        private readonly IMongoCollection<Song> collection;

        public MusicController()
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("stenoe");
            collection = database.GetCollection<Song>("songs");
        }

        [Route()]
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var result = await collection.Find(c => c.Title != null).ToListAsync();
            return Ok(result);
        }

        [Route()]
        [HttpPost]
        public async Task<IHttpActionResult> InsertConfirmation(JObject data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var songs = new List<Song>();
            
            foreach (var prop in data.Properties()
                .Where(prop => 
                              prop.Name.Contains("song") && !string.IsNullOrWhiteSpace(prop.Value.ToString())
                      ))
            {
                var existingSong = collection.AsQueryable().Any(s => s.Title == prop.Value.ToString());

                if (!existingSong)
                {
                    var song = data.ToObject<Song>();
                    song.Title = prop.Value.ToString();
                    
                    songs.Add(song);
                }
            }

            await collection.InsertManyAsync(songs);
            return Ok("ok");
        }
    }
}
