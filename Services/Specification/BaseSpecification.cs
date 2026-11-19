using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;

namespace Services.Specification
{
    public class BaseSpecification<TEntity, T> : Ispecification<TEntity, T> where TEntity : BaseEntity<T>
    {
        public BaseSpecification(Expression<Func<TEntity , bool>>? criteria)
        {
            Criteria = criteria;
        }
        public Expression<Func<TEntity, bool>> Criteria
        {
            get;
            private set;
           
        }

        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = [];

        public Expression<Func<TEntity, object>>? OrderByAsc { get; private set; }

        public Expression<Func<TEntity, object>>? OrderByDescending { get; private set; }

        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPaginated { get; private set; }

        public void ApplyPaging(int PageSize, int PageIndex)
        {
            IsPaginated = true;
            Skip = PageSize;
            Take = (PageIndex - 1)* PageSize;
        }
        protected void AddIncludes(Expression<Func<TEntity, object>> include)
            => IncludeExpressions.Add(include);

        protected void AddOrderByAsc(Expression< Func<TEntity, object>> orderByAsc)
            => OrderByAsc = orderByAsc;

        protected void AddOrderByDesc(Expression<Func<TEntity, object>> orderByDesc)
            => OrderByDescending = orderByDesc;
    }
}
