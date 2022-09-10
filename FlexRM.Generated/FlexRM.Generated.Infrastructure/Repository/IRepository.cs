using FlexRM.Generated.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexRM.Generated.Infrastructure.Repository
{
    public interface IRepository<T> : IDisposable where T : class
    {
        public Task<T> Create(T entity);
        public Task<T> ReadOne(ISpecification<T> spec);
        public Task<List<T>> ReadMany(ISpecification<T> spec);
        public Task<T> Update(T entity);//ISpecification<T> spec, Action<T> func);
        public Task Delete(T entity);
    }
}
