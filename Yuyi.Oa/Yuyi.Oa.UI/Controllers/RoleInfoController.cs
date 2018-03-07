
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
    public class RoleInfoController : BaseController
    {
        IRoleInfoService roleInfoService = new RoleInfoService();
        short NoDel = (short)Model.Enum.DelFlagEnum.NoDel;

        #region 首页加载数据

        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //ViewData.Model = roleInfoService.GetEntity(u => true);
            return View();
        } 
        #endregion

        #region 增加数据
        public ActionResult AddRoleInfo(RoleInfo roleInfo)
        {
            roleInfo.SubTime = DateTime.Now;
            roleInfo.ModifyOn = DateTime.Now;
            roleInfo.DelFlag = NoDel;
            if (ModelState.IsValid)
            {
                roleInfoService.Add(roleInfo);
                return Content("ok");
            }
            return Content("error");

        }
        #endregion

        #region 展示数据
        public ActionResult GetRoleInfo()
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
            var temp = roleInfoService.LoadPageData(queryParam);
            var pageData = temp.Select(u => new
                {
                    u.ID,
                    u.Name,
                    u.SubTime,
                    u.Remark,
                    u.DelFlag
                });
            var data = new { total = queryParam.Total, rows = pageData.ToList() };

            //具有导航属性注意 循环序列化

            return Json(data, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region 删除角色

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
            //    roleInfoService.Delete(new RoleInfo() { ID = int.Parse(id) });
            //} 
            #endregion

            //换成批量删除
            string[] strids = ids.Split(',');
            List<int> idList = new List<int>();
            foreach (string id in strids)
            {
                idList.Add(int.Parse(id));

            }
            int result = roleInfoService.DeleteList(idList);
            if (result <= 0)
            {
                return Content("删除失败！");
            }
            return Content("ok");

        }

        #endregion

        #region 修改角色信息

        public ActionResult Edit(int id)
        {
            ViewData.Model = roleInfoService.GetEntity(u => u.ID == id).FirstOrDefault();
            return View();
        }
        [HttpPost]
        public ActionResult Edit(RoleInfo roleInfo)
        {
            //ID, RoleName, Remark, DelFlag, SubTime, ModifyOn
            string[] array = new string[] { "RoleName", "Remark", "DelFlag", "ModifyOn", "SubTime" };
            roleInfoService.Update(roleInfo);
            return Content("ok");
        }

        #endregion

    }
}
