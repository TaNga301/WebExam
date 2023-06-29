using System.Web.Mvc;

namespace WebExam.Areas.Admin.Controllers
{
    public class BaseController: Controller
    {
        public class CheckLoginViewFilterAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext context)
            {
                var controller = context.Controller as BaseController;
                if (controller.Session["RoleID"] == null)
                {
                    controller.Session.Abandon();
                    context.Result = new RedirectResult("/Admin/Employee/Login");
                }
                else
                {
                    var filePath = context.HttpContext.Request.FilePath;
                    if ((filePath.IndexOf("/Question") == -1 && filePath.IndexOf("/Exam") == -1 && filePath.IndexOf("/") == -1) && (int)controller.Session["RoleID"] != 2)
                    {
                        context.Result = new RedirectResult("/Admin/Dashboard");
                    }
                }
            }
        }
    }
}