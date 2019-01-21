namespace ESX.Api.Models.ModelView
{
    public class BaseViewModel<T>
    {
        public bool Sucesso { get; set; } = true;
        public string Mensagem { get; set; } = null;
        public long TempoDeProcessamento { get; set; } = 0;
        public T ObjetoDeRetorno { get; set; } = default(T);

        public void GerarRetornoErro(T objeto, string mensagem)
        {
            Mensagem = mensagem;
            Sucesso = false;
        }
    }
}