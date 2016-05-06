using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Google.Apis.Admin.Directory.directory_v1;
using Google.Apis.Admin.Directory.directory_v1.Data;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.GData.Client;
using Google.GData.Photos;

namespace Api.Controllers
{
    [RoutePrefix("api/google")]
    public class GoogleController : ApiController
    {
        static string[] Scopes = { DirectoryService.Scope.AdminDirectoryUserReadonly };
        static string ApplicationName = "ApiSteNoe";

        [Route()]
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            UserCredential credential;

            using (var stream =
                new FileStream(@"E:\Tools\client_secret.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/admin-directory_v1-dotnet-quickstart.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "damicos",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            var service = new DirectoryService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define parameters of request.
            UsersResource.ListRequest request = service.Users.List();
            request.Customer = "damicos@gmail.com";
            request.MaxResults = 10;
            request.OrderBy = UsersResource.ListRequest.OrderByEnum.Email;

            // List users.
            IList<User> users = request.Execute().UsersValue;
            Console.WriteLine("Users:");
            if (users != null && users.Count > 0)
            {
                foreach (var userItem in users)
                {
                    Console.WriteLine("{0} ({1})", userItem.PrimaryEmail,
                        userItem.Name.FullName);
                }
            }
            else
            {
                Console.WriteLine("No users found.");
            }


            Console.Read();

            return Ok(users);
        }
    }
}
