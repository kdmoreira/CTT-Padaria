using AutoMapper;
using CTT_Padaria.API.Dto;
using Padaria.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CTT_Padaria.API.Helpers
{
    public class PadariaProfile : Profile
    {
        public PadariaProfile()
        {
            CreateMap<Usuario, UsuarioDto>()
                .ForMember(dest => dest.DataNascimento, opt => opt.MapFrom(src => src.DataNascimento.Value.ToString("dd/MM/yyyy")));

            CreateMap<MateriaPrima, MateriaPrimaDto>()
                .ForMember(dest => dest.UnidadeDeMedida, opt => opt.MapFrom(src => src.UnidadeDeMedida));
        }
    }
}
