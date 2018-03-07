using System.Linq;
using System.Web.Mvc;
using Yuyi.Oa.BLL;
using Yuyi.Oa.IBLL;
using Yuyi.Oa.Model;

namespace Yuyi.Oa.UI.Controllers
{
    public class BaseController : Controller
    {
        public bool IsCheck = true;
        public UserInfo CurrentLoginUserInfo { get; set; }
        public IUserInfoService UserInfoService = new UserInfoService();
        public IActionInfoService ActionInfoService = new ActionInfoService();
        public IR_User_ActionInfoService RUserActionInfoService = new R_User_ActionInfoService();
        //Controller提供用于响应MVC网站进行的Http请求的方法  Controller本身也是一个控制器
        //在当前控制器所有代码执行之前执行此方法
        //以后想做登录校验的控制器，直接继承此基类
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            if (IsCheck)
            {
                //单机
                if (filterContext.HttpContext.Session["loginUser"] == null)
                {
                    filterContext.HttpContext.Response.Redirect("/UserLogin/Index");
                }
                else
                {
                    //将其转化为类
                    UserInfo userInfo = filterContext.HttpContext.Session["loginUser"] as UserInfo;

                    this.CurrentLoginUserInfo = userInfo;
                    //权限过滤 权限校验 
                    //1.看是否直接拒绝或则直接允许
                    //2.根据角色授权
                    if (userInfo != null && userInfo.Name == "admin")
                    {
                        return;//默认不为admin用户做权限校验，admin用户可以畅通无阻
                    }
                    if (Request.Url != null)
                    {
                        var url = Request.Url.AbsolutePath.ToLower();
                        var httpMethod = Request.HttpMethod.ToLower();
                        //查看当前登录用户的Action表是否包含当前请求的url
                        var actionInfo = ActionInfoService.GetEntity(u => u.Url == url && u.HttpMethod == httpMethod).FirstOrDefault();
                        if (actionInfo == null)
                        {
                            //权限表无此数据 跳转错误页
                            Response.Redirect("/ErrorPage.html");
                        }
                        //1.先看特殊权限表
                        var rUsers = RUserActionInfoService.GetEntity(u => u.UserInfoID == userInfo.ID);
                        var item = (from r in rUsers
                                    where r.ActionInfoID == actionInfo.ID
                                    select r).FirstOrDefault();
                        if (item != null && item.HasPerssion == true)
                        {
                            return;
                        }
                        else
                        {
                            //2.根据当前登录用户查看
                            var currentUserInfo = UserInfoService.GetEntity(u => u.ID == userInfo.ID).FirstOrDefault();
                            if (currentUserInfo != null)
                            {
                                var action = from r in currentUserInfo.RoleInfo
                                             from a in r.ActionInfo
                                             select a;
                                var temp = (from info in action
                                            where actionInfo != null && info.ID == actionInfo.ID
                                            select info).Count();

                                if (temp <= 0)
                                {
                                    Response.Redirect("/ErrorPage.html");
                                }
                                else
                                {
                                    return;
                                }
                            }
                            //Response.Redirect("/ErrorPage.html");
                            //return;
                        }
                        
                    }
                }
            }
        
        }

    }
}
