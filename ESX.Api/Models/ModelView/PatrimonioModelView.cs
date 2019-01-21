namespace ESX.Api.Models.ModelView
{
    public class PatrimonioModelView : IModelView
    {
        public int PatrimonioId { get; set; }
        public string Descricao { get; set; }
        public string NumeroTombo { get; set; }
        public MarcaModelView Marca { get; set; }
    }
}