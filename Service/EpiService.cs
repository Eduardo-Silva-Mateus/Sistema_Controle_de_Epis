
using Controle_de_Epis.Enums;
using Controle_de_Epis.Results;
using Controle_de_Epis.Service.Interfaces;
using Controle_de_Epis.ViewModel.Epi;

namespace Controle_de_Epis.Service
{
    public class EpiService : IEpiService
    {  
        private readonly IEpiService _epiService;

        public EpiService(IEpiService epiService)
        {
            _epiService = epiService;
        }

        public async Task<List<ListarEpiViewModel>> ListarEpisAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ResultError<CriarEpiViewModel>> CriarEpiAsync(CriarEpiViewModel epi)
        {
            throw new NotImplementedException();
        }

        public async Task<ResultError<StatusEpi>> AlterarStatusEpiAsync(int id, StatusEpi novoStatus)
        {
            throw new NotImplementedException();
        }
    }
}
