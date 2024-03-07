using System;
using System.Collections.Generic;
using System.Text;

namespace Network.Models
{
    public class Goods
    {
        public int id { get; set; }
        public List<Manufacturer> manufacturer { get; set; }
        public List<Subbrand> subbrand { get; set; }
        public string[] model { get; set; }
        public string Class { get; set; }
        public float Price { get; set; }
        public int Count { get; set; }
        public string updatedAt { get; set; }
        public string createdAt { get; set; }
    }
}
