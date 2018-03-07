namespace Yuyi.Oa.IDAL
{
    /// <summary>
    /// 拥有所有dal实体类
    /// </summary>
    public interface IDbSession
    {

        IUserInfoDal GetUserInfoDal { get; }

        IRoleInfoDal GetRoleInfoDal { get; }

        IActionInfoDal GetActionInfoDal { get; }

        IR_User_ActionInfoDal GetRUserActionInfoDal { get; }

        IBikeInfoDal GeBikeInfoDal { get; }

        IAccountInfoDal GetAccountInfoDal { get; }

        IYiquanInfoDal GetYiquanInfoDal { get; }

        //-----接口里的方法
        int SaveChanges();

    }
}