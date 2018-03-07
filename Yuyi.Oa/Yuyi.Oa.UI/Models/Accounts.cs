using System;
using System.Collections.Generic;
using System.Threading;
using Yuyi.Oa.BLL;
using Yuyi.Oa.IBLL;

namespace Yuyi.Oa.UI.Models
{
    public class Accounts
    {
        private static object Objone = new object();
        public static Random Random = new Random();
        public static List<ReturnYiquanInfo> ReturnYiquanList;//= new List<ReturnYiquanInfo>();
        static IAccountInfoService accountInfoService = new AccountInfoService();

        public Accounts()
        {
            ReturnYiquanList = new List<ReturnYiquanInfo>();
        }

        /// <summary>
        /// 获取所有账户表充值的用户前五个月数据，做好统计，返还
        /// </summary>
        public static void GetAllAccount()
        {
            
            while (true)
            {
                if (ReturnYiquanList == null)
                {
                    var accountList = accountInfoService.GetEntity(u => true);
                    foreach (var accountInfo in accountList)
                    {
                        ReturnYiquanInfo returnYiquan = new ReturnYiquanInfo();
                        returnYiquan.UserId = accountInfo.UserInfoID;
                        returnYiquan.ReturnCounts = int.Parse(accountInfo.Money.ToString());
                        ReturnYiquanList.Add(returnYiquan);


                    }

                }
                Thread.Sleep(10 * 60 * 1000);//休息十分钟
            }
        }

        /// <summary>
        /// 获取相应数据信息
        /// </summary>
        /// <returns></returns>
        public static ReturnYiquanInfo GetReturnYiquanInfo()
        {
            lock (Objone)
            {
                if (ReturnYiquanList != null)
                {
                    ReturnYiquanInfo sh = ReturnYiquanList[Random.Next(ReturnYiquanList.Count)];
                    ReturnYiquanList.Remove(sh);
                    return sh;
                }
                return null;
            }
        }
    }
}