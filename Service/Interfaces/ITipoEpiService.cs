using Controle_de_Epis.ViewModel;
using Controle_de_Epis.Results;
using Controle_de_Epis.ViewModel.TipoEpi;

namespace Controle_de_Epis.Service.Interfaces
{
    public interface ITipoEpiService
    {
        Task<List<ListarTipoEpiViewModel>> ListarTipoEpisAsync();
        Task<ResultError<CriarTipoEpiViewModel>> CriarTipoEpiAsync(CriarTipoEpiViewModel tipoEpi);
        Task<ResultError<bool>> AlterarStatusTipoEpiAsync(int id, bool status);
    }
}