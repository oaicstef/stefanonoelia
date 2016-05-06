using System.Web.Script.Serialization;
using System.Xml.Serialization;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace Api.Models
{
    public class Song
    {
        public Song()
        {
        }

        public Song(string title)
        {
            this.Title = title;
        }

        [JsonIgnore]
        [XmlIgnore]
        public ObjectId _id { get; set; }

        public string Title { get; set; }
    }
}
