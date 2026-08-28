using Controle_de_Epis.ViewModel;
using Controle_de_Epis.Results;

namespace Controle_de_Epis.Service.Interfaces
{
    public interface ITipoEpiService
    {
        Task<ResultError<ListarTipoEpiViewModel>> ListarTipoEpiAsync();
        Task<ResltError<CriarTipoEpiViewModel>> CriarTipoEpiAsync(CriarTipoEpiViewModel tipoepi);
        Task<ResultError<EditarTipoEpiViewModel>> EditarTipoEpiAsync(int id);
        Task<ResultError<EditarTipoEpiViewMode>> UpdateTipoEpiAsync(EditarTipoEpiViewModel tipoepi)
        Task<ResultErro<bool>> AlterarStatusAsync(int id);
    }
}