using ESX.Domain.Core.Entity;
using ESX.Domain.Core.Exceptions;
using ESX.Domain.Core.Interfaces.Repository;
using ESX.Domain.Core.Interfaces.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESX.Domain.Core.Service
{
    public class PatrimonioService : ServiceBase<Patrimonio, IPatrimonioRepository>, IPatrimonioService
    {
        private readonly IMarcaRepository _marcaRepository;

        public PatrimonioService(
            IPatrimonioRepository repository,
            IMarcaRepository marcaRepository) : base(repository)
        {
            _marcaRepository = marcaRepository;
        }

        public async Task DeleteAsync(int patrimonioId)
        {
            var uow = GetRepository().GetUow();

            uow.Begin();

            var patrimonio = await base.GetByIdAsync(patrimonioId);

            if (patrimonio is null) throw new DomainException("Patrimonio nao encontrado");

            await GetRepository().DeleteAsync(patrimonioId);

            uow.Commit();
        }

        public override async Task<Patrimonio> GetByIdAsync(object patrimonioId)
        {
            var patrimonio = await base.GetByIdAsync(patrimonioId);

            patrimonio.Marca = await _marcaRepository.GetByIdAsync(patrimonio.MarcaId);

            return patrimonio;
        }

        public override async Task<IList<Patrimonio>> GetAllAsync()
        {
            var data = await base.GetAllAsync();

            foreach (var patrimonio in data)
                patrimonio.Marca = await _marcaRepository.GetByIdAsync(patrimonio.Marca);

            return data;
        }

        public async Task UpdateAsync(object objId, Patrimonio patrimonio)
        {
            var uow = GetRepository().GetUow();

            uow.Begin();

            var item = await base.GetByIdAsync(objId);

            if (item is null)
                throw new DomainException("Patrimonio nao encontrado!");

            var marca = await _marcaRepository.GetByIdAsync(patrimonio.MarcaId);

            if (marca is null)
                throw new DomainException("Marca nao encontrada!");

            await GetRepository().UpdateAsync(patrimonio.PatrimonioId, patrimonio);

            uow.Commit();
        }

        public async Task<Patrimonio> SaveAsync(Patrimonio patrimonio)
        {
            var uow = GetRepository().GetUow();

            uow.Begin();

            var item = await GetRepository().SaveAsync(patrimonio);

            item.Marca = await _marcaRepository.GetByIdAsync(item.MarcaId);

            uow.Commit();

            return item;
        }
        public async Task<IList<Patrimonio>> ObterPatrimoniosMarcaAsync(int marcaId)
        {
            var marca = await base.GetByIdAsync(marcaId);

            if (marca is null)
                throw new DomainException("Marca nao encontrada!");

            return await GetRepository().ObterPatrimoniosPorMarcaAsync(marca.MarcaId);
        }
    }
}