using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repository
{
    public static class  SpecificationEvaluation 
    {
        public static IQueryable<TEntity> CreateQuery<TEntity, T>(IQueryable<TEntity> inputQuery, Ispecification<TEntity, T> specification) where TEntity : class
        {
            var query = inputQuery;
            if (specification.Criteria != null)      
                query = query.Where(specification.Criteria);
            //foreach (var include in specification.IncludeExpressions)
            //{
            //    query = query.Include(include);
            //}


                query = specification.IncludeExpressions.Aggregate(query, (current, include) => current.Include(include));
            if(specification.OrderByAsc != null)
                query = query.OrderBy(specification.OrderByAsc);
            else if (specification.OrderByDescending != null)
                query = query.OrderByDescending(specification.OrderByDescending);
            if (specification.IsPaginated)
                query = query.Skip(specification.Skip).Take(specification.Take);
            return query;
        }
    }
}
