using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Controllers
{
    public class ForumController : Controller
    {
        // GET: Forum
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}