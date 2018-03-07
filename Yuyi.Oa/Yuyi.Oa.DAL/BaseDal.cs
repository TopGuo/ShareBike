using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Yuyi.Oa.DAL
{
    public class BaseDal<T> where T : class ,new()
    {
        //ModelDataContainer Db = new ModelDataContainer();
        public DbContext Db {
            get { return DbContentFactory.GetCurrentContext(); }
        }

        #region 查询
        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="whereLamda"></param>
        /// <returns></returns>
        public IQueryable<T> GetEntity(Expression<Func<T, bool>> whereLamda)
        {
            return Db.Set<T>().Where(whereLamda);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="total"></param>
        /// <param name="wherelamda"></param>
        /// <param name="whereorder"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public IQueryable<T> GetEntityByPage<TKey>(int pageIndex, int pageSize, out int total, Expression<Func<T, bool>> wherelamda,
            Expression<Func<T, TKey>> whereorder, bool isAsc)
        {
            total = Db.Set<T>().Where(wherelamda).Count();
            // 分页 一定注意： Skip 之前一定要 OrderBy 
            if (isAsc)
            {
                var temp =
                    Db.Set<T>()
                        .Where(wherelamda)
                        .OrderBy<T, TKey>(whereorder)
                        .Skip(pageSize * (pageIndex - 1))
                        .Take(pageSize);

                return temp;
            }
            else
            {
                var temp =
                    Db.Set<T>()
                        .Where(wherelamda)
                        .OrderByDescending<T, TKey>(whereorder)
                        .Skip(pageSize * (pageIndex - 1))
                        .Take(pageSize);

                return temp;
            }
        } 
        #endregion

        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entityInfo"></param>
        /// <returns></returns>
        public bool Add(T entityInfo)
        {
            Db.Set<T>().Add(entityInfo);
            return true; //Db.SaveChanges() > 0;
        } 
        #endregion

        #region 修改
        public bool Update(T entityInfo)
        {
            Db.Entry(entityInfo).State = EntityState.Modified;
            return true;//Db.SaveChanges() > 0;

        } 
        #endregion

        #region 删除
        /// <summary>
        /// 按条件删除
        /// </summary>
        /// <param name="entitityInfo"></param>
        /// <returns></returns>
        public bool Delete(T entitityInfo)
        {
            Db.Entry(entitityInfo).State = EntityState.Deleted;
            return true; //Db.SaveChanges() > 0;
        }

        /// <summary>
        /// 根据Id删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            var entity = Db.Set<T>().Find(id);
            if (entity == null)
            {
                return false;
            }
            Db.Set<T>().Remove(entity);
            return true;
        } 
        #endregion
    }
}