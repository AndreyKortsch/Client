using System;
using System.Collections.Generic;
using System.Text;

namespace Network.Models
{
    class User
    {
        public int id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string[] roles { get; set; }
        public string accessToken { get; set; }
    }
}
