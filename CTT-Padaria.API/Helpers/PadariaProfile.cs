using AutoMapper;
using CTT_Padaria.API.Dto;
using Padaria.Domain.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
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

            CreateMap<ProdutoMateria, ProdutoMateriaDto>()
                .ForPath(dest => dest.Produto, opt => opt.MapFrom(src => src.Produto.Nome))                
                .ForPath(dest => dest.MateriaPrima, opt => opt.MapFrom(src => src.MateriaPrima.Nome));

            CreateMap<Produto, ProdutoDto>()                
                .ForPath(dest => dest.Producao, opt => opt.MapFrom(src => src.Producao))
                .ForPath(dest => dest.UnidadeDeMedida, opt => opt.MapFrom(src => src.UnidadeDeMedida))                
                .ForMember(dest => dest.Materias, opt => opt.MapFrom(src => src.ProdutosMaterias
                .Select(m => new MateriasProdutoDto
                {
                    Materia = m.MateriaPrima.Nome,
                    Quantidade = m.Quantidade,
                    Porcao = m.Porcao
                })));

            CreateMap<Comanda, ComandaDto>()
                .ForMember(dest => dest.Comanda, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Data, opt => opt.MapFrom(src => src.DataEntrada.ToString(CultureInfo.CreateSpecificCulture("es-ES"))))
                .ForMember(dest => dest.ValorTotal, opt => opt.MapFrom(src => src.ProdutosComanda.Sum(c => c.PrecoTotal)))
                .ForMember(dest => dest.Produtos, opt => opt.MapFrom(src => src.ProdutosComanda
                .Select(m => new ProdutosComandaDto
                {
                    Produto = m.Produto.Nome,
                    Quantidade = m.Quantidade,
                    ValorUnitario = (decimal)m.Produto.Valor,
                    Valor = m.PrecoTotal
                }))); 
        }
    }
}
