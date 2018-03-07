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
    public class ActionInfoController : BaseController
    {
        IActionInfoService actionInfoService=new ActionInfoService();
        IRoleInfoService roleInfoService=new RoleInfoService();
        short NoDel = (short)Model.Enum.DelFlagEnum.NoDel;
        #region 首次加载
        /// <summary>
        /// 渲染
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        } 
        #endregion

        #region 增加数据
        /// <summary>
        /// 添加数据 注意对客户端数据经行验证
        /// </summary>
        /// <param name="actionInfo"></param>
        /// <returns></returns>
        public ActionResult AddActionInfo(ActionInfo actionInfo)
        {
            
            actionInfo.SubTime = DateTime.Now;
            actionInfo.ModifyOn = DateTime.Now;
            actionInfo.DelFlag = NoDel;
            actionInfo.IsMenu = Request["IsMenu"] == "true" ? true : false;
            if (actionInfoService.Add(actionInfo))
            {
                return Content("ok");
            }
            else
            {
                return Content("error");
            }
            
            
        }
        #endregion

        #region 删除权限

        public ActionResult Delete(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return Content("请选中要删除的数据！");
            }
            //拆分接收过来的ids
            string[] strids = ids.Split(',');

            //完成删除
            //1.清除该权限所关联的角色
            foreach (var strid in strids)
            {
                int delActionId = int.Parse(strid);
                var actionInfo = actionInfoService.GetEntity(u => u.ID == delActionId).FirstOrDefault();
                if (actionInfo != null)
                {
                    actionInfo.RoleInfo.Clear();//清除掉原有的角色
                }
            }
            //2.执行删除
            List<int> idList = strids.Select(int.Parse).ToList();

            int result = actionInfoService.DeleteList(idList);
            if (result <= 0)
            {
                return Content("error");
            }
            return Content("ok");


            //-------------------------
            //List<int> idList = strids.Select(int.Parse).ToList();

            //int result = actionInfoService.DeleteList(idList);
            //if (result <= 0)
            //{
            //    return Content("删除失败！");
            //}
            //return Content("ok");

        }

        #endregion

        #region 查询数据
        public ActionResult GetActionInfo()
        {
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
            var temp = actionInfoService.LoadPageData(queryParam);

            //具有导航属性注意 循环序列化
            var pageData = temp.Select(u => new { u.ID, u.ModifyOn, u.Remark, u.SubTime, u.ActionName, u.HttpMethod, u.Sort, u.Url, u.IsIcon,u.IsMenu });

            var data = new { total = queryParam.Total, rows = pageData.ToList() };

            return Json(data, JsonRequestBehavior.AllowGet);    
        }
        #endregion

        #region 图片上传处理

        public ActionResult UploadImg()
        {
            var file = Request.Files["fileMenuIcon"];
            if (file==null)
            {
                return Content("error");
            }
            string path = "/UploadFiles/UploadImg/" + Guid.NewGuid().ToString() + "-" + file.FileName;
            file.SaveAs(Request.MapPath(path));
            return Content(path);
        }
        #endregion

        #region 修改数据

        public ActionResult Edit(int id)
        {
            ViewData.Model = actionInfoService.GetEntity(u => u.ID == id).FirstOrDefault();
            return View();
        }
        [HttpPost]
        public ActionResult Edit(ActionInfo actionInfo)
        {
            
            //ID, Url, SubTime, HttpMethod, Remark, ModifyOn, ActionName, IsMenu, Sort, DelFlag, IconImg
            string[] array = new string[] { "Url", "SubTime", "HttpMethod", "Remark", "ModifyOn", "ActionName", "IsMenu", "Sort", "DelFlag", "IconImg" };
            actionInfoService.Update(actionInfo);
            if (actionInfoService.Update(actionInfo))
            {
                return Content("ok");
            }
            else
            {
                return Content("error");
            } 
        }

        #endregion

        #region 给权限设置角色
        /// <summary>
        /// 设置权限角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult SetRole(int id)
        {
            int userid = id;
            ActionInfo actionInfo = actionInfoService.GetEntity(u => u.ID == userid).FirstOrDefault();
            //返回所有角色
            ViewBag.AllRoles = roleInfoService.GetEntity(u => u.DelFlag == NoDel).ToList();
            //将所有关联角色传到cshtml
            //linq 语法
            if (actionInfo != null)
            {
                ViewBag.ExitsRoles = (from r in actionInfo.RoleInfo select r.ID).ToList();
            }
            return View(actionInfo);
        }
        #endregion

        #region 将权限和角色关联

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
            actionInfoService.SetRole(UId, roleIdList);
            return Content("ok");

        }

        #endregion



    }
}
