using Controle_de_Epis.Results;
using Controle_de_Epis.ViewModel.Colaborador;

namespace Controle_de_Epis.Service.Interfaces
{
    public interface IColaboradorService
    {
        Task<List<ListarColaboradorViewModel>> GetAllColaboradoresAsync();
        Task<ResultError<CriarColaboradorViewModel>> CriarColaboradorAsync(CriarColaboradorViewModel colaborador);
        Task<ResultError<EditarColaboradorViewModel>> GetColaboradorByIdAsync(int id); 
        Task<ResultError<EditarColaboradorViewModel>> UpdateColaboradorAsync(EditarColaboradorViewModel colaborador);
        Task<ResultError<bool>> AlterarStatusAsync(int id);
        //Task<EmprestimosColaboradorViewModel> EmprestimosColaborador(EmprestimosColaboradorViewModel emprestimosColaborador);
    }
}
