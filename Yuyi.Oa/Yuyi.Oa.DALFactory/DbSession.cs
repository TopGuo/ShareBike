using Yuyi.Oa.DAL;
using Yuyi.Oa.IDAL;

namespace Yuyi.Oa.DALFactory
{
    /// <summary>
    /// 架于Dal层之上 Bll访问Dal层的统一入口 DbSession（数据会话层）
    /// 封装所有dal层的实例
    /// </summary>
    public class DbSession : IDbSession
    {

        public IUserInfoDal GetUserInfoDal
        {
            get { return AbstractDalFactory.GetUserInfoDal(); }
        }

        public IRoleInfoDal GetRoleInfoDal
        {
            get { return AbstractDalFactory.GetRoleInfoDal(); }
        }

        public IActionInfoDal GetActionInfoDal
        {
            get { return AbstractDalFactory.GetActionInfoDal(); }
        }

        public IR_User_ActionInfoDal GetRUserActionInfoDal {
            get { return AbstractDalFactory.GetRUserActionInfoDal(); }
        }

        public IBikeInfoDal GeBikeInfoDal {
            get { return AbstractDalFactory.GetBikeInfoDal(); }
        }

        public IAccountInfoDal GetAccountInfoDal {
            get { return AbstractDalFactory.GetAccountInfoDal(); }
        }
        public IYiquanInfoDal GetYiquanInfoDal {
            get { return AbstractDalFactory.GetYiquanInfoDal(); }
        }


        //----封装SaveChanages----减少用户与数据库之间的会话----单元工作----批量提交----提高性能
        //将提交数据操作从Dal层提升到Bll层
        /// <summary>
        /// DbContentFactory.GetCurrentContext()获取上下文
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
            return DbContentFactory.GetCurrentContext().SaveChanges();
        }

    }
}