using System;
using System.Collections.Generic;
using System.Text;

namespace Network.Models
{
   public class Model
   {
        public int id { get; set; }
        public string name { get; set; }
        public int count { get; set; }
        public float price { get; set; }
        public string updatedAt { get; set; }
        public string createdAt { get; set; }
        public Model(string name)
            {
                this.name = name;
            }
        public Model(int id,string name,int count,float price)
        {
            this.id = id;
            this.name = name;
            this.count = count;
            this.price = price;
        }
        public Model()
        {
            
            
        }
    }
   
}
