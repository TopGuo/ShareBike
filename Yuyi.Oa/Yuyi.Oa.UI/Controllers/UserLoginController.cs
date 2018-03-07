using System.Linq;
using System.Web.Mvc;
using Yuyi.Oa.BLL;
using Yuyi.Oa.IBLL;

namespace Yuyi.Oa.UI.Controllers
{
    public class UserLoginController : BaseController
    {
        readonly IUserInfoService userInfoService = new UserInfoService();
        /// <summary>
        /// 取消登录验证
        /// </summary>
        public UserLoginController()
        {
            IsCheck = false;//如果登录页面继承了校验基类 但不想进行校验   将Ischeck设为false
        }
        //
        // GET: /UserLogin/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProcLogin()
        {
            //1 拿到表单中的验证码
            string strCode = Request["txtValidate"];

            //2 拿到session中的验证码
            string sessionCode = Session["code"] as string;
            //解决一个bug
            Session["code"] = null;
            if (string.IsNullOrEmpty(sessionCode) || strCode != sessionCode)
            {
                return Content("验证码错误！");
            }
            else
            {
                //3 拿到前台用户名密码
                string name = Request["txtLoginName"];
                string password = Request["txtLoginPwd"];
                short noDel = (short)Model.Enum.DelFlagEnum.NoDel;
                //3 验证用户名 密码
                var userInfo = userInfoService.GetEntity(u => u.Name == name && u.Pwd == password&&u.DelFlag==noDel).FirstOrDefault();

                if (userInfo == null)//没有查到数据
                {
                    return Content("用户名或密码错误！");
                }
                else
                {
                    //单机模式
                    Session["loginUser"] = userInfo;
                    //mm+cookie
                    //string userLoginId = Guid.NewGuid().ToString();

                    //Common.Cache.CacheHelper.AddCache(userLoginId, userInfo, DateTime.Now.AddMinutes(20));//设置滑动过期时间

                    //Response.Cookies["userLoginId"].Value = userLoginId;

                    return Content("ok");
                }
            }
        }

        #region 处理验证码
        public ActionResult ShowVCode()
        {
            Common.ValidateCode validateCode = new Common.ValidateCode();

            string code = validateCode.CreateValidateCode(4);//生成验证码，传几就是几位验证码
            Session["code"] = code;
            byte[] buffer = validateCode.CreateValidateGraphic(code);//把验证码画到画布

            return File(buffer, "image/jpeg");

        }
        #endregion

    }
}
