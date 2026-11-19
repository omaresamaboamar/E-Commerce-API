using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface Ispecification<TEntity,T> where TEntity : class
    {
        Expression<Func<TEntity, bool>> Criteria{ get; }
        List<Expression<Func<TEntity,object>>> IncludeExpressions { get; }
        Expression<Func<TEntity, object>>? OrderByAsc { get; }
        Expression<Func<TEntity, object>>? OrderByDescending { get; }
        int Take { get; }
        int Skip { get; }
        bool IsPaginated { get; }
    }
}
