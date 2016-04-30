using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Google.GData.Photos;

namespace Api.Controllers
{
    [RoutePrefix("api/google")]
    public class GoogleController : ApiController
    {
        [Route()]
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {

            PicasaService s = new PicasaService("Clave de servidor 1");
            s.SetAuthenticationToken("AIzaSyAnYNqu7BfRQsxqSJZMTVWHur-RfWkEGI0");
            var query = new AlbumQuery(PicasaQuery.CreatePicasaUri("damicos@gmail.com"));
            var feed = s.Query(query);
            foreach (var item in feed.Entries)
            {
                var c = item;
            }

            return Ok(feed);
        }
    }
}
