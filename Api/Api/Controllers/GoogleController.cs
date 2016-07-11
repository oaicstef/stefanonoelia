using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Util.Store;
using Google.GData.Client;
using Google.GData.Photos;
using Google.Picasa;
using Newtonsoft.Json.Linq;

namespace Api.Controllers
{
    [RoutePrefix("api/google")]
    public class GoogleController : ApiController
    {
        string[] Scopes = { DriveService.Scope.Drive, "http://picasaweb.google.com/data/" };
        const string ApplicationName = "ApiSteNoe";
        const string username = "noeliastefano@gmail.com";
        const string albumId = "6305744093763953121"; // Our Wedding

        [Route("photos")]
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            
            var picasaService = getPicasaService();
            PhotoQuery photoQuery = new PhotoQuery(PicasaQuery.CreatePicasaUri(username, albumId));
            PicasaFeed feed = picasaService.Query(photoQuery);

            var result = new List<dynamic>();
            foreach (var entry in feed.Entries)
            {

                var photo = new Photo();
                photo.AtomEntry = entry;

                result.Add(new
                {
                    title = photo.Title,
                    URI = photo.PhotoUri,
                    photo_Width = photo.Width,
                    photo_Height = photo.Height
                });
            }
            
            return Ok(new
            {
                Photos = result
            });
        }

        [Route("albums")]
        [HttpGet]
        public async Task<IHttpActionResult> GetAlbums()
        {
            var picasaService = getPicasaService();
            AlbumQuery query = new AlbumQuery(PicasaQuery.CreatePicasaUri(username));
            PicasaFeed feed = picasaService.Query(query);

            var result = new List<dynamic>();
            foreach (PicasaEntry entry in feed.Entries)
            {
                var album = new AlbumAccessor(entry);
                result.Add(new
                {
                    title = entry.Title.Text,
                    numPhotos = album.NumPhotos,
                    AlbumId = album.Id
                });
            }

            return Ok(new
            {
                Albums = result
            });
        }

        [Route]
        [HttpPost]
        public async Task<IHttpActionResult> PostPhoto()
        {
            try
            {
                if (HttpContext.Current.Request.Files.Count == 0)
                    return BadRequest("No file has been sent");

                var file = HttpContext.Current.Request.Files[0];
                if (file != null)
                {
                    using (var dataStream = file.InputStream)
                    {
                        var filePath = $"{Environment.GetFolderPath(Environment.SpecialFolder.Personal)}\\postedPhoto.jpg";
                        var picasaService = getPicasaService();
                        var postUri = new Uri(PicasaQuery.CreatePicasaUri("noeliastefano", albumId));
                        PicasaEntry entry =
                                (PicasaEntry)picasaService.Insert(postUri, dataStream, file.ContentType, file.FileName);
                    }
                }
                return Ok();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private PicasaService getPicasaService()
        {
            var userCredential = getCredentials();
            var auth = new OAuth2Parameters
            {
                ClientId = "951622103837-fl5hjltc91v3l75djc10cdtvok7qgsk5.apps.googleusercontent.com",
                ClientSecret = "lbLu0g6w7gk5ckuPzSZZRQem",
                RedirectUri = "[ 'urn:ietf: wg: oauth: 2.0:oob', 'http://localhost' ]",
                Scope = "http://picasaweb.google.com/data/",
                AccessToken = userCredential.Token.AccessToken,
                RefreshToken = userCredential.Token.RefreshToken,
                TokenType = userCredential.Token.TokenType,
            };

            var requestFactory = new GOAuth2RequestFactory(null, ApplicationName, auth);
            var picasaService = new PicasaService(ApplicationName);
            var userToken = userCredential.Token.TokenType + " " + userCredential.Token.AccessToken;
            picasaService.Credentials = new GDataCredentials(userToken);
            picasaService.RequestFactory = requestFactory;
            return picasaService;
        }

        private UserCredential getCredentials()
        {
            UserCredential credential;
            string credPath = Path.Combine(HostingEnvironment.MapPath("~/"), "Config");

            using (var stream =
                new FileStream($"{credPath}/client_secret.json", FileMode.Open, FileAccess.Read))
            {
                credPath = Path.Combine(credPath, "credentials.json");
                var store = new FileDataStore(credPath, true);
                
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    username,
                    CancellationToken.None,
                    store).Result;

                if (credential.Token.IsExpired(Google.Apis.Util.SystemClock.Default))
                {
                    var resultRefreshToken = credential.RefreshTokenAsync(CancellationToken.None).Result;
                }
                
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            return credential;

        }
    }
}

//    UserCredential credential;

//    using (var stream =
//        new FileStream(@"E:\Tools\client_secret.json", FileMode.Open, FileAccess.Read))
//    {
//        string credPath = System.Environment.GetFolderPath(
//            System.Environment.SpecialFolder.Personal);
//        credPath = Path.Combine(credPath, ".credentials/admin-directory_v1-dotnet-quickstart.json");

//        credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
//            GoogleClientSecrets.Load(stream).Secrets,
//            Scopes,
//            "damicos",
//            CancellationToken.None,
//            new FileDataStore(credPath, true)).Result;
//        Console.WriteLine("Credential file saved to: " + credPath);
//    }

//    var service = new DirectoryService(new BaseClientService.Initializer()
//    {
//        HttpClientInitializer = credential,
//        ApplicationName = ApplicationName,
//    });

//    // Define parameters of request.
//    UsersResource.ListRequest request = service.Users.List();
//    request.Customer = "damicos@gmail.com";
//    request.MaxResults = 10;
//    request.OrderBy = UsersResource.ListRequest.OrderByEnum.Email;

//    // List users.
//    IList<User> users = request.Execute().UsersValue;
//    Console.WriteLine("Users:");
//    if (users != null && users.Count > 0)
//    {
//        foreach (var userItem in users)
//        {
//            Console.WriteLine("{0} ({1})", userItem.PrimaryEmail,
//                userItem.Name.FullName);
//        }
//    }
//    else
//    {
//        Console.WriteLine("No users found.");
//    }


//    Console.Read();

//    return Ok(users);