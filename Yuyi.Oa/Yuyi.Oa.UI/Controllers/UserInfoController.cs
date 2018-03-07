
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Yuyi.Oa.BLL;
using Yuyi.Oa.IBLL;
using Yuyi.Oa.Model;
using Yuyi.Oa.Model.Param;

namespace Yuyi.Oa.UI.Controllers
{
    public class UserInfoController : BaseController
    {
        IUserInfoService userInfoService = new UserInfoService();
        IRoleInfoService roleInfoService = new RoleInfoService();
        IActionInfoService actionInfoService = new ActionInfoService();
        IR_User_ActionInfoService iRUserActionService = new R_User_ActionInfoService();
        short NoDel = (short)Model.Enum.DelFlagEnum.NoDel;
        short YseDel = (short)Model.Enum.DelFlagEnum.YesDel;

        #region 初次请求
        /// <summary>
        /// 渲染
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //ViewData.Model = userInfoService.GetEntity(u => true);
            return View();
        }
        #endregion

        #region 添加用户数据
        public ActionResult AddUserInfo(UserInfo userInfo)
        {
            //非空验证

            //赋值操作AddUserInfo
            userInfo.SubTime = DateTime.Now;
            userInfo.ModifyOn = DateTime.Now;
            userInfo.DelFlag = (short)Model.Enum.DelFlagEnum.NoDel;
            if (ModelState.IsValid)
            {
                userInfoService.Add(userInfo);
            }
            return Content("ok");
        }
        #endregion

        #region 获取用户数据
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <returns></returns>
        public ActionResult GetUserInfo()
        {
            //jQuery easy UI  table：{total：32，rows:[{},{}]}
            //报文中请求自动发送 rows page
            int pageSize = int.Parse(Request["rows"] ?? "10");
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
            var temp = userInfoService.LoadPageData(queryParam);

            //具有导航属性注意 循环序列化
            var pageData = temp.Select(u => new { u.ID, u.Name, u.Pwd, u.ShowName, u.DelFlag, u.SubTime, u.Remark });

            var data = new { total = queryParam.Total, rows = pageData.ToList() };

            return Json(data, JsonRequestBehavior.AllowGet);

        }

        #endregion

        #region 删除用户信息

        public ActionResult Delete(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return Content("请选中要删除的数据！");
            }
            #region shit删除
            ////拆分接收过来的ids
            //string[] strids = ids.Split(',');
            //foreach (string id in strids)
            //{
            //    userInfoService.Delete(new UserInfo() { ID = int.Parse(id) });

            //} 
            #endregion

            //换成批量删除
            string[] strids = ids.Split(',');
            #region 上一句代码可以代替下面的foreach代码
            //List<int> idList = strids.Select(int.Parse).ToList();
            //List<int> idList = new List<int>();
            //foreach (string id in strids)
            //{
            //    idList.Add(int.Parse(id));
            //} 
            #endregion
            //完成删除
            //1.清楚该用户的角色关联
            foreach (var strid in strids)
            {
                int delUserId = int.Parse(strid);
                var userInfo = userInfoService.GetEntity(u => u.ID == delUserId).FirstOrDefault();
                if (userInfo != null)
                {
                    //userInfo.R_User_Action.Clear();
                    userInfo.RoleInfo.Clear();//清除掉原有的角色
                }
            }
            //2.执行删除
            List<int> idList = strids.Select(int.Parse).ToList();

            int result = userInfoService.DeleteList(idList);
            if (result <= 0)
            {
                return Content("error");
            }
            return Content("ok");

        }
        #endregion

        #region 修改用户数据

        public ActionResult Edit(int id)
        {
            ViewData.Model = userInfoService.GetEntity(u => u.ID == id).FirstOrDefault();
            return View();
        }
        [HttpPost]
        public ActionResult Edit(UserInfo userInfo)
        {
            //ID, UName, Pwd, ShowName, DelFlag, Remark, ModifyOn, SubTime
            string[] array = new string[] { "UName", "Pwd", "ShowName", "DelFlag", "Remark", "ModifyOn", "SubTime" };

            userInfoService.Update(userInfo);
            return Content("ok");
        }
        #endregion

        #region 设置角色
        public ActionResult SetRole(int id)
        {
            int userid = id;
            UserInfo userInfo = userInfoService.GetEntity(u => u.ID == userid).FirstOrDefault();

            //将所有角色传到cshtml
            ViewBag.AllRoles = roleInfoService.GetEntity(u => u.DelFlag == NoDel).ToList();
            //将所有关联角色传到cshtml
            //linq 语法
            if (userInfo != null)
            {
                ViewBag.ExitsRoles = (from r in userInfo.RoleInfo select r.ID).ToList();
            }
            return View(userInfo);
        }
        #endregion

        #region 将用户和角色关联

        public ActionResult ProcessSetRole(int UId)
        {
            //拿到当前用户
            //得到所用打钩的角色
            List<int> roleIdList = new List<int>();
            //小技巧
            foreach (var key in Request.Form.AllKeys)
            {
                if (key.StartsWith("ckb_"))
                {
                    int roleId = int.Parse(key.Replace("ckb_", ""));
                    roleIdList.Add(roleId);
                }
            }
            if (userInfoService.SetRole(UId, roleIdList))
            {
                return Content("ok");
            }
            else
            {
                return Content("error");
            }
        }
        #endregion

        #region 设置特殊权限

        public ActionResult SetAction(int id)
        {
            var user = userInfoService.GetEntity(u => u.ID == id).FirstOrDefault();
            ViewBag.userInfo = user;
            //查出当前权限表里有哪些权限
            var allActions = actionInfoService.GetEntity(u => u.DelFlag == NoDel);
            ViewData.Model = allActions;
            //关联表
            //ViewBag.ActionUserInfo= iRUserActionService.GetEntity(u => u.UserInfoID == id);
            return View();
        }

        #endregion

        #region 删除特殊权限
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uId"></param>
        /// <param name="actionId"></param>
        /// <returns></returns>
        public ActionResult DeleteUserAction(int uId,int actionId)
        {
            var rUserAction = iRUserActionService.GetEntity(u => u.UserInfoID == uId && u.ActionInfoID == actionId).FirstOrDefault();
            if (rUserAction!=null)
            {
                rUserAction.DelFlag = YseDel;
                //软删除 修改状态
               var a= iRUserActionService.Update(rUserAction);
            }
            return Content("ok");
        }
        #endregion

        #region 特殊权限设置(允许\禁止)
        /// <summary>
        /// 允许拒绝
        /// </summary>
        /// <param name="uId"></param>
        /// <param name="actionId"></param>
        /// <param name="valueInfo"></param>
        /// <returns></returns>
        public ActionResult SetUserAction(int uId, int actionId, int valueInfo)
        {
            //1.先查中间表
            var rUserActionInfo = iRUserActionService.GetEntity(u => u.UserInfoID == uId && u.ActionInfoID == actionId).FirstOrDefault();
            if (rUserActionInfo != null)
            {
                //如果valueInfo==1 true,else false
                rUserActionInfo.HasPerssion = valueInfo == 1 ? true : false;
                iRUserActionService.Update(rUserActionInfo);
            }
            else
            {
                R_User_ActionInfo useractionInfo = new R_User_ActionInfo();
                useractionInfo.ActionInfoID = actionId;
                useractionInfo.UserInfoID = uId;
                useractionInfo.HasPerssion = valueInfo == 1 ? true : false;
                useractionInfo.DelFlag = NoDel;
                iRUserActionService.Add(useractionInfo);
            }
            return Content("ok");

        } 
        #endregion
    }
}
