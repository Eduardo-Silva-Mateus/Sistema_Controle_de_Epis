namespace Controle_de_Epis.ViewModel.TipoEpi
{
    public class ListarTipoEpiViewModel
    {
        public int id { get; set; }
        public string NomeTipoEpi { get; set; }= string.Empty;
        public int VidaUtilTipoEpi { get; set; }
        public bool ObrigatorioCA { get; set; }
        public bool StatusTipoEpi { get; set; }
    }
}