using AutoMapper;
using DocumentMe.DataAccessLayer.DTO.Document;
using DocumentMe.DataAccessLayer.DTO.Public;
using DocumentMe.DataAccessLayer.Entity.Public;

namespace DocumentMe.Service.Helper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Document, DocumentDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}
