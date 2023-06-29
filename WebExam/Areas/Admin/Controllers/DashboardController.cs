using System.Web.Mvc;
using WebExam.Commons;
using WebExam.DB;

namespace WebExam.Areas.Admin.Controllers
{
    public class DashboardController : BaseController
    {
        // GET: Dasboard
        [CheckLoginViewFilter]
        public ActionResult Index()
        {
            return View();
        }



        [ChildActionOnly]
        public ActionResult RenderMenu()
        {
            int errCode = Message.OK;
            var dbMgr = new DBMgr();

            var sub = dbMgr.SubjectAccess.Get(out errCode);

            if (errCode != Message.OK)
            {
                TempData["DisplayMessage"] = "Something went wrong!";
            }

            ViewBag.subject = sub;

            return PartialView("_Navbar");
        }
    }
}
