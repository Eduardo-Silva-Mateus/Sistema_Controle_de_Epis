using Controle_de_Epis.Enums;
using Controle_de_Epis.Results;
using Controle_de_Epis.ViewModel.Epi;


namespace Controle_de_Epis.Service.Interfaces
{
    public interface IEpiService 
    {

        Task<List<ListarEpiViewModel>> ListarEpisAsync();
        Task<ResultError<CriarEpiViewModel>> CriarEpiAsync(CriarEpiViewModel epi);
        Task<ResultError<StatusEpi>> AlterarStatusEpiAsync(int id, StatusEpi novoStatus);
    }
}
