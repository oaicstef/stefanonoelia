using System;
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
            this.DateTime = DateTime.Now;
        }

        public ObjectId _id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public IList<string> Guests { get; set; }

        [JsonProperty("confirmationResponse")]
        public string ConfirmationResponse { get; set; }

        public string Message { get; set; }

        public DateTime DateTime { get; set; }
    }
}