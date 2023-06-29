using System.Web.Mvc;

namespace WebExam.Controllers
{
    public class BaseController: Controller
    {
        public class CheckLoginViewFilterAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext context)
            {
                var controller = context.Controller as BaseController;
                var filePath = context.HttpContext.Request.FilePath;
                if (filePath.IndexOf("/Home") == -1 && controller.Session["StudentID"] == null)
                {
                    //controller.Session.Abandon();
                    controller.Session["prePath"] = filePath;
                    controller.Session["prePathContentStr"] = "Login to access this content!";
                    context.Result = new RedirectResult("/User/Login");
                }
            }
        }
    }
}