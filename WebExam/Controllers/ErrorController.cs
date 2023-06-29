using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebExam.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index(int id)
        {
            ViewBag.ErrorCode = id;
            return View();
        }
    }
}