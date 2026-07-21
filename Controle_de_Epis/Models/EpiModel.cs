using Controle_de_Epis.Enums;

namespace Controle_de_Epis.Models
{
    public class EpiModel
    {

        public int Id { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string MarcaEpi { get; set; } = string.Empty;
        public string ModeloEpi { get; set; } = string.Empty;
        public int NumeroCa { get; set; }
        public DateTime DataValidadeEpi { get; set; }
        public StatusEpi StatusEpi { get; set; }
        public int TipoEpiId { get; set; }
        public TipoEpiModel TipoEpi { get; set; } = null!;
        public ICollection<EstoqueEpiModel> MovimentacoesEstoque { get; set; } = new List<EstoqueEpiModel>();
        public ICollection<EmprestimoEpiModel> Emprestimos { get; set; } = new List<EmprestimoEpiModel>();
    }
}
