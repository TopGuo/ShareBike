using System;
using System.Threading;
using Yuyi.Oa.BLL;
using Yuyi.Oa.IBLL;
using Yuyi.Oa.Model;

namespace Yuyi.Oa.UI.Models
{
    public class Yiquan
    {
        static IYiquanInfoService yiquanInfoService = new YiquanInfoService();
        static YiquanInfo yiquanInfo = new YiquanInfo();
        static short noDel = (short)Model.Enum.DelFlagEnum.NoDel;
        public static void SetYiquanToUser()
        {
            while (true)
            {
                //1、获取会员信息
                ReturnYiquanInfo sh = Accounts.GetReturnYiquanInfo();
                if (sh == null){
                    Thread.Sleep(3000);//休眠三秒钟，等待获取线程获取会员
                    continue;
                }
                //2、执行返券

                yiquanInfo.UserInfoID = sh.UserId;
                yiquanInfo.Counts = sh.ReturnCounts;
                yiquanInfo.DelFlag = noDel;
                yiquanInfo.ModifyOn = DateTime.Now;
                yiquanInfo.SubTime = DateTime.Now;
                yiquanInfo.Remark = "返券";
                yiquanInfoService.Add(yiquanInfo);
            }
        }
    }
}