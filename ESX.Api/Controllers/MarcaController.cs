using AutoMapper;
using ESX.Api.Models.ModelView;
using ESX.Api.Models.ViewModel;
using ESX.Application.Interfaces;
using ESX.Domain.Core.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ESX.Api.Controllers
{
    /// <summary>
    /// Controller Responsavel por realizar as operacoes da entidade Marca
    /// Usuario deve estar autenticado para acessar
    /// </summary>
    [Route("marcas")]
    [Authorize("Bearer")]
    public class MarcaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMarcaAppService _marcaAppService;
        public MarcaController(
            IMapper mapper,
            IMarcaAppService marcaAppService)
        {
            _mapper = mapper;
            _marcaAppService = marcaAppService;
        }
        /// <summary>
        /// Retorna todas as marcas
        /// </summary>
        /// <response code="200">Lista com todas as marcas cadastradas</response>
        /// <response code="401">Caso usuario nao estiver autenticado</response> 
        /// <response code="404">Caso nenhuma marca for encontrada</response> 
        /// <response code="409">Caso alguma regra for violada</response> 
        /// <response code="500">Erro interno desconhecido</response> 
        [HttpGet]
        [ProducesResponseType(typeof(BaseViewModel<List<MarcaModelView>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(BaseViewModel<ErroViewModel>), (int)HttpStatusCode.Conflict)]
        [ProducesResponseType(typeof(BaseViewModel<ErroViewModel>), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ObterMarcas()
        {
            var objRetorno = new BaseViewModel<List<MarcaModelView>>();

            var marcas = await _marcaAppService.GetAllAsync();

            if (marcas is null || !marcas.Any())
                return NotFound();

            var data = _mapper.Map<List<MarcaModelView>>(marcas);

            objRetorno.ObjetoDeRetorno = data;

            return Ok(objRetorno);
        }
        /// <summary>
        /// Retorna marca
        /// </summary>
        /// <response code="200">Marca</response>
        /// <response code="401">Caso usuario nao estiver autenticado</response> 
        /// <response code="404">Caso marca nao for encontrada</response> 
        /// <response code="409">Caso alguma regra for violada</response> 
        /// <response code="500">Erro interno desconhecido</response>
        [HttpGet]
        [Route("{marcaId}")]
        [ProducesResponseType(typeof(BaseViewModel<MarcaModelView>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(BaseViewModel<ErroViewModel>), (int)HttpStatusCode.Conflict)]
        [ProducesResponseType(typeof(BaseViewModel<ErroViewModel>), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ObterMarcas(int marcaId)
        {
            var objRetorno = new BaseViewModel<MarcaModelView>();

            var marca = await _marcaAppService.GetByIdAsync(marcaId);

            if (marca is null) return NotFound();

            var data = _mapper.Map<MarcaModelView>(marca);

            objRetorno.ObjetoDeRetorno = data;

            return Ok(objRetorno);
        }
        
        /// <summary>
        /// Remove marca
        /// Usuario deve ser Administador para executar esta funcionalidade
        /// </summary>
        /// <response code="200">Ok caso patrimonio seja removido</response>
        /// <response code="401">Caso usuario nao estiver autenticado</response> 
        /// <response code="404">Caso patrimonio nao for encontrado</response> 
        /// <response code="409">Caso alguma regra for violada</response> 
        /// <response code="500">Erro interno desconhecido</response>
        [HttpDelete]
        [Authorize(Policy = "Administrador")]
        [Route("{marcaId}")]
        [ProducesResponseType(typeof(BaseViewModel<bool>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(BaseViewModel<ErroViewModel>), (int)HttpStatusCode.Conflict)]
        [ProducesResponseType(typeof(BaseViewModel<ErroViewModel>), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeleteMarca(int marcaId)
        {
            var objRetorno = new BaseViewModel<bool>();

            var marca = await _marcaAppService.GetByIdAsync(marcaId);

            if (marca is null) return NotFound();

            await _marcaAppService.DeleteAsync(marcaId);

            objRetorno.ObjetoDeRetorno = true;

            return Ok(objRetorno);
        }
        /// <summary>
        /// Insere marca, 
        /// Usuario deve ser Administador para executar esta funcionalidade
        /// </summary>
        /// <response code="200">Marca inserida</response>
        /// <response code="401">Caso usuario nao estiver autenticado</response> 
        /// <response code="409">Caso alguma regra for violada</response> 
        /// <response code="500">Erro interno desconhecido</response>
        [HttpPost]
        [Authorize(Policy = "Administrador")]
        [ProducesResponseType(typeof(BaseViewModel<MarcaModelView>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(BaseViewModel<ErroViewModel>), (int)HttpStatusCode.Conflict)]
        [ProducesResponseType(typeof(BaseViewModel<ErroViewModel>), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> AdicionarMarca([FromBody] MarcaViewModel item)
        {
            if (item is null || !ModelState.IsValid)
                return BadRequest(ModelState);

            var objRetorno = new BaseViewModel<MarcaModelView>();

            var entity = _mapper.Map<Marca>(item);

            var marca = await _marcaAppService.SaveAsync(entity);

            objRetorno.ObjetoDeRetorno = _mapper.Map<MarcaModelView>(marca);

            return Ok(objRetorno);
        }
        /// <summary>
        /// Atualiza marca
        /// Usuario deve ser Administador para executar esta funcionalidade
        /// </summary>
        /// <response code="200">Ok caso marca seja atualizada</response>
        /// <response code="401">Caso usuario nao estiver autenticado</response> 
        /// <response code="404">Caso marca nao for encontrada</response> 
        /// <response code="409">Caso alguma regra for violada</response> 
        /// <response code="500">Erro interno desconhecido</response>
        [HttpPut]
        [Authorize(Policy = "Administrador")]
        [Route("{marcaId}")]
        [ProducesResponseType(typeof(BaseViewModel<bool>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(BaseViewModel<ErroViewModel>), (int)HttpStatusCode.Conflict)]
        [ProducesResponseType(typeof(BaseViewModel<ErroViewModel>), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> AdicionarMarca(
            int marcaId,
            [FromBody] MarcaViewModel item)
        {
            if (item is null || !ModelState.IsValid)
                return BadRequest(ModelState);

            var objRetorno = new BaseViewModel<bool>();

            var marca = await _marcaAppService.GetByIdAsync(marcaId);

            if (marca is null) return NotFound();

            var entity = _mapper.Map<Marca>(item);

            await _marcaAppService.UpdateAsync(marcaId, entity);

            objRetorno.ObjetoDeRetorno = true;

            return Ok(objRetorno);
        }
        /// <summary>
        /// Retorna todos os patrimonios pela marca
        /// </summary>
        /// <response code="200">Todos os patrimonios que estao cadastrados com a marca</response>
        /// <response code="401">Caso usuario nao estiver autenticado</response> 
        /// <response code="404">Caso marca nao for encontrada</response> 
        /// <response code="404">Caso nenhum patrimonio nao for encontrado</response> 
        /// <response code="409">Caso alguma regra for violada</response> 
        /// <response code="500">Erro interno desconhecido</response>
        [HttpGet]
        [Route("{marcaId}/patrimônios")]
        [ProducesResponseType(typeof(BaseViewModel<List<PatrimonioModelView>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(BaseViewModel<ErroViewModel>), (int)HttpStatusCode.Conflict)]
        [ProducesResponseType(typeof(BaseViewModel<ErroViewModel>), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ObterPatrimoniosMarca(
            int marcaId,
            [FromServices] IPatrimonioAppService patrimonioAppService)
        {
            var objRetorno = new BaseViewModel<List<PatrimonioModelView>>();

            var marca = await _marcaAppService.GetByIdAsync(marcaId);

            if (marca is null) return NotFound();

            var patrimoniosMarca = await patrimonioAppService.ObterPatrimoniosMarcaAsync(marcaId);

            if (patrimoniosMarca is null) return NotFound();

            var data = _mapper.Map<List<PatrimonioModelView>>(patrimoniosMarca);

            objRetorno.ObjetoDeRetorno = data;

            return Ok(objRetorno);
        }
    }
}