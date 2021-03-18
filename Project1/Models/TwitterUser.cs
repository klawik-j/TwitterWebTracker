using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.Models
{

    public class TwitterUser
    {   
        public Data Data { get; set; }
        public Error[] Errors { get; set; }
        public TwitterUser()
        {
            Data = new Data();
            Errors = Array.Empty<Error>();
        }
    }

    public class Data
    {
        public Data()
        {
            Id = null;
            Name = null;
            Username = null;
            Profile_image_url = null;
        }
        public string? Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Profile_image_url { get; set; }
    }
    public class Error
    {
        public Error()
        {
            Detail = null;
            Title = null;
            Resource_type = null;
            Parameter = null;
            Value = null;
            Type = null;
        }
        public string Detail { get; set; }
        public string Title { get; set; }
        public string Resource_type { get; set; }
        public string Parameter { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
    }

}
