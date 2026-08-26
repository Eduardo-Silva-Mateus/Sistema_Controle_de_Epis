namespace Controle_de_Epis.Results
{
    public class ResultError<T>
    {
        public bool Sucesso { get; set; }

        public string? Erro { get; set; }

        public T? Dados { get; set; }

        public static ResultError<T> Ok(T dados)
        {
            return new ResultError<T>
            {
                Sucesso = true,
                Dados = dados
            };
        }

        public static ResultError<T> Falha(string erro)
        {
            return new ResultError<T>
            {
                Sucesso = false,
                Erro = erro
            };
        }
    }
}