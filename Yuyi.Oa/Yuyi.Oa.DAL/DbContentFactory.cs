using System.Data.Entity;
using System.Runtime.Remoting.Messaging;
using Yuyi.Oa.Model;

namespace Yuyi.Oa.DAL
{
    /// <summary>
    /// 线程共享
    /// </summary>
    public class DbContentFactory
    {
        /// <summary>
        /// 同一请求共用一个上下文
        /// </summary>
        /// <returns>DbContext</returns>
        public static DbContext GetCurrentContext()
        {
            var db = CallContext.GetData("DbContent") as DbContext;
            if (db == null)
            {
                db = new ModelDataContainer();
                CallContext.SetData("DbContent", db);
            }
            return db;
        }
    }
}