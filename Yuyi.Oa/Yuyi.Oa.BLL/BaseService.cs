using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Yuyi.Oa.DALFactory;
using Yuyi.Oa.IDAL;

namespace Yuyi.Oa.BLL
{
    /// <summary>
    /// 封装业务逻辑crud
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseService<T> where T : class ,new()
    {
        public readonly IDbSession DbSession = DbSessionFactory.GetCurrentDbsession();//new DbSession();

        public IBaseDal<T> CurrentDal { get; set; }

        public abstract void SetCurrentDal();

        protected BaseService()
        {
            SetCurrentDal();
        }

        //---增删查改---//
        public IQueryable<T> GetEntity(Expression<Func<T, bool>> whereLamda)
        {
            return CurrentDal.GetEntity(whereLamda);
        }

        public IQueryable<T> GetEntityByPage<TKey>(int pageIndex, int pageSize, out int total,
            Expression<Func<T, bool>> wherelamda, Expression<Func<T, TKey>> whereorder, bool isAsc)
        {
            return CurrentDal.GetEntityByPage(pageIndex, pageSize, out total, wherelamda, whereorder, isAsc);
        }

        public bool Add(T entityInfo)
        {
            CurrentDal.Add(entityInfo);
            return DbSession.SaveChanges() > 0;
        }

        public bool Update(T entityInfo)
        {
            CurrentDal.Update(entityInfo);
            return DbSession.SaveChanges() > 0;
        }

        #region 删除
        public bool Delete(T entityInfo)
        {
            CurrentDal.Delete(entityInfo);
            return DbSession.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            CurrentDal.Delete(id);
            return DbSession.SaveChanges() > 0;
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteList(List<int> ids)
        {
            foreach (var id in ids)
            {
                CurrentDal.Delete(id);
            }
            return DbSession.SaveChanges();
        } 
        #endregion
    }
}