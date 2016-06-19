using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Models
{
    public class Photo
    {
        public string data { get; set; }

        [JsonIgnore]
        public string ImageType {
            get
            {
                return this.data.Split(';')[0];
            }
        }

        [JsonIgnore]
        public byte[] ImageBytes {
            get
            {
                var base64Complete = this.data.Split(';')[1];
                var base64 = base64Complete.Remove(0, 7);
                return Convert.FromBase64String(base64);
            }
        }
    }
}
