namespace Controle_de_Epis.ViewModel.Colaborador
{
    public class EmprestimosColaboradorViewModel
    {
        public int Id { get; set; }
        public string NomeColaborador { get; set; } = string.Empty;
        public string CargoColaborador { get; set; } = string.Empty;
        public string SetorColaborador { get; set; } = string.Empty;
        //public ICollection<EmprestimoEpiViewModel> Emprestimos { get; set; } = new List<EmprestimoEpiViewModel>();
    }
}
