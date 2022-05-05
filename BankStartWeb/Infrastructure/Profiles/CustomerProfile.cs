using AutoMapper;
using BankStartWeb.Data;
using BankStartWeb.Pages;

namespace BankStartWeb.Infrastructure.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerModel>().ReverseMap();
        }
    }
}
