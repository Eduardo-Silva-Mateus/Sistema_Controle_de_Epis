using Controle_de_Epis.Enums;
using Controle_de_Epis.Models;

namespace Controle_de_Epis.ViewModel.Epi
{
    public class ListarEpiViewModel
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string MarcaEpi { get; set; }
        public string ModeloEpi { get; set; }
        public int NumeroCa { get; set; }
        public DateTime DataValidadeEpi { get; set; }
        public StatusEpi StatusEpi { get; set; }
        public int TipoEpiId { get; set; }
        public TipoEpiModel TipoEpi { get; set; }
    }
}