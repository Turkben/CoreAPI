using CoreAPI.Core.Services;
using Shared.Libary.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoreAPI.Service.Services
{
    public class GenericService<TEntity, TDto> : IGenericService<TEntity, TDto> where TEntity : class where TDto : class
    {
        public Task<Response<TDto>> AddAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<Response<IEnumerable<TDto>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Response<TDto>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<NoContentDto>> Remove(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<Response<NoContentDto>> Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<Response<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
