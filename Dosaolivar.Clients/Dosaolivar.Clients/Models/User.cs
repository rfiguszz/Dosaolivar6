
using System;

namespace SmartHotel.Clients.Core.Models
{
    public class User
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string username { get; set; }
        public bool success { get; set; }
        public string visible_name { get; set; }
        public string profile_image { get; set; }
        public string token { get; set; }
        public string password { get; set; }
        public string confirm_password { get; set; }
        public string message { get; set; }
        public string location { get; set; }
    }
}
