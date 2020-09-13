using AutoMapper;
using CB.Catalogo.API.Models;
using CB.Catalogo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CB.Catalogo.API.Configuration
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Produto, ListarProdutoViewModel>();
        }
    }
}
