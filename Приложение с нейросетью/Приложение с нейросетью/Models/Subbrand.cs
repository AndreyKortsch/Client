using System;
using System.Collections.Generic;
using System.Text;

namespace Network.Models
{
    public class Subbrand
    {
        public int id { get; set; }
        public string name { get; set; }
        public string updatedAt { get; set; }
        public string createdAt { get; set; }
        public Subbrand(int id,string name)
        {
            this.id = id;
            this.name = name;
        }
        public Subbrand(string name)
        {
            this.name = name;
        }
        public Subbrand()
        {
          
        }
    }
}
