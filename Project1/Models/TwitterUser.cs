using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.Models
{

    public class TwitterUser
    {   
        public Data Data { get; set; }
    }

    public class Data
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
    }

}
