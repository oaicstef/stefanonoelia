using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Api.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Api.Controllers
{
    [RoutePrefix("api/mongo")]
    public class MongoController : ApiController
    {
        private const string ConnectionString = "mongodb://oaicstef:ciaociao1981@ds013221.mlab.com:13221/stenoe";
        private IMongoCollection<Confirmation> collection;

        public MongoController()
        {
            var client = new MongoClient(ConnectionString);
            var database = client.GetDatabase("stenoe");
            collection = database.GetCollection<Confirmation>("confirmations");
            
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            return Ok();
        }

        [Route()]
        [HttpPost]
        public async Task<IHttpActionResult> InsertConfirmation(Confirmation data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await collection.InsertOneAsync(data);
            return Ok();
        }
    }
}
