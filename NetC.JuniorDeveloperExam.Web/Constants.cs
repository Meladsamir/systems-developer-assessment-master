using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetC.JuniorDeveloperExam.Web
{
    public static class Constants
    {
        public const string JsonFilePath = "~/App_Data/Blog-Posts.json";
        public const string PostNotFoundErrorMsg = "we did not find a post with id";
        public const string EnterValidUrlErrorMsg = "enter a valid url";
        public const string ValidUrlExamples = "Example: /blog/1/, /blog/2/, /blog/<ID>/ etc....";
        public const string Done = "done!";
        public const string ErrorNotDone = "error:The process wasn't completed!";
    }
}