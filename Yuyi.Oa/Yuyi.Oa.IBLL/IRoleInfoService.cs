using System.Collections.Generic;
using System.Linq;
using Yuyi.Oa.Model;
using Yuyi.Oa.Model.Param;

namespace Yuyi.Oa.IBLL
{
    public interface IRoleInfoService:IBaseService<RoleInfo>
    {
        #region 角色多条件查询
        /// <summary>
        /// 多条件查询业务逻辑封装
        /// </summary>
        /// <param name="userQueryParam">传入参数条件</param>
        /// <returns></returns>
        IQueryable<RoleInfo> LoadPageData(UserQueryParam userQueryParam); 
        #endregion
    }
}