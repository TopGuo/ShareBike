using System.Collections.Generic;
using System.Linq;
using Yuyi.Oa.IBLL;
using Yuyi.Oa.Model;
using Yuyi.Oa.Model.Param;

namespace Yuyi.Oa.BLL
{
    public class ActionInfoService:BaseService<ActionInfo>,IActionInfoService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = DbSession.GetActionInfoDal;
        }

        #region 多条件查询封装
        /// <summary>
        /// 实现多条件查询
        /// </summary>
        /// <param name="userQueryParam">查询参数</param>
        /// <returns></returns>
        public IQueryable<ActionInfo> LoadPageData(UserQueryParam userQueryParam)
        {
            short noDel = (short)Model.Enum.DelFlagEnum.NoDel;
            //查询出所有没有删除的数据
            var temp = CurrentDal.GetEntity(u => u.DelFlag == noDel);
            //判断查询名字是否为空
            if (!string.IsNullOrEmpty(userQueryParam.SearchName))
            {
                temp = temp.Where(u => u.ActionName.Contains(userQueryParam.SearchName));
            }
            //在此处拼接多个条件 返回temp 可以写多个if 获取参数

            var pageSize = userQueryParam.PageSize;
            var pageIndex = userQueryParam.PageIndex - 1;
            //分页  记得先排序
            var actionInfos = temp.OrderBy(u => u.ID)
                .Skip(pageSize * pageIndex)
                .Take(pageSize);
            //查询到数据总条数
            userQueryParam.Total = temp.Count();
            return actionInfos;
        }
        #endregion

        #region 为权限设置角色
        /// <summary>
        /// 实现设置角色
        /// </summary>
        /// <param name="actionId">权限id</param>
        /// <param name="roleIdList">权限所拥有角色</param>
        /// <returns></returns>
        public bool SetRole(int actionId, List<int> roleIdList)
        {
            //更具id权限信息
            var action = DbSession.GetActionInfoDal.GetEntity(u => u.ID == actionId).FirstOrDefault();
            if (action == null)
            {
                return false;
            }
            action.RoleInfo.Clear();//清除掉原有的角色，统一加上
            var allRole = DbSession.GetRoleInfoDal.GetEntity(u => roleIdList.Contains(u.ID));

            foreach (var roleInfo in allRole)
            {
                action.RoleInfo.Add(roleInfo);//统一加上
            }
            return DbSession.SaveChanges() > 0;
        }
        #endregion
    }
}