using System;
using System.Collections.Generic;
using System.Text;

namespace Network.Models
{
    public class Manufacturer
    {
        public int id { get; set; }
        public string name { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }

        public int classId { get; set; }

        public Manufacturer(int id,string name) {
            this.id = id;
            this.name = name;
        }
        public Manufacturer(string name)
        {
            this.name = name;
        }
        public Manufacturer()
        {
            
        }
    }
}
