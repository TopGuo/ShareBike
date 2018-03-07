using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using Yuyi.Oa.BLL;
using Yuyi.Oa.IBLL;
using Yuyi.Oa.Model.Param;
using Yuyi.Oa.UI.Models;

namespace Yuyi.Oa.UI.Controllers
{
    public class YiquanInfoController : Controller
    {
        IYiquanInfoService yiquanInfoService = new YiquanInfoService();
        IUserInfoService userInfoService=new UserInfoService();

        #region 渲染首页
        /// <summary>
        /// 渲染首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var userInfos = userInfoService.GetEntity(u => true);
            List<SelectListItem> listItems = new List<SelectListItem>();
            SelectListItem selectListItem = null;
            foreach (var userinfo in userInfos)
            {
                selectListItem = new SelectListItem();
                selectListItem.Text = userinfo.Name;
                selectListItem.Value = userinfo.ID.ToString();
                listItems.Add(selectListItem);
            }


            ViewData["currentUser"] = listItems;
            return View();
        } 
        #endregion

        #region 展示数据
        public ActionResult GetActionInfo()
        {
            //报文中请求自动发送 rows page
            int pageSize = int.Parse(Request["rows"] ?? "5");
            int pageIndex = int.Parse(Request["page"] ?? "1");

            //多条件差寻过滤
            string searchName = Request["seacrchName"];

            var queryParam = new UserQueryParam()
            {
                PageSize = pageSize,
                PageIndex = pageIndex,
                Total = 0,
                SearchName = searchName
            };
            //封装业务逻辑
            var temp = yiquanInfoService.LoadPageData(queryParam);
            var pageData = temp.Select(u => new
            {
                u.ID,
                u.Counts,
                u.DelFlag,
                u.ModifyOn,
                u.SubTime,
                CurrentUser = u.UserInfo.Name,
                u.Remark
            });
            var data = new { total = queryParam.Total, rows = pageData.ToList() };

            //具有导航属性注意 循环序列化

            return Json(data, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region 赠送翼卷

        public ActionResult AddYiquanInfo()
        {
            //先查看这个用户是否有翼卷
            //有在原有基础上加
            //没有新增一条记录
            return Content("ok");
        }

        #endregion

        #region 翼卷记录删除

        public ActionResult Delete(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return Content("请选中要删除的数据！");
            }

            //换成批量删除
            string[] strids = ids.Split(',');
            List<int> idList = new List<int>();
            foreach (string id in strids)
            {
                idList.Add(int.Parse(id));

            }
            int result = yiquanInfoService.DeleteList(idList);
            if (result <= 0)
            {
                return Content("删除失败！");
            }
            return Content("ok");

        }

        #endregion

        #region 返回翼券
        /// <summary>
        /// 返回翼券
        /// </summary>
        /// <returns></returns>
        public ActionResult ReturnYiquan()
        {
            //开启多线程
            //1.查询本月以及前四个月的账单表 对每个月用户充值翼卷的充值金额做一个统计
            Thread getCount = new Thread(Accounts.GetAllAccount);
            getCount.Start();
            //2.开启五十个线程（N个线程）执行返卷任务
            for (var i = 0; i < 50; i++)
            {
                Thread setYiquanToUser =new Thread(Yiquan.SetYiquanToUser);
                setYiquanToUser.Start();
            }
            return Content("ok");
        }

        #endregion


    }
}
