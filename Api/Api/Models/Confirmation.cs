using System.Collections.Generic;

namespace Api.Models
{
    public class Confirmation
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public IList<string> Guests { get; set; }
    }
}