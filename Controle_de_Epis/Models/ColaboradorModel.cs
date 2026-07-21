namespace Controle_de_Epis.Models
{
    public class ColaboradorModel
    {

        public int Id { get; set; }
        public string NomeColaborador { get; set; } = string.Empty;
        public string CpfColaborador { get; set; } = string.Empty;
        public string CargoColaborador { get; set; } = string.Empty;
        public string SetorColaborador { get; set; } = string.Empty;
        public bool AtivoColaborador { get; set; }
        public ICollection<EmprestimoEpiModel> Emprestimos { get; set; } = new List<EmprestimoEpiModel>();

    }
}
