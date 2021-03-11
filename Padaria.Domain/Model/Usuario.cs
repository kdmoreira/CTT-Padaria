using Padaria.Domain.Interface;
using System;

namespace Padaria.Domain.Model
{
    public class Usuario : IBaseEntity
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string Perfil { get; set; }
        public string Senha { get; set; }
        public string Token { get; set; }
    }
}
