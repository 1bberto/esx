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
    /// Controller Responsavel por realizar as operacoes da entidade Patrimonio
    /// Usuario deve estar autenticado para acessar
    /// </summary>
    [Route("patrimonios")]
    [Authorize("Bearer")]
    public class PatrimonioController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPatrimonioAppService _patrimonioAppService;

        public PatrimonioController(
            IMapper mapper,
            IPatrimonioAppService patrimonioAppService)
        {
            _mapper = mapper;
            _patrimonioAppService = patrimonioAppService;
        }
        /// <summary>
        /// Retorna todos os patrimonios
        /// </summary>
        /// <response code="200">Lista com todos os patrimonios cadastrados</response>
        /// <response code="401">Caso usuario nao estiver autenticado</response> 
        /// <response code="404">Caso nenhum patrimonio for encontrado</response> 
        /// <response code="409">Caso alguma regra for violada</response> 
        /// <response code="500">Erro interno desconhecido</response> 
        [HttpGet]
        [ProducesResponseType(typeof(BaseViewModel<List<PatrimonioModelView>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(BaseViewModel<ErroViewModel>), (int)HttpStatusCode.Conflict)]
        [ProducesResponseType(typeof(BaseViewModel<ErroViewModel>), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ObterPatrimonios()
        {
            var objRetorno = new BaseViewModel<List<PatrimonioModelView>>();

            var patrimonios = await _patrimonioAppService.GetAllAsync();

            if (patrimonios is null || !patrimonios.Any())
                return NotFound();

            var data = _mapper.Map<List<PatrimonioModelView>>(patrimonios);

            objRetorno.ObjetoDeRetorno = data;

            return Ok(objRetorno);
        }
        /// <summary>
        /// Retorna patrimonio
        /// </summary>
        /// <response code="200">Patrimonio</response>
        /// <response code="401">Caso usuario nao estiver autenticado</response> 
        /// <response code="404">Caso patrimonio nao for encontrado</response> 
        /// <response code="409">Caso alguma regra for violada</response> 
        /// <response code="500">Erro interno desconhecido</response>
        [HttpGet]
        [Route("{patrimonioId}")]
        [ProducesResponseType(typeof(BaseViewModel<PatrimonioModelView>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(BaseViewModel<ErroViewModel>), (int)HttpStatusCode.Conflict)]
        [ProducesResponseType(typeof(BaseViewModel<ErroViewModel>), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> ObterPatrimonios(int patrimonioId)
        {
            var objRetorno = new BaseViewModel<PatrimonioModelView>();

            var patrimonio = await _patrimonioAppService.GetByIdAsync(patrimonioId);

            if (patrimonio is null) return NotFound();

            var data = _mapper.Map<PatrimonioModelView>(patrimonio);

            objRetorno.ObjetoDeRetorno = data;

            return Ok(objRetorno);
        }
        /// <summary>
        /// Remove patrimonio
        /// Usuario deve ser Administador para executar esta funcionalidade
        /// </summary>
        /// <response code="200">Ok caso patrimonio seja removido</response>
        /// <response code="401">Caso usuario nao estiver autenticado</response> 
        /// <response code="404">Caso patrimonio nao for encontrado</response> 
        /// <response code="409">Caso alguma regra for violada</response> 
        /// <response code="500">Erro interno desconhecido</response>
        [HttpDelete]
        [Authorize(Policy = "Administrador")]
        [Route("{patrimonioId}")]
        [ProducesResponseType(typeof(BaseViewModel<bool>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(BaseViewModel<ErroViewModel>), (int)HttpStatusCode.Conflict)]
        [ProducesResponseType(typeof(BaseViewModel<ErroViewModel>), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeletePatrimonio(int patrimonioId)
        {
            var objRetorno = new BaseViewModel<bool>();

            var patrimonio = await _patrimonioAppService.GetByIdAsync(patrimonioId);

            if (patrimonio is null) return NotFound();

            await _patrimonioAppService.DeleteAsync(patrimonioId);

            objRetorno.ObjetoDeRetorno = true;

            return Ok(objRetorno);
        }
        /// <summary>
        /// Insere patrimonio, 
        /// Usuario deve ser Administador para executar esta funcionalidade
        /// </summary>
        /// <response code="200">Patrimonio inserido</response>
        /// <response code="401">Caso usuario nao estiver autenticado</response> 
        /// <response code="409">Caso alguma regra for violada</response> 
        /// <response code="500">Erro interno desconhecido</response>
        [HttpPost]
        [Authorize(Policy = "Administrador")]
        [ProducesResponseType(typeof(BaseViewModel<PatrimonioModelView>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(BaseViewModel<ErroViewModel>), (int)HttpStatusCode.Conflict)]
        [ProducesResponseType(typeof(BaseViewModel<ErroViewModel>), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> AdicionarPatrimonio([FromBody] PatrimonioViewModel item)
        {
            if (item is null || !ModelState.IsValid)
                return BadRequest(ModelState);

            var objRetorno = new BaseViewModel<PatrimonioModelView>();

            var entity = _mapper.Map<Patrimonio>(item);

            var patrimonio = await _patrimonioAppService.SaveAsync(entity);

            objRetorno.ObjetoDeRetorno = _mapper.Map<PatrimonioModelView>(patrimonio);

            return Ok(objRetorno);
        }
        /// <summary>
        /// Atualiza patrimonio
        /// Usuario deve ser Administador para executar esta funcionalidade
        /// </summary>
        /// <response code="200">Ok caso patrimonio seja atualizado</response>
        /// <response code="401">Caso usuario nao estiver autenticado</response> 
        /// <response code="404">Caso patrimonio nao for encontrado</response> 
        /// <response code="409">Caso alguma regra for violada</response> 
        /// <response code="500">Erro interno desconhecido</response>
        [HttpPut]
        [Authorize(Policy = "Administrador")]
        [Route("{patrimonioId}")]
        [ProducesResponseType(typeof(BaseViewModel<bool>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(BaseViewModel<ErroViewModel>), (int)HttpStatusCode.Conflict)]
        [ProducesResponseType(typeof(BaseViewModel<ErroViewModel>), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> AdicionarPatrimonio(
            int patrimonioId,
            [FromBody] PatrimonioViewModel item)
        {
            if (item is null || !ModelState.IsValid)
                return BadRequest(ModelState);

            var objRetorno = new BaseViewModel<bool>();

            var patrimonio = await _patrimonioAppService.GetByIdAsync(patrimonioId);

            if (patrimonio is null) return NotFound();

            var entity = _mapper.Map<Patrimonio>(item);

            await _patrimonioAppService.UpdateAsync(patrimonioId, entity);

            objRetorno.ObjetoDeRetorno = true;

            return Ok(objRetorno);
        }
    }
}