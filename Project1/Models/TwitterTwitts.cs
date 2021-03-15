using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.Models
{

    public class TwitterTwitts
    {
        public Datum[] Data { get; set; }
        public Meta Meta { get; set; }
    }

    public class Meta
    {
        public string Oldest_id { get; set; }
        public string Newest_id { get; set; }
        public int Result_count { get; set; }
        public string Next_token { get; set; }
    }

    public class Datum
    {
        public DateTime Created_at { get; set; }
        public string Id { get; set; }
        public bool Possibly_sensitive { get; set; }
        public string Text { get; set; }
    }


}
