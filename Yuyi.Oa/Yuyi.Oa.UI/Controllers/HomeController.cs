using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using Yuyi.Oa.BLL;
using Yuyi.Oa.IBLL;
using Yuyi.Oa.Model;

namespace Yuyi.Oa.UI.Controllers
{
    //[LoginCheckFilterAttributer(IsCheck = true)]
    public class HomeController : BaseController
    {
        IUserInfoService userInfoService = new UserInfoService();
        IActionInfoService actionInfoService=new ActionInfoService();
        short NoDel = (short) Model.Enum.DelFlagEnum.NoDel;
        public ActionResult Index()
        {
            /*
             权限管理在此输出link[]
             * 
             var links = [
                { icon: '/images/Home/3DSMAX.png', title: '用户列表', url: '/UserInfo/Index' },
                { icon: '/images/Home/Xp-G5 006.png', title: '角色列表', url: '/RoleInfo/Index' },
                { icon: '/images/Home/Alien Folder.png', title: '权限列表', url: '/ActionInfo/Index' },
                { icon: '/images/Home/Program Files Folder.png', title: '填写周报', url: '/RoleInfo/Index' },
                { icon: '/images/Home/Xp-G5 006.png', title: '订单管理', url: '/RoleInfo/Index' }
                ];
             */
            if (Session["loginUser"] != null)
            {
                UserInfo user = Session["loginUser"] as UserInfo;
                ViewBag.userName = user == null ? "未登录" : user.Name;
                //再执行查询 
                if (user != null)
                {
                    #region 升级版本前代码
                    ////1.根据当前登录用户Id 拿到当前登录用户的实体
                    //var current = userInfoService.GetEntity(u => u.ID == user.ID).FirstOrDefault();
                    ////2.拿到该用户所有的角色
                    //if (current != null)
                    //{
                    //    var allRoles = current.RoleInfo;
                    //    //3.用linq查询 拿到该该角色下所有的权限信息
                    //    var allActions = from roleInfo in allRoles
                    //        from action in roleInfo.ActionInfo
                    //        select action;
                    //    //4.过滤并将其转化为List
                    //    var allActionsList = allActions.Where(u => u.IsMenu==true).ToList();
                    //    //5.设置给ViewBag 前端做拼接
                    //    ViewBag.AllActionList = allActionsList;

                    //} 
                    #endregion
                    #region 老马 加上特殊权限 升级后代码
                    //1.根据当前登录用户Id 拿到当前登录用户的实体
                    var current1 = userInfoService.GetEntity(u => u.ID == user.ID).FirstOrDefault();
                    //2.拿到该用户所有的角色
                    if (current1 != null)
                    {
                        var allRoles1 = current1.RoleInfo;
                        //3.用linq查询 拿到该该角色下所有的权限信息
                        var allActions = (from roleInfo in allRoles1
                                         from action in roleInfo.ActionInfo
                                         select action.ID).ToList();
                        //4.拿到特殊权限中拒绝的权限集合
                        var allRejectActions = (from r in current1.R_User_Action
                            where r.HasPerssion==false
                            select r.ActionInfoID).ToList();
                        //5.两个集合做交集
                        //var actionId = allActions.Where(u=>!allRejectActions.Contains(u)).ToList();
                        var actionId = allActions.Where(u => !allRejectActions.Contains(u)).ToList();
                        //6.查出特殊权限表赋予用户的权限
                        var allActionId = (from r in current1.R_User_Action
                            where r.HasPerssion == true
                            select r.ActionInfoID).ToList();
                        //7.将5和6做并集 并且再去重
                        allActionId.AddRange(actionId.AsEnumerable());
                        //7.1 去重
                        var allActionId1 = allActionId.Distinct();
                        //8.actionInfoService 中
                        ViewBag.AllActionList = actionInfoService.GetEntity(u => allActionId1.Contains(u.ID) && u.DelFlag == NoDel && u.IsMenu == true).ToList();

                    }

                    #endregion

                    #region shit 代码
                    ////处理多对多的关系 先根据用户ID查出来用户的角色 再根据角色查出来对应的权限
                    //var userId = user.ID;
                    //var roles = userInfoService.GetEntity(u => u.ID == userId);


                    //ShowLinkInfo showLink = new ShowLinkInfo();
                    //foreach (var userInfo in roles)
                    //{
                    //    //
                    //    var actionInfos = (from r in userInfo.RoleInfo select r.ActionInfo);


                    //    foreach (var actionInfo in actionInfos)
                    //    {
                    //        var oneActionInfo = actionInfo.FirstOrDefault();
                    //        if (oneActionInfo != null)
                    //        {
                    //            showLink.icon = oneActionInfo.IsIcon;
                    //            showLink.title = oneActionInfo.ActionName;
                    //            showLink.url = oneActionInfo.Url;
                    //            listLinkInfos.Add(showLink);
                    //        }
                    //    }
                    //}
                    //var b = JsonConvert.SerializeObject(listLinkInfos);
                    ////返回权限过滤后的数据
                    //ViewBag.LinksInfo = b; 
                    #endregion
                }

            }


            return View();
        }

        #region 退出系统
        /// <summary>
        /// 退出系统
        /// </summary>
        /// <returns></returns>
        public ActionResult ExitSystem()
        {
            Session.Remove("loginUser");
            return Redirect("/UserLogin/Index");
        }
        #endregion
    }
    
}
