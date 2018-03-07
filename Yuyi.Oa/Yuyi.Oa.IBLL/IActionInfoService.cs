using System.Collections.Generic;
using System.Linq;
using Yuyi.Oa.Model;
using Yuyi.Oa.Model.Param;

namespace Yuyi.Oa.IBLL
{
    public interface IActionInfoService:IBaseService<ActionInfo>
    {
        #region 权限多条件查询
        /// <summary>
        /// 多条件查询业务逻辑封装
        /// </summary>
        /// <param name="userQueryParam">传入参数条件</param>
        /// <returns></returns>
        IQueryable<ActionInfo> LoadPageData(UserQueryParam userQueryParam);
        #endregion

        /// <summary>
        /// 设置角色
        /// </summary>
        /// <param name="actionId">权限id</param>
        /// <param name="roleIdList">权限所拥有角色</param>
        /// <returns></returns>
        bool SetRole(int actionId, List<int> roleIdList);
    }
}