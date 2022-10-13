using AutoMapper;
using Contracts;
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
        public ServiceManager(IRepositoryManager repository, IMapper mapper)
        {
            _bookService = new Lazy<IBookService>(new BookService(repository, mapper));
        }

        public IBookService BookService => _bookService.Value;
    }
}
