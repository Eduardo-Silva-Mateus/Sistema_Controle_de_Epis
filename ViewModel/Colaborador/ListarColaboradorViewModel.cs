namespace Controle_de_Epis.ViewModel.Colaborador
{
    public class ListarColaboradorViewModel
    {
        public int Id { get; set; }
        public string NomeColaborador { get; set; } = string.Empty;
        public string CpfColaborador { get; set; } = string.Empty;
        public string CargoColaborador { get; set; } = string.Empty;
        public string SetorColaborador { get; set; } = string.Empty;
        public bool AtivoColaborador { get; set; }
    }
}
