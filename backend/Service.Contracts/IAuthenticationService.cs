using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IAuthenticationService
    {
        Task<string> CreateTokenAsync(UserForAuthenticationDto userForAuth);
        Task RegisterUserAsync(UserForRegistrationDto userForRegistration);
    }
}
