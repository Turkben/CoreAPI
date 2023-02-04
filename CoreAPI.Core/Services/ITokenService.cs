using CoreAPI.Core.Configuration;
using CoreAPI.Core.DTOs;
using CoreAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreAPI.Core.Services
{
    public interface ITokenService
    {
        TokenDto CreateToken(User userApp);
        ClientTokenDto CreateTokenForClient(Client client);
    }
}
