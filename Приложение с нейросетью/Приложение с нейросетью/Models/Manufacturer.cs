using System;
using System.Collections.Generic;
using System.Text;

namespace Network.Models
{
    public class Manufacturer
    {
        public int id { get; set; }
        public string name { get; set; }
        public Manufacturer(string name) {
            this.name = name;
        }
    }
}
