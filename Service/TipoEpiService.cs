using Controle_de_Epis.Data;
using Controle_de_Epis.Models;
using Controle_de_Epis.Results;
using Controle_de_Epis.Service.Interfaces;
using Controle_de_Epis.ViewModel.TipoEpi;
using Microsoft.EntityFrameworkCore;

namespace Controle_de_Epis.Service
{
    public class TipoEpiService : ITipoEpiService
    {
        private readonly BancoContext _tipoEpiContext;

        public TipoEpiService(BancoContext tipoEpiContext)
        {
            _tipoEpiContext = tipoEpiContext;
        }

        public async Task<List<ListarTipoEpiViewModel>> ListarTipoEpisAsync()
        {
            var tipoEpis = await _tipoEpiContext.TipoEpi
                .OrderBy(t => t.NomeTipoEpi)
                .ThenBy(t => t.StatusTipoEpi)
                .ToListAsync();

            var lista = new List<ListarTipoEpiViewModel>();
            foreach (var tipoEpi in tipoEpis) 
            {
                lista.Add(new ListarTipoEpiViewModel
                {
                    id = tipoEpi.Id,
                    NomeTipoEpi = tipoEpi.NomeTipoEpi,
                    VidaUtilTipoEpi = tipoEpi.VidaUtilTipoEpi,
                    ObrigatorioCA = tipoEpi.ObrigatorioCA,
                    StatusTipoEpi = tipoEpi.StatusTipoEpi
                });
            }
            return lista;
        }
        public async Task<ResultError<CriarTipoEpiViewModel>> CriarTipoEpiAsync(CriarTipoEpiViewModel tipoEpi)
        {
            var novoTipoEpi = new TipoEpiModel
            {
                NomeTipoEpi = tipoEpi.NomeTipoEpi,
                VidaUtilTipoEpi = tipoEpi.VidaUtilTipoEpi,
                ObrigatorioCA = tipoEpi.ObrigatorioCA,
                StatusTipoEpi = tipoEpi.StatusTipoEpi
            };

            await _tipoEpiContext.TipoEpi.AddAsync(novoTipoEpi);
            await _tipoEpiContext.SaveChangesAsync();

            tipoEpi.Id = novoTipoEpi.Id;

            return ResultError<CriarTipoEpiViewModel>.Ok(tipoEpi);
        }
   
        public async Task<ResultError<bool>> AlterarStatusTipoEpiAsync(int id, bool status)
        {
            var tipoEpi = await _tipoEpiContext.TipoEpi
                .FirstOrDefaultAsync(t => t.Id == id);

            if(tipoEpi == null)
            {
                return ResultError<bool>.Falha("Tipo de EPI não encontrado.");
            }

            tipoEpi.StatusTipoEpi = !tipoEpi.StatusTipoEpi;

            await _tipoEpiContext.SaveChangesAsync();

            return ResultError<bool>.Ok(tipoEpi.StatusTipoEpi);
         }
    }
}
