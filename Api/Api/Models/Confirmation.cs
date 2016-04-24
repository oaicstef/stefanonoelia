using System.Collections.Generic;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace Api.Models
{
    public class Confirmation
    {
        public Confirmation()
        {
            this.Guests = new List<string>();
        }

        public ObjectId _id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public IList<string> Guests { get; set; }

        [JsonProperty("confirmationResponse")]
        public string ConfirmationResponse { get; set; }
    }
}