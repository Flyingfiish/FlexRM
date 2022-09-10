using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FlexRM.Core.Domain.Specifications
{
    public interface ISpecification<T>
    {
        Func<IQueryable<T>, IIncludableQueryable<T, object>> Includes { get; set; }
        bool IsSatisfiedBy(T entity);
        Expression<Func<T, bool>> ToExpression();
    }
}