using AutoMapper;
using Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ServiceManager : IServiceManager
    {
        private Lazy<IBookService> _bookService;
        private Lazy<IAuthenticationService> _authenticationService;
        public ServiceManager(IRepositoryManager repository, IMapper mapper, UserManager<IdentityUser<Guid>> userManager, IConfiguration configuration)
        {
            _bookService = new Lazy<IBookService>(new BookService(repository, mapper));
            _authenticationService = new Lazy<IAuthenticationService>(new AuthenticationService(userManager, configuration));
        }

        public IBookService BookService => _bookService.Value;
        public IAuthenticationService AuthencationService => _authenticationService.Value;
    }
}
