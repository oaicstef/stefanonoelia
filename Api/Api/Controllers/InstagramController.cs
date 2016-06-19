using InstaSharp;
using InstaSharp.Models.Responses;
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
        static OAuthResponse response;
        private string clientId = "58d480e00e9d4af8bb675bb94c318f57";
        private string clientSecret = "77b4d270f36b4af6b09d84bbad6f38e8";
        private string redirectUri = @"https://stenoeapi.azurewebsites.net/api/instagram/";
        private string realtimeUri = "";

        [Route("GetAuthorizationLink")]
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            //var client = new HttpClient {BaseAddress = new Uri("https://api.instagram.com")};
            //var hashtag = "noeste2016";
            ////var access_token = "343055137.1677ed0.82fe8f3fe22d409887cb02784daab5d2"; // Stefano
            //var access_token = "251080076.1677ed0.2b8b498ad6df4aef8a67178336940789"; // Noelia
            //var count = 200;
            //var result = await client.GetAsync($"v1/tags/{hashtag}/media/recent?access_token={access_token}&count={count}");
            //var content = await result.Content.ReadAsAsync<dynamic>();
            //return Ok(content);
                       
            var scopes = new List<OAuth.Scope>()
            {
                InstaSharp.OAuth.Scope.Basic,
                InstaSharp.OAuth.Scope.Likes,
                InstaSharp.OAuth.Scope.Comments,
                InstaSharp.OAuth.Scope.Public_Content
            };
            InstagramConfig conf = new InstagramConfig(clientId, clientSecret, redirectUri, realtimeUri);
            var link = InstaSharp.OAuth.AuthLink(conf.OAuthUri + "authorize", conf.ClientId, conf.RedirectUri, scopes, InstaSharp.OAuth.ResponseType.Code);

            return Ok(link);
        }

        [Route("GetAccessToken")]
        [HttpGet]
        public async Task<IHttpActionResult> OAuth(string code)
        {
            var scopes = new List<OAuth.Scope>()
            {
                InstaSharp.OAuth.Scope.Basic,
                InstaSharp.OAuth.Scope.Likes,
                InstaSharp.OAuth.Scope.Comments
            };
            InstagramConfig config = new InstagramConfig(clientId, clientSecret, redirectUri, realtimeUri);


            // add this code to the auth object
            var auth = new OAuth(config);

            // now we have to call back to instagram and include the code they gave us
            // along with our client secret
            response = await auth.RequestToken(code);

            // both the client secret and the token are considered sensitive data, so we won't be
            // sending them back to the browser. we'll only store them temporarily.  If a user's session times
            // out, they will have to click on the authenticate button again - sorry bout yer luck.
            //Session.Add("InstaSharp.AuthInfo", oauthResponse);

            // all done, lets redirect to the home controller which will send some intial data to the app
            return this.Ok(response);
        }

        [Route()]
        [HttpGet]
        public async Task<IHttpActionResult> MyPhotos()
        {
            var scopes = new List<OAuth.Scope>()
            {
                InstaSharp.OAuth.Scope.Basic,
                InstaSharp.OAuth.Scope.Likes,
                InstaSharp.OAuth.Scope.Comments,
                InstaSharp.OAuth.Scope.Public_Content
            };
            InstagramConfig config = new InstagramConfig(clientId, clientSecret, redirectUri, realtimeUri);
            var oAuthResponse = response as OAuthResponse;
            if (oAuthResponse == null)
            {
                oAuthResponse = new OAuthResponse();
                oAuthResponse.AccessToken = "251080076.58d480e.84d679a79c7a4153b8de442085b93c2a";
                oAuthResponse.User = new InstaSharp.Models.UserInfo();
                oAuthResponse.User.FullName = "Noelia Grande Galan";
                oAuthResponse.User.ProfilePicture = @"https://scontent.cdninstagram.com/t51.2885-19/11251187_927368407302345_1303700712_a.jpg";
                oAuthResponse.User.Username = "noegrandegalan";
            }
            var tags = new InstaSharp.Endpoints.Tags(config, oAuthResponse);

            var tagResponse = await tags.Recent("noeste");

            return Ok(tagResponse);
        }
    }
}
