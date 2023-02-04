using AutoMapper;
using CoreAPI.Core.Repositories;
using CoreAPI.Core.Services;
using CoreAPI.Core.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
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
        private readonly IGenericRepository<TEntity> _repository;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        public GenericService(IGenericRepository<TEntity> repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Response<TDto>> AddAsync(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            var newDto = _mapper.Map<TDto>(entity);
            return Response<TDto>.Success(newDto, 200);
        }

        public async Task<Response<IEnumerable<TDto>>> GetAllAsync()
        {
            var entites = await _repository.GetAllAsync();
            var newDtos = _mapper.Map<IEnumerable<TDto>>(entites);
            return Response<IEnumerable<TDto>>.Success(newDtos, StatusCodes.Status200OK);
        }

        public async Task<Response<TDto>> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
            {
                return Response<TDto>.Fail("Not Found", StatusCodes.Status404NotFound, true);
            }
            var newDto = _mapper.Map<TDto>(entity);
            return Response<TDto>.Success(newDto, StatusCodes.Status200OK);
        }

        public async Task<Response<NoContentDto>> RemoveAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                return Response<NoContentDto>.Fail("Not Found", StatusCodes.Status404NotFound, true);
            }
            _repository.Remove(entity);
            await _unitOfWork.CommitAsync();
            return Response<NoContentDto>.Success(StatusCodes.Status204NoContent);
        }

        public async Task<Response<NoContentDto>> Update(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            _repository.Update(entity);
            await _unitOfWork.CommitAsync();

            return Response<NoContentDto>.Success(StatusCodes.Status204NoContent);
        }

        public async Task<Response<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> predicate)
        {
            var entites = await _repository.Where(predicate).ToListAsync();
            var dtos = _mapper.Map<IEnumerable<TDto>>(entites);

            return Response<IEnumerable<TDto>>.Success(dtos, StatusCodes.Status200OK);
        }
    }
}
