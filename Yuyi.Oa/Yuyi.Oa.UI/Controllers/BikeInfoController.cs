using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Yuyi.Oa.BLL;
using Yuyi.Oa.Common;
using Yuyi.Oa.IBLL;
using Yuyi.Oa.Model;
using Yuyi.Oa.Model.Param;

namespace Yuyi.Oa.UI.Controllers
{
    public class BikeInfoController : BaseController
    {
        IBikeInfoService bikeInfoService=new BikeInfoService();
        short noDel = (short) Model.Enum.DelFlagEnum.NoDel;
        public ActionResult Index()
        {
            return View();
        }

        #region 获取单车信息
        public ActionResult GetBikeInfo()
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
            var pageData = bikeInfoService.LoadPageData(queryParam);

            var data = new { total = queryParam.Total, rows = pageData.ToList() };

            //具有导航属性注意 循环序列化

            return Json(data, JsonRequestBehavior.AllowGet);

        } 
        #endregion

        #region 添加单车

        public ActionResult AddBikeInfo(BikeInfo bikeInfo)
        {
            //计算总价
            var price = bikeInfo.Price;
            var count = bikeInfo.Counts;
            var total = price*count;
            if (Session["loginUser"] != null)
            {
                UserInfo user = Session["loginUser"] as UserInfo;
                //调用生成UID的方法
                bikeInfo.UID = CommonWeb.AutoNumber();
                bikeInfo.AdminUser =user==null?"未登录": user.Name;
                bikeInfo.DelFlag = noDel;
                bikeInfo.SubTime = DateTime.Now;
                bikeInfo.ModifyOn = DateTime.Now;
                bikeInfo.TotalPrice = total;
                if (ModelState.IsValid)
                {
                    bikeInfoService.Add(bikeInfo);
                    return Content("ok");
                }
            }
            return Content("error");

        }

        #endregion

        #region 删除如可记录

        public ActionResult Delete(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return Content("请选中要删除的数据！");
            }
            //换成批量删除
            var strids = ids.Split(',');
            var idList = strids.Select(int.Parse).ToList();

            var result = bikeInfoService.DeleteList(idList);
            return Content(result <= 0 ? "删除失败！" : "ok");
        }

        #endregion

        #region 修改单车信息

        public ActionResult Edit(int id)
        {
            ViewData.Model = bikeInfoService.GetEntity(u => u.ID == id).FirstOrDefault();
            return View();
        }
        [HttpPost]
        public ActionResult Edit(BikeInfo bikeInfo)
        {
            bikeInfoService.Update(bikeInfo);
            return Content("ok");
        }
        #endregion

    }
}
