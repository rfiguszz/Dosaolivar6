using System;
using System.Collections.Generic;

namespace SmartHotel.Clients.Core.Models
{
    public class GenericServiceAnswer
    {
        public string new_id { get; set; }

        public string result { get; set; }

        public string type { get; set; }

        public string message { get; set; }

        public bool success { get; set; }
    }
}