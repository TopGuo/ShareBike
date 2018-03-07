using System.Reflection;
using Yuyi.Oa.IDAL;

namespace Yuyi.Oa.DALFactory
{
    /// <summary>
    /// 抽象工厂依赖反射简单工厂帮我们创建一个类
    /// 目的：应对变化点 1.换库 2.换数据库驱动
    /// </summary>
    public class AbstractDalFactory
    {
        /// <summary>
        /// 在web.cong配置文件中 保存了一个appsetion name="FactoryAssembly" 的连接字符串 →指向数据库驱动Dal的程序集 比如说 
        /// Assembly  是一个程序集 并可以无冲突的运行 自定义构造的程序集
        /// </summary>
        public static string AssemblyName = System.Configuration.ConfigurationManager.AppSettings["FactoryAssembly"];
        
        public static IUserInfoDal GetUserInfoDal()
        {
            //简单工厂→抽象工厂通过反射 相当于调用了  各种类的Dal  下一步应该将其封装到DbSession 
            //实现一个目的  SaveChange()实现其一个跨历史性的飞跃进步
            return Assembly.Load(AssemblyName).CreateInstance(AssemblyName + ".UserInfoDal") as IUserInfoDal;
        }

        public static IRoleInfoDal GetRoleInfoDal()
        {
            return Assembly.Load(AssemblyName).CreateInstance(AssemblyName + ".RoleInfoDal") as IRoleInfoDal;
        }

        public static IActionInfoDal GetActionInfoDal()
        {
            return Assembly.Load(AssemblyName).CreateInstance(AssemblyName + ".ActionInfoDal") as IActionInfoDal;
        }

        public static IR_User_ActionInfoDal GetRUserActionInfoDal()
        {
            return Assembly.Load(AssemblyName).CreateInstance(AssemblyName+".R_User_ActionInfoDal") as IR_User_ActionInfoDal;
        }

        public static IBikeInfoDal GetBikeInfoDal()
        {
            return Assembly.Load(AssemblyName).CreateInstance(AssemblyName + ".BikeInfoDal") as IBikeInfoDal;
        }

        public static IAccountInfoDal GetAccountInfoDal()
        {
            return Assembly.Load(AssemblyName).CreateInstance(AssemblyName + ".AccountInfoDal") as IAccountInfoDal;
        }

        public static IYiquanInfoDal GetYiquanInfoDal()
        {
            return Assembly.Load(AssemblyName).CreateInstance(AssemblyName + ".YiquanInfoDal") as IYiquanInfoDal;
        }


    }
}