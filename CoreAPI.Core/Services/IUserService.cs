using CoreAPI.Core.DTOs;
using Shared.Libary.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreAPI.Core.Services
{
    public interface IUserService
    {
        Task<Response<UserDto>> CreateUserAsync(CreateUserDto createUserDto);
        Task<Response<UserDto>> GetUserByNameAsync(string userName);

        Task<Response<NoContentDto>> CreateUserRoles(string userName);
    }
}
