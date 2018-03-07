using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yuyi.Oa.DALFactory;
using Yuyi.Oa.IDAL;
using Yuyi.Oa.Model;

namespace UnitTest.Bll
{
    [TestClass]
    public class UserServiceUnitTest
    {
        IDbSession dbSession = new DbSession();
        [TestMethod]
        public void UserServiceAddTest()
        {
            short nodel = (short)1;
            for (int i = 0; i < 3; i++)
            {
                dbSession.GetUserInfoDal.Add(new UserInfo()
                {
                    
                    Name = "郭东生" + i,
                    DelFlag = nodel,
                    SubTime = DateTime.Now,
                    ModifyOn = DateTime.Now,
                    Pwd = "123456",
                    Remark = "...",
                    ShowName = "guo..."
                    
                });
            }
            var result = dbSession.SaveChanges() > 0;
            if (result)
            {
                Console.WriteLine("ok");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("error");
                Console.ReadKey();
            }
        }

        [TestMethod]
        public void UserServiceGetEntityTest()
        {
            Assert.AreEqual(true,dbSession.GetUserInfoDal.GetEntity(u=>u.Name.Contains("1")).Count()>1);
        }
        
    }
}
