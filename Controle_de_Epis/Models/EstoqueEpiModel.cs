using Controle_de_Epis.Enums;
using Controle_de_Epis.Models.Identity;

namespace Controle_de_Epis.Models
{
    public class EstoqueEpiModel
    {
        public int Id {  get; set; }
        public TipoMovimentacao TipoMovimentacao { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataMovimentacao { get; set; }
        public string? Observacao { get; set; }
        public EpiModel Epi { get; set; } = null!;
        public int EpiId { get; set; }
        public ApplicationUser Usuario { get; set; } = null!;
        public string UsuarioId { get; set; } = string.Empty;
    }
}