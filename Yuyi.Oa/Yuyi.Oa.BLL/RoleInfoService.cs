using System.Linq;
using Yuyi.Oa.IBLL;
using Yuyi.Oa.Model;
using Yuyi.Oa.Model.Param;

namespace Yuyi.Oa.BLL
{
    public class RoleInfoService : BaseService<RoleInfo>, IRoleInfoService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = DbSession.GetRoleInfoDal;
        }
        #region 多条件查询封装
        /// <summary>
        /// 实现多条件查询
        /// </summary>
        /// <param name="userQueryParam">查询参数</param>
        /// <returns></returns>
        public IQueryable<RoleInfo> LoadPageData(UserQueryParam userQueryParam)
        {
            short noDel = (short)Model.Enum.DelFlagEnum.NoDel;
            //查询出所有没有删除的数据
            var temp = CurrentDal.GetEntity(u => u.DelFlag == noDel);
            //判断查询名字是否为空
            if (!string.IsNullOrEmpty(userQueryParam.SearchName))
            {
                temp = temp.Where(u => u.Name.Contains(userQueryParam.SearchName));
            }
            //在此处拼接多个条件 返回temp 可以写多个if 获取参数

            var pageSize = userQueryParam.PageSize;
            var pageIndex = userQueryParam.PageIndex - 1;
            //分页  记得先排序
            var roleInfos = temp.OrderBy(u => u.ID)
                .Skip(pageSize * pageIndex)
                .Take(pageSize);
            //查询到数据总条数
            userQueryParam.Total = temp.Count();
            return roleInfos;
        } 
        #endregion
    }
}