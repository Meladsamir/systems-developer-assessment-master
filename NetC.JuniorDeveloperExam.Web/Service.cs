using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using NetC.JuniorDeveloperExam.Web.Models;
using Newtonsoft.Json;

namespace NetC.JuniorDeveloperExam.Web
{
    public class Service
    {
        /// <summary>
        /// get post by postId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Post GetPost(int? id)
        {
            //load data from file
            var fileData = ReadAll();

            //check if the data is not null
            if (fileData != null)
            {
                //get posts 
                List<Post> Posts = fileData.Posts;

                //check if threre any post
                if (Posts.Any())
                {
                    // return the required pos
                    return Posts.Where(p => p.Id == id).FirstOrDefault();
                }
            }
            return null;
        }

        /// <summary>
        /// Add a new comment to a post
        /// </summary>
        /// <param name="comment"></param>
        /// <param name="postId"></param>
        /// <returns></returns>
        public bool AddComment(Comment comment, int postId)
        {
            //read all data from file
            JsonContainer fileData = ReadAll();

            //check if the data is not null
            if (fileData != null)
            {
                var post = fileData.Posts.Where(p => p.Id == postId).First();
                var comments = post.Comments;

                if (comments == null) comments = new List<Comment>();
                
                    comment.Id = comments.Count + 1;
                
                comment.Date = DateTime.Now;
                //add comment to comments list
                comments.Add(comment);

                //set new list
                post.Comments = comments;
                //write new list to the file      
                var newPostsList = fileData.Posts.Where(p => p.Id != postId).ToList();
                newPostsList.Add(post);
                return WriteAll(new JsonContainer() { Posts = newPostsList });
            }
            return false;
        }

        /// <summary>
        /// Add reply to a comment
        /// </summary>
        /// <param name="reply"></param>
        /// <param name="commentId"></param>
        /// <param name="postId"></param>
        /// <returns></returns>
        public bool AddReply(Reply reply, int commentId, int postId)
        {
            //read all data from file
            JsonContainer fileData = ReadAll();

            //check if the data is not null
            if (fileData != null)
            {
                var post = fileData.Posts.Where(p => p.Id == postId).First();
                var comment = post.Comments.Where(p => p.Id == commentId).First();
                //if replies ==null create new empty list 
                if (comment.Replies == null) comment.Replies = new List<Reply>();

                reply.Date = DateTime.Now;
                //add reply to replies list
                comment.Replies.Add(reply);

               post.Comments.Where(c=>c.Id==commentId).First().Replies = comment.Replies;
              var newPostsList=  fileData.Posts.Where(p => p.Id != postId).ToList();
                newPostsList.Add(post);
                //write new list to the file      
                return WriteAll(new JsonContainer() { Posts = newPostsList });
            }
            return false;
        }

        /// <summary>
        /// Read all data from json file 
        /// </summary>
        /// <returns>JsonContainer</returns>
        private JsonContainer ReadAll()
        {
            try
            {

                using (StreamReader sr = new StreamReader(HttpContext.Current.Server.MapPath(Constants.JsonFilePath)))
                {
                    return JsonConvert.DeserializeObject<JsonContainer>(sr.ReadToEnd());
                }
            }
            catch
            {
                return null;
            }

        }

        /// <summary>
        /// write data into json file
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool WriteAll(JsonContainer data)
        {
            try
            {
                // serialize JSON directly to a file
                using (StreamWriter file = new StreamWriter(HttpContext.Current.Server.MapPath(Constants.JsonFilePath)))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, data);
                    return true;
                }

            }
            catch
            {
                return false;
            }

        }
    }
}