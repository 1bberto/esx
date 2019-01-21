using System;

namespace ESX.Api.Models.ModelView
{
    public class TokenModelView
    {
        public string UsuarioId { get; set; }
        public string Nome { get; set; }
        public bool Autenticado { get; set; }
        public DateTime Criacao { get; set; }
        public DateTime Expira { get; set; }
        public string Token { get; set; }
    }
}