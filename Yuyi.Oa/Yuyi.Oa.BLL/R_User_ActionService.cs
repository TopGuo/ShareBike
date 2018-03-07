using Yuyi.Oa.IBLL;
using Yuyi.Oa.Model;

namespace Yuyi.Oa.BLL
{
    public class R_User_ActionInfoService:BaseService<R_User_ActionInfo>,IR_User_ActionInfoService
    {
        public override void SetCurrentDal()
        {
            CurrentDal = DbSession.GetRUserActionInfoDal;
        }
    }
}