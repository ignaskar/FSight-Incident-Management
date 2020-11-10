using AutoMapper;
using FSight.API.Dtos;
using FSight.Core.Entities;

namespace FSight.API.Profiles
{
    public class CustomersProfile : Profile
    {
        public CustomersProfile()
        {
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto, Customer>();
        }
    }
}