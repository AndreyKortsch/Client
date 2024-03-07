using System;
using System.Collections.Generic;
using System.Text;

namespace Network.Models
{
    public class Subbrand
    {
        public int id { get; set; }
        public string name { get; set; }
        public Subbrand(string name)
        {
            this.name = name;
        }
    }
}
