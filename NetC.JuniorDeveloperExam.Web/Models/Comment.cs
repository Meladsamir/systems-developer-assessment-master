using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NetC.JuniorDeveloperExam.Web.Models
{
    public class Comment
    {
        [JsonProperty("id")]
        public int Id { get; set; }


        [JsonProperty("name")]
        [Required(ErrorMessage = "The Name is required")]
        public string Name { get; set; }

        [JsonProperty("date")]
        [Required]
        public DateTime Date { get; set; }

        [JsonProperty("emailAddress")]
        [Required(ErrorMessage = "The email address is required")]
        [Display (Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EmailAddress { get; set; }

        [JsonProperty("message")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "The Message is required")]
        public string Message { get; set; }

         public List<Reply> Replies { get; set; }
    }
}