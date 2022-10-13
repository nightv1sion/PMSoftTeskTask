using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;

namespace API
{
    public class MappingProfile : Profile 
    {
        public MappingProfile()
        {
            CreateMap<BookForCreationDto, Book>();
            CreateMap<BookForUpdateDto, Book>();
        }
    }
}
