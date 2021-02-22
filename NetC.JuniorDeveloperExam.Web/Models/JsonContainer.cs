using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetC.JuniorDeveloperExam.Web.Models
{
    public class JsonContainer
    {
        [JsonProperty("blogPosts")]
      public List<Post> Posts { get; set; }
    }
}