using Yuyi.Oa.Model;

namespace Yuyi.Oa.IDAL
{
    public interface IUserInfoDal : IBaseDal<UserInfo>
    {
        /*
         封装到基接口
         */
        //IQueryable<UserInfo> GetEntity(Expression<Func<UserInfo, bool>> whereLamda);
        //IQueryable<UserInfo> GetEntityByPage(Expression<Func<UserInfo, bool>> wherelamda);

        //bool Add(UserInfo userinfo);

        //bool Update(UserInfo userInfo);

        //bool Delete(Expression<Func<UserInfo, bool>> whereLamda);
    }
}