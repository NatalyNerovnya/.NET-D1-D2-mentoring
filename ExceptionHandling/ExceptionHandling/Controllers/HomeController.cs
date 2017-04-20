using System.Web.Mvc;
using BadServise;
using BadServise.Exceptions;
using ExceptionHandling.ErrorHandlers;

namespace ExceptionHandling.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HandleError(ExceptionType = typeof(AttributeException), View = "AttributeError")]
        public ActionResult AttributeError()
        {
            ExceptionService.AttributeException();
            return Index();
        }

        [CustomHandleError(View = "CustomAttributeError")]
        public ActionResult CustomHandleError()
        {
            ExceptionService.CustomAttributeException();
            return Index();
        }

        public ActionResult ControllerError()
        {
            ExceptionService.ControllerLevelException();
            return Index();
        }

        public ActionResult GlobalError()
        {
            ExceptionService.GlobalException();
            return Index();
        }

        public ActionResult AnyError()
        {
            throw new AttributeException("Any unhandled error");
        }

        public ActionResult LocalError()
        {
            try
            {
                ExceptionService.LocalException();
            }
            catch (LocalException ex)
            {
                return View();
            }
            return Index();
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception is ControllerLevelException)
            {
                string controller = filterContext.RouteData.Values["controller"].ToString();
                string action = filterContext.RouteData.Values["action"].ToString();
                HandleErrorInfo model = new HandleErrorInfo(filterContext.Exception, controller, action);
                filterContext.Result = new ViewResult
                {
                    ViewName = "ControllerError",
                    ViewData = new ViewDataDictionary<HandleErrorInfo>(model),
                    TempData = filterContext.Controller.TempData
                };

                filterContext.ExceptionHandled = true;
            }
        }
    }
}
