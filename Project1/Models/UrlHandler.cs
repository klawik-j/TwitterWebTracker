using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.Models
{
    public class UrlHandler
    {
        static public string makeUserUrl(string _TwitterUserName)
        {
            string url = "";
            if (string.IsNullOrWhiteSpace(_TwitterUserName))
            {
                throw new ArgumentException("Username was not passed");
            }
            else
            {
                url = $"https://api.twitter.com/2/users/by/username/{ _TwitterUserName }?user.fields=profile_image_url";
                return url;
            }
        }
    }
}
