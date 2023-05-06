using AutoMapper;
using Stack.DTOs.Models;
using Stack.Entities.Models;

namespace Stack.API.AutoMapperConfig
{
    public class AutoMapperProfile : Profile
    {
        //Auto Mapper Configuration File . 
        public AutoMapperProfile()
        {
            //Examples for mapping between entities and their respective DTOs

            //Mirror mapping between an entity and it's DTO . 
            //CreateMap<Section, SectionDTO>().ReverseMap();

            //Mapping an entity and it's DTO while ignorig cyclic dependancy errors . 

            CreateMap<Customer, CustomerDTO>()
            .ForMember(dest => dest.ApplicationUser, opt => opt.Ignore())
            .ForMember(dest => dest.ServiceRequests, opt => opt.Ignore())
            .ReverseMap(); 

            CreateMap<Banks, BanksDTO>()
            .ForMember(dest => dest.ServiceRequests, opt => opt.Ignore())
            .ReverseMap();

            CreateMap<ServiceTypes, ServiceTypesDTO>()
            .ForMember(dest => dest.Services, opt => opt.Ignore())
            .ReverseMap();

            CreateMap<ApplicationUser, ApplicationUserDTO>()
            .ReverseMap();


            CreateMap<Services, ServicesDTO>()
            .ReverseMap();


            CreateMap<ServiceRequests, ServiceRequestsDTO>()
            .ReverseMap();

            CreateMap<Offer, OfferDTO>()
            .ForMember(dest => dest.ServiceTypes, opt => opt.Ignore())
            .ReverseMap();

            CreateMap<OfferBenefit, OfferBenefitDTO>()
            .ForMember(dest => dest.Offer, opt => opt.Ignore())
            .ReverseMap();


            CreateMap<ContactRequest, ContactRequestDTO>()
            .ReverseMap();

            CreateMap<NewsletterSubscription, NewsletterSubscriptionDTO>()
            .ReverseMap();

            CreateMap<ServiceRequestComment, ServiceRequestCommentDTO>()
            .ForMember(dest => dest.ServiceRequests, opt => opt.Ignore())
            .ReverseMap();

            CreateMap<IslamicServiceRequest, IslamicServiceRequestDTO>()
              .ReverseMap();


            CreateMap<CorporateServiceRequest, CorporateServiceRequestDTO>()
              .ReverseMap();

            CreateMap<CorporateRequestComment, CorporateRequestCommentDTO>()
           .ForMember(dest => dest.CorporateServiceRequest, opt => opt.Ignore())
           .ReverseMap();

            CreateMap<IslamicRequestComment, IslamicRequestCommmentDTO>()
           .ForMember(dest => dest.IslamicServiceRequest, opt => opt.Ignore())
           .ReverseMap();
            
            CreateMap<NotifiedOfferRequest, NotifiedOfferRequestDTO>()
           .ForMember(dest => dest.NotifiedOffer, opt => opt.Ignore())
           .ReverseMap();

            CreateMap<NotifiedOffer, NotifiedOfferDTO>()
           .ReverseMap();


        }

    }
}
