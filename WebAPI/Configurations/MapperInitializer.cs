using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Configurations
{
    public class MapperInitializer : Profile
    {
        /// <summary>
        /// Bridge between data classes and DTOs
        /// </summary>
        public MapperInitializer()
        {
            CreateMap<APIUser, UserDTO>().ReverseMap();
            CreateMap<APIUser, LoginUserDTO>().ReverseMap();

            CreateMap<Product, ProductDTO>().ReverseMap();

            CreateMap<Customer, CustomerDTO>().ReverseMap();

            CreateMap<Invoice, InvoiceDTO>().ReverseMap();
            CreateMap<Invoice, CreateInvoiceDTO>().ReverseMap();

            CreateMap<InvoiceProduct, InvoiceProductDTO>().ReverseMap();

        }
    }
}
