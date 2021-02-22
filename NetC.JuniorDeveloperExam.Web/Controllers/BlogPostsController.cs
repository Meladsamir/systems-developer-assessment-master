using NetC.JuniorDeveloperExam.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NetC.JuniorDeveloperExam.Web.Controllers
{
    public class BlogPostsController : Controller
    {
        private readonly Service _service;
        public BlogPostsController()
        {
            _service = new Service();
        }

        public ActionResult Home()
        {
            ViewBag.Message = $"{Constants.EnterValidUrlErrorMsg}\n {Constants.ValidUrlExamples}";
            return View("PostNotFound");
        }
        // GET
        [Route("blog/{id}")]
        public ActionResult Blog(int? id)
        {
            if (id != null)
            {
                var post = _service.GetPost(id);
                if (post != null) return View(post);
                //if there is no post with this id add viewbag with error message
                ViewBag.Message = $"{Constants.PostNotFoundErrorMsg}:{id}";
            }
            else
            {
                //if no id entered show to user example of valid urls
                ViewBag.Message = $"{Constants.EnterValidUrlErrorMsg}\n {Constants.ValidUrlExamples}";
            }
            return View("PostNotFound");

        }

        [HttpPost]
        public ActionResult PostComment(Comment comment)
        {
            int postId = Convert.ToInt32(TempData["PostId"]);
            //add comment
            if (_service.AddComment(comment, postId))
            {
                TempData["msg"] = Constants.Done;
                return RedirectToAction("Blog", new { id = postId });
            }
            TempData["msg"] = Constants.ErrorNotDone;
            return RedirectToAction("Blog");
        }

        public ActionResult Reply(int postId, int commentId)
        {
            TempData["PostId"] = postId;
            TempData["CommentId"] = commentId;
            return View();
        }

        [HttpPost]
        public ActionResult Replay(Reply reply, int postId, int commentId)
        {
            //add reply
            if (_service.AddReply(reply, commentId, postId))
            {
                TempData["msg"] = Constants.Done;
                return RedirectToAction("Blog", new { id = postId });
            }

            TempData["msg"] = Constants.ErrorNotDone;
            return RedirectToAction("Blog");
        }
    }
}