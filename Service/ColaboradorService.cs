using Controle_de_Epis.Data;
using Microsoft.EntityFrameworkCore;
using Controle_de_Epis.Service.Interfaces;
using Controle_de_Epis.ViewModel.Colaborador;
using Controle_de_Epis.Models;
using Controle_de_Epis.Results;

namespace Controle_de_Epis.Service
{
    public class ColaboradorService : IColaboradorService
    {
        private readonly BancoContext _colaboradorcontext;

        public ColaboradorService(BancoContext colaboradorcontext)
        {
            _colaboradorcontext = colaboradorcontext;
        }

        public async Task<List<ListarColaboradorViewModel>> GetAllColaboradoresAsync()
        {
            var colaboradores = await _colaboradorcontext.Colaboradores
                .OrderBy(u => u.NomeColaborador)
                .ThenBy(u => u.CargoColaborador)
                .ToListAsync();

            var lista = new List<ListarColaboradorViewModel>();
            foreach(var colaborador in colaboradores)
            {
          
                lista.Add(new ListarColaboradorViewModel
                {
                    Id = colaborador.Id,
                    NomeColaborador = colaborador.NomeColaborador,
                    CpfColaborador = colaborador.CpfColaborador,
                    CargoColaborador = colaborador.CargoColaborador,
                    SetorColaborador = colaborador.SetorColaborador,
                    AtivoColaborador = colaborador.AtivoColaborador
                });
            }
            return lista;
        }

        public async Task<ResultError<CriarColaboradorViewModel>> CriarColaboradorAsync(CriarColaboradorViewModel colaborador)
        {
            var cpfExiste = await _colaboradorcontext.Colaboradores
                .AnyAsync(c => c.CpfColaborador == colaborador.CpfColaborador);

            if(cpfExiste)
            {
                return ResultError<CriarColaboradorViewModel>
                    .Falha("Já existe um colaborador cadastrado com esse CPF.");
            }

            var novoColaborador = new ColaboradorModel
            {
                NomeColaborador = colaborador.NomeColaborador,
                CpfColaborador = colaborador.CpfColaborador,
                CargoColaborador = colaborador.CargoColaborador,
                SetorColaborador = colaborador.SetorColaborador,
                AtivoColaborador = colaborador.AtivoColaborador
            };

            await _colaboradorcontext.Colaboradores.AddAsync(novoColaborador);
            await _colaboradorcontext.SaveChangesAsync();

            colaborador.Id = novoColaborador.Id;

            return ResultError<CriarColaboradorViewModel>.Ok(colaborador);
        }

        public async Task<ResultError<EditarColaboradorViewModel>> GetColaboradorByIdAsync(int id)
        {
            var colaboradores = await _colaboradorcontext.Colaboradores
                .FirstOrDefaultAsync( c=> c.Id == id);

            if(colaboradores == null)
            {
                return ResultError<EditarColaboradorViewModel>.Falha("Colaborador não existe!");
            }

            return ResultError<EditarColaboradorViewModel>.Ok( new EditarColaboradorViewModel
            {
                id = colaboradores.Id,
                NomeColaborador = colaboradores.NomeColaborador,
                CargoColaborador = colaboradores.CargoColaborador,
                SetorColaborador = colaboradores.SetorColaborador
            });
        }

        public async Task<ResultError<EditarColaboradorViewModel>> UpdateColaboradorAsync(EditarColaboradorViewModel colaborador)
        {
            var colaboradorExiste = await _colaboradorcontext.Colaboradores
                .FirstOrDefaultAsync(c => c.Id == colaborador.id);

            if(colaboradorExiste == null)
            {
                return ResultError<EditarColaboradorViewModel>
                    .Falha("Colaborador não encontrado");
            }

            colaboradorExiste.NomeColaborador = colaborador.NomeColaborador;
            colaboradorExiste.CargoColaborador = colaborador.CargoColaborador;
            colaboradorExiste.SetorColaborador = colaborador.SetorColaborador;

            await _colaboradorcontext.SaveChangesAsync();

            return ResultError<EditarColaboradorViewModel>.Ok(colaborador);
        }

        public async Task<ResultError<bool>> AlterarStatusAsync(int id)
        {
            var colaborador = await _colaboradorcontext.Colaboradores
                .FirstOrDefaultAsync(c => c.Id == id);

            if (colaborador == null)
            {
                return ResultError<bool>
                    .Falha("Colaborador não encontrado.");
            }

            colaborador.AtivoColaborador = !colaborador.AtivoColaborador;

            await _colaboradorcontext.SaveChangesAsync();

            return ResultError<bool>.Ok(colaborador.AtivoColaborador);
        }
    }
}
