using System;
using System.Linq;
using System.Linq.Expressions;

namespace Yuyi.Oa.IDAL
{
    public interface IBaseDal<T> where T : class, new()
    {
        IQueryable<T> GetEntity(Expression<Func<T, bool>> whereLamda);

        IQueryable<T> GetEntityByPage<TKey>(int pageIndex, int pageSize, out int total, Expression<Func<T, bool>> wherelamda, Expression<Func<T, TKey>> whereorder, bool isAsc);

        bool Add(T entityInfo);

        bool Update(T entityInfo);

        bool Delete(T entityInfo);

        bool Delete(int id);
    }
}