namespace Controle_de_Epis.Models
{
    public class TipoEpiModel
    {

        public int Id {  get; set; }
        public string NomeTipoEpi { get; set; } = string.Empty;
        public int VidaUtilTipoEpi { get; set; } //em dias
        public bool ObrigatorioCA { get; set; }
        public bool StatusTipoEpi { get; set; }
        public ICollection<EpiModel> Epis { get; set; } = new List<EpiModel>();
    }
}
