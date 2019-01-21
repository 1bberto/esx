using ESX.Domain.Core.Entity;
using ESX.Domain.Core.Exceptions;
using ESX.Domain.Core.Interfaces.Repository;
using ESX.Domain.Core.Interfaces.Service;
using System.Linq;
using System.Threading.Tasks;

namespace ESX.Domain.Core.Service
{
    public class MarcaService : ServiceBase<Marca, IMarcaRepository>, IMarcaService
    {
        private readonly IPatrimonioRepository _patrimonioRepository;

        public MarcaService(
            IMarcaRepository repository,
            IPatrimonioRepository patrimonioRepository) : base(repository)
        {
            _patrimonioRepository = patrimonioRepository;
        }

        public override async Task DeleteAsync(object marcaId)
        {
            var uow = GetRepository().GetUow();

            uow.Begin();

            var marca = await base.GetByIdAsync(marcaId);

            if (marca is null) throw new DomainException("Marca nao encontrada");

            var patrimonio = await _patrimonioRepository.ObterPatrimoniosPorMarcaAsync(marca.MarcaId);

            if (patrimonio.Any())
                throw new DomainException("Marca nao pode ser deletada!, a mesma possui vinculo com um ou mais patrimonios");

            await GetRepository().DeleteAsync(marcaId);

            uow.Commit();
        }

        public async Task UpdateAsync(object objId, Marca marca)
        {
            var uow = GetRepository().GetUow();

            uow.Begin();

            var item = await base.GetByIdAsync(objId);

            if (item is null)
                throw new DomainException("Marca nao encontrada!");

            if (marca.Nome != item.Nome)
            {
                var marcaNome = await GetRepository().VerificarMarcaNomeAsync(marca.Nome);
                if (marcaNome != null)
                    throw new DomainException($"Nome Ja Cadatrado na marca {marcaNome.MarcaId}!");
            }

            await GetRepository().UpdateAsync(objId, marca);

            uow.Commit();
        }

        public async Task<Marca> SaveAsync(Marca marca)
        {
            var uow = GetRepository().GetUow();

            uow.Begin();

            var marcaNome = await GetRepository().VerificarMarcaNomeAsync(marca.Nome);
            if (marcaNome != null)
                throw new DomainException($"Nome Ja Cadatrado na marca {marcaNome.MarcaId}!");

            var data = await GetRepository().SaveAsync(marca);

            uow.Commit();

            return data;
        }
    }
}