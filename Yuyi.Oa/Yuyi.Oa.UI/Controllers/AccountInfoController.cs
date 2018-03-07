using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yuyi.Oa.BLL;
using Yuyi.Oa.IBLL;
using Yuyi.Oa.Model;
using Yuyi.Oa.Model.Param;

namespace Yuyi.Oa.UI.Controllers
{
    public class AccountInfoController : BaseController
    {
        IAccountInfoService accountInfoService=new AccountInfoService();
        IUserInfoService userInfoService=new UserInfoService();
        IYiquanInfoService yiquanInfoService=new YiquanInfoService();
        short noDel = (short) Model.Enum.DelFlagEnum.NoDel;

        #region 渲染主页
        /// <summary>
        /// 渲染主页
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
            var temp = accountInfoService.LoadPageData(queryParam);
            var pageData = temp.Select(u => new
            {
                u.ID,
                u.Money,
                u.SubTime,
                u.Remark,
                u.DelFlag,
                AdminUser= u.AdminUser,
                Name=u.UserInfo.Name
            });
            var data = new { total = queryParam.Total, rows = pageData.ToList() };

            //具有导航属性注意 循环序列化

            return Json(data, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region 删除账户

        public ActionResult Delete(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return Content("请选中要删除的数据！");
            }
            ////先根据ids查处要删除的用户，再清除其下的翼卷
            //var data = accountInfoService.GetEntity(u => ids.Contains(u.ID.ToString()));

            //var userId = (from r in data
            //    select r.UserInfoID).ToList();
            //yiquanInfoService.DeleteList(userId);
            //return Content("error");
            //换成批量删除
            string[] strids = ids.Split(',');
            List<int> idList = new List<int>();
            foreach (string id in strids)
            {
                idList.Add(int.Parse(id));

            }
            int result = accountInfoService.DeleteList(idList);
            if (result <= 0)
            {
                return Content("删除失败！");
            }
            return Content("ok");

        }

        #endregion

        #region 充值
        /// <summary>
        /// 充值翼卷
        /// </summary>
        /// <param name="accountInfo"></param>
        /// <returns></returns>
        public ActionResult AddAccountInfo(AccountInfo accountInfo)
        {
            accountInfo.DelFlag = noDel;
            accountInfo.SubTime=DateTime.Now;
            accountInfo.ModifyOn=DateTime.Now;
            //接收当前传过来的充值用户
            var currentUserInfoId = Request["currentUser"];
            accountInfo.UserInfoID = int.Parse(currentUserInfoId);
            UserInfo user = Session["loginUser"] as UserInfo;
            if (user != null) accountInfo.AdminUser=user.Name;

            if (ModelState.IsValid)
            {
                accountInfoService.Add(accountInfo);
                var count = int.Parse(accountInfo.Money.ToString());
                //对应添加翼卷
                if (AddYiquan(accountInfo.UserInfoID, count))
                {
                    return Content("ok");
                }
                return Content("error");
                
            }
            return Content("error");
        }

        #endregion

        #region 添加翼卷

        public bool AddYiquan(int userId,int counts)
        {
            YiquanInfo yiquanInfo=new YiquanInfo();
            yiquanInfo.UserInfoID = userId;
            yiquanInfo.Counts = counts;
            yiquanInfo.DelFlag = noDel;
            yiquanInfo.ModifyOn=DateTime.Now;
            yiquanInfo.SubTime=DateTime.Now;
            if (yiquanInfoService.Add(yiquanInfo))
            {
                return true;
            }
            return false;
        }

        #endregion

    }
}
