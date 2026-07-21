using Controle_de_Epis.Enums;
using Controle_de_Epis.Models.Identity;

namespace Controle_de_Epis.Models
{
    public class EmprestimoEpiModel
    {

        public int Id { get; set; }
        public int ColaboradorId { get; set; }
        public ColaboradorModel Colaborador { get; set; } = null!;
        public int EpiId { get; set; }
        public EpiModel Epi { get; set; } = null!;
        public string UsuarioId { get; set; } = string.Empty;
        public ApplicationUser Usuario { get; set; } = null!;
        public DateTime DataEmprestimo { get; set; }
        public DateTime DataPrevistaTroca { get; set; }
        public DateTime DataPrevistaDevolucao { get; set; }
        public DateTime? DataDevolucao { get; set; }
        public int Quantidade { get; set; }
        public StatusEmprestimo StatusEmprestimo { get; set; }
    }
}
