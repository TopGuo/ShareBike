using System.Collections.Generic;
using System.Linq;
using Yuyi.Oa.Model;
using Yuyi.Oa.Model.Param;

namespace Yuyi.Oa.IBLL
{
    /// <summary>
    /// 接口封装
    /// </summary>
    public interface IUserInfoService:IBaseService<UserInfo>
    {
        /// <summary>
        /// 多条件查询业务逻辑封装
        /// </summary>
        /// <param name="userQueryParam">传入参数条件</param>
        /// <returns></returns>
        IQueryable<UserInfo> LoadPageData(UserQueryParam userQueryParam);

        /// <summary>
        /// 设置角色
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="roleIdList">用户所拥有角色</param>
        /// <returns></returns>
        bool SetRole(int userId, List<int> roleIdList);
    }
}