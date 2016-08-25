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
    [RoutePrefix("api/mongo")]
    public class MongoController : ApiController
    {
        private readonly string connectionString = ConfigurationManager.AppSettings["mongoConnectionString"];
        private readonly IMongoCollection<Confirmation> collection;

        public MongoController()
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("stenoe");
            collection = database.GetCollection<Confirmation>("confirmations");
        }

        [Route()]
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var result = await collection.Find(c => c.Name != null).ToListAsync();
            
            return Ok(result);
        }

        [Route("emails")]
        public async Task<IHttpActionResult> GetEmails()
        {
            var result = (await collection.FindAsync(c => c.Name != null));
            var r = string.Join(";", (await result.ToListAsync()).Select(c => c.Email));
            return Ok(r);
        }

        [Route()]
        [HttpPost]
        public async Task<IHttpActionResult> InsertConfirmation(JObject data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var conf = data.ToObject<Confirmation>();
            
            var exists = collection.AsQueryable().Any(c => c.Email.Equals(conf.Email));
            if (exists)
            {
                conf.Name += "------REPEATED-----";
            }

            foreach (var prop in data.Properties()
                .Where(prop => 
                              prop.Name.Contains("guest") && !string.IsNullOrWhiteSpace(prop.Value.ToString())
                      ))
            {
                conf.Guests.Add(prop.Value.ToString());
            }

            await collection.InsertOneAsync(conf);
            return Ok("ok");
        }


    }
}
