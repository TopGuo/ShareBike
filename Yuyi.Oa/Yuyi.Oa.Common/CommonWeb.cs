using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace Yuyi.Oa.Common
{
    public class CommonWeb
    {


        #region MD5
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="str">要加密的密码</param>
        /// <returns>加密后的</returns>
        public static string MD5String(string str)
        {
            MD5 md5 = MD5.Create();
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(str);
            byte[] md5buffer = md5.ComputeHash(buffer);
            StringBuilder sb = new StringBuilder();
            foreach (byte a in md5buffer)
            {
                sb.Append(a.ToString("x2"));
            }
            md5.Clear();//释放资源
            return sb.ToString();

        }
        #endregion

        #region 计算评论发表时间
        /// <summary>
        /// 返回时间差 
        /// </summary>
        /// <param name="tsSpan"></param>
        /// <returns></returns>
        public static string GetTimeSpan(TimeSpan tsSpan)
        {
            if (tsSpan.TotalDays >= 365)
            {
                return Math.Floor(tsSpan.TotalDays / 365) + "年前";
            }
            else if (tsSpan.TotalDays >= 30)
            {
                return Math.Floor(tsSpan.TotalDays / 30) + "月前";
            }
            else if (tsSpan.TotalHours >= 24)
            {
                return Math.Floor(tsSpan.TotalDays) + "天前";
            }
            else if (tsSpan.TotalHours >= 1)
            {
                return Math.Floor(tsSpan.TotalHours) + "小时前";
            }
            else if (tsSpan.TotalMinutes >= 1)
            {
                return Math.Floor(tsSpan.TotalMinutes) + "分钟前";
            }
            else
            {
                return "刚刚";
            }
        }
        #endregion

        #region 生成单车编号

        public static string AutoNumber()
        {
            var suiJi=Guid.NewGuid().ToString().Substring(25,6);
            var nowData=DateTime.Now.ToString("yyyyMMddhms");
            StringBuilder sb=new StringBuilder();
            sb.Append("ZY");
            sb.Append(nowData);
            sb.Append(suiJi);
            return sb.ToString();
        }

        #endregion


    }
}
