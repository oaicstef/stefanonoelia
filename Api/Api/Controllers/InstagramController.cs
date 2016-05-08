using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Api.Controllers
{
    [RoutePrefix("api/instagram")]
    public class InstagramController : ApiController
    {
        [Route()]
        [HttpGet]
        public async Task<IHttpActionResult> Get()
       {
            var client = new HttpClient {BaseAddress = new Uri("https://api.instagram.com")};
            var hashtag = "NoeSte2016";
            //var access_token = "343055137.1677ed0.82fe8f3fe22d409887cb02784daab5d2"; // Stefano
            var access_token = "251080076.1677ed0.2b8b498ad6df4aef8a67178336940789";
            var count = 200;
            var result = await client.GetAsync($"v1/tags/{hashtag}/media/recent?access_token={access_token}&count={count}");
            var content = await result.Content.ReadAsAsync<object>();
            return Ok(content);
        }
    }
}
