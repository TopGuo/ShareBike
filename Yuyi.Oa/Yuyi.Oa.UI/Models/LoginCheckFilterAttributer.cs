using System.Web.Mvc;

namespace Yuyi.Oa.UI.Models
{
    
    public class LoginCheckFilterAttributer:ActionFilterAttribute
    {
        public bool IsCheck { get; set; }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            //校验用户是否登录

            //如果需要校验，则校验
            if (IsCheck)
            {
                if (filterContext.HttpContext.Session["loginUser"] == null)
                {
                    filterContext.HttpContext.Response.Redirect("/UserLogin");
                }
            }
        }
    }
}