using Padaria.Domain.Interface;
using System;

namespace Padaria.Domain.Model
{
    public class Usuario : IBaseEntity
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public PerfilAcesso Perfil { get; set; }
        public enum PerfilAcesso
        {
            Administrador,
            Padeiro,
            Estoquista,
            Vendedor
        }
    }
}
