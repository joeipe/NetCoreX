using AutoMapper;
using NetCoreX.Domain;
using NetCoreX.ViewModel;
using SharedKernel.Extensions;

namespace NetCoreX.API.AutoMapper
{
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            CreateMap<Contact, ContactVM>()
                .ForMember(dest => dest.DoB, opt => opt.MapFrom(src => src.DoB.ParseDate()));

            CreateMap<ContactVM, Contact>()
                .ForMember(dest => dest.DoB, opt => opt.MapFrom(src => src.DoB.ParseDate()));
        }
    }
}