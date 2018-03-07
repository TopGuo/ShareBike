using System.Runtime.Remoting.Messaging;
using Yuyi.Oa.IDAL;

namespace Yuyi.Oa.DALFactory
{
    public class DbSessionFactory
    {
        /// <summary>
        /// 同一请求共用DbSession 数据会话
        /// 为业务逻辑层提供 优化 一次请求共有一个DbSession
        /// </summary>
        /// <returns>IDbSession</returns>
        public static IDbSession GetCurrentDbsession()
        {
            var dbsession = CallContext.GetData("DbSession") as IDbSession;
            if (dbsession == null)
            {
                dbsession = new DbSession();
                CallContext.SetData("DbSession", dbsession);
            }
            return dbsession;
        }
    }
}