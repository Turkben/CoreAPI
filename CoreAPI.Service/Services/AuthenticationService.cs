using CoreAPI.Core.Configuration;
using CoreAPI.Core.DTOs;
using CoreAPI.Core.Models;
using CoreAPI.Core.Repositories;
using CoreAPI.Core.Services;
using CoreAPI.Core.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Shared.Libary.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreAPI.Service.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly List<Client> _clients;
        private readonly ITokenService _tokenService;
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<UserRefreshToken> _repository;

        public AuthenticationService(IOptions<List<Client>> optionClients, ITokenService tokenService, UserManager<User> userManager, IUnitOfWork unitOfWork, IGenericRepository<UserRefreshToken> repository)
        {
            _clients = optionClients.Value;
            _tokenService = tokenService;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task<Response<TokenDto>> CreateTokenAsync(LoginDto loginDto)
        {
            if (loginDto == null) throw new ArgumentNullException(nameof(loginDto));
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) return Response<TokenDto>.Fail("Email or Password is wrong!",StatusCodes.Status400BadRequest,true);
            if (!await _userManager.CheckPasswordAsync(user,loginDto.Password))
            {
                return Response<TokenDto>.Fail("Email or Password is wrong!", StatusCodes.Status404NotFound, true);
            }
            var token = _tokenService.CreateToken(user);
            var userRefreshToken = await _repository.Where(x => x.UserId == user.Id).SingleOrDefaultAsync();
            if (userRefreshToken == null)
            {
                await _repository.AddAsync(new UserRefreshToken { UserId = user.Id, Code = token.RefreshToken, Expiration = token.RefreshTokenExpiration });
            }
            else
            {
                userRefreshToken.Code= token.RefreshToken;
                userRefreshToken.Expiration= token.RefreshTokenExpiration;
            }
            await _unitOfWork.CommitAsync();

            return Response<TokenDto>.Success(token,statusCode: StatusCodes.Status200OK);          
        }

        public async Task<Response<TokenDto>> CreateTokenByRefreshTokenAsync(string refreshToken)
        {
            var existRefreshToken = await _repository.Where(x => x.Code == refreshToken).SingleOrDefaultAsync();
            if (existRefreshToken==null)
            {
                return Response<TokenDto>.Fail("Refresh token not found", StatusCodes.Status404NotFound, true);
            }
            var user = await _userManager.FindByIdAsync(existRefreshToken.UserId);
            if (user == null)
            {
                return Response<TokenDto>.Fail("User not found", StatusCodes.Status404NotFound, true);
            }
            var tokenDto = _tokenService.CreateToken(user);
            existRefreshToken.Code = tokenDto.RefreshToken;
            existRefreshToken.Expiration= tokenDto.RefreshTokenExpiration;
            await _unitOfWork.CommitAsync();

            return Response<TokenDto>.Success(tokenDto,statusCode: StatusCodes.Status200OK);

        }

        public Response<ClientTokenDto> CreateTokenForClient(ClientLoginDto clientLoginDto)
        {
            var client = _clients.SingleOrDefault(x => x.Id == clientLoginDto.ClientId && x.Secret == clientLoginDto.ClientSecret);
            if (client == null)
            {
                return Response<ClientTokenDto>.Fail("ClientId or ClientSecret not found!", StatusCodes.Status404NotFound, true);
            }
            var token =_tokenService.CreateTokenForClient(client);
            return Response<ClientTokenDto>.Success(token, StatusCodes.Status200OK);
        }

        public async Task<Response<NoContentDto>> RevokeRefreshTokenAsync(string refreshToken)
        {
            var existRefreshToken = await _repository.Where(x => x.Code == refreshToken).SingleOrDefaultAsync();
            if (existRefreshToken == null)
            {
                return Response<NoContentDto>.Fail("Refresh token not found", StatusCodes.Status404NotFound, true);
            }

            _repository.Remove(existRefreshToken);
            await _unitOfWork.CommitAsync();
            return Response<NoContentDto>.Success(StatusCodes.Status200OK);
        }
    }
}
