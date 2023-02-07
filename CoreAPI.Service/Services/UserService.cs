using AutoMapper;
using CoreAPI.Core.DTOs;
using CoreAPI.Core.Models;
using CoreAPI.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Shared.Libary.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreAPI.Service.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        protected readonly IMapper _mapper;

        public UserService(UserManager<User> userManager, IMapper mapper, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _roleManager = roleManager;
        }

        public async Task<Response<UserDto>> CreateUserAsync(CreateUserDto createUserDto)
        {
            var user = new User
            {
                Email = createUserDto.Email,
                UserName = createUserDto.UserName
            };
            var result = await _userManager.CreateAsync(user, createUserDto.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();
                return Response<UserDto>.Fail(new ErrorDto(errors, true), StatusCodes.Status400BadRequest);
            }
            return Response<UserDto>.Success(_mapper.Map<UserDto>(user), StatusCodes.Status201Created);
        }

        public async Task<Response<UserDto>> GetUserByNameAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) return Response<UserDto>.Fail("User not found", StatusCodes.Status404NotFound, true);
            return Response<UserDto>.Success(_mapper.Map<UserDto>(user), StatusCodes.Status200OK);
        }

        //TODO: will deprecate
        public async Task<Response<NoContentDto>> CreateUserRoles(string userName)
        {

            if (!await _roleManager.RoleExistsAsync("Manager"))
            {
                await _roleManager.CreateAsync(new() { Name = "Manager" });
            }
            
            var user = await _userManager.FindByNameAsync(userName);
            await _userManager.AddToRoleAsync(user, "Manager");

            return Response<NoContentDto>.Success(StatusCodes.Status201Created);

        }
    }
}
