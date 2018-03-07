using System.Collections.Generic;
using System.Linq;
using Yuyi.Oa.IBLL;
using Yuyi.Oa.Model;
using Yuyi.Oa.Model.Param;

namespace Yuyi.Oa.BLL
{
    public class UserInfoService : BaseService<UserInfo>, IUserInfoService
    {
        #region 代码已经封装到BaseService
        ////IUserInfoDal _dal = new UserInfoDal();
        ////public IUserInfoDal UserInfoDal = AbstractDalFactory.GetUserInfoDal();
        //public readonly IDbSession DbSession = DbSessionFactory.GetCurrentDbsession();//new DbSession();

        //public bool Add(UserInfo userInfo)
        //{
        //    //return UserInfoDal.Add(userInfo);
        //    DbSession.GetUserInfoDal.Add(userInfo);
        //    return DbSession.SaveChanges() > 0;
        //}

        //public IQueryable<UserInfo> GetAllUserInfo(Expression<Func<UserInfo, bool>> wherelamda)
        //{
        //    //return UserInfoDal.GetEntity(wherelamda);
        //    return DbSession.GetUserInfoDal.GetEntity(wherelamda);
        //}
        #endregion

        #region 重写父类Dal
        /// <summary>
        /// 当ui层调用业务逻辑层时，先为其父类CurrentDal赋值
        /// </summary>
        public override void SetCurrentDal()
        {
            CurrentDal = DbSession.GetUserInfoDal;
        } 
        #endregion

        #region 多条件过滤的封装
        /// <summary>
        /// 实现多条件查询
        /// </summary>
        /// <param name="userQueryParam">查询参数</param>
        /// <returns></returns>
        public IQueryable<UserInfo> LoadPageData(UserQueryParam userQueryParam)
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


            //查询到数据总条数
            var pageSize = userQueryParam.PageSize;
            var pageIndex = userQueryParam.PageIndex - 1;

            //分页  记得先排序
            var userInfos = temp.OrderBy(u => u.ID)
                .Skip(pageSize * pageIndex)
                .Take(pageSize);
            userQueryParam.Total = temp.Count();

            return userInfos;
        }
        #endregion

        #region 为用户设置角色
        /// <summary>
        /// 实现设置角色
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="roleIdList">用户所拥有角色</param>
        /// <returns></returns>
        public bool SetRole(int userId, List<int> roleIdList)
        {
            //更具id查用户信息
            var user = DbSession.GetUserInfoDal.GetEntity(u => u.ID == userId).FirstOrDefault();
            if (user == null)
            {
                return false;
            }
            user.RoleInfo.Clear();//清除掉原有的角色，统一加上
            var allRole = DbSession.GetRoleInfoDal.GetEntity(u => roleIdList.Contains(u.ID));

            foreach (var roleInfo in allRole)
            {
                user.RoleInfo.Add(roleInfo);//统一加上
            }
            return DbSession.SaveChanges() > 0;
        }
        #endregion
    }
}