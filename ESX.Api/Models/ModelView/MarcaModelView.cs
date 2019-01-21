using System;

namespace ESX.Api.Models.ModelView
{
    public class MarcaModelView : IModelView
    {
        public int MarcaId { get; set; }
        public string Nome { get; set; }
    }
}