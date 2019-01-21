using AutoMapper;
using ESX.Api.Models.ModelView;
using ESX.Api.Models.ViewModel;
using ESX.Api.Security;
using ESX.Application.Interfaces;
using ESX.Domain.Core.Entity;
using ESX.Domain.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace ESX.Api.Controllers
{
    [Route("usuario")]
    [Produces("application/json")]
    public class UsuarioController : ControllerBase
    {
        private readonly IMapper _mapper;
        public UsuarioController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(typeof(BaseViewModel<TokenModelView>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(BaseViewModel<ErroViewModel>), (int)HttpStatusCode.Conflict)]
        [ProducesResponseType(typeof(BaseViewModel<ErroViewModel>), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Login(
            [FromBody] UsuarioLoginViewModel item,
            [FromServices]TokenConfiguration tokenConfiguration,
            [FromServices]SignConfigurationToken signinConfiguration,
            [FromServices]TokenGenerator tokenGenerator,
            [FromServices]IUsuarioAppService usuarioAppService)
        {
            if (item is null || !ModelState.IsValid)
                return BadRequest(ModelState);

            var objRetorno = new BaseViewModel<TokenModelView>();

            var usuario = _mapper.Map<Usuario>(item);

            var userBase = await usuarioAppService.ValidarUsuarioAsync(usuario);

            if (userBase == null) return Unauthorized();

            objRetorno.ObjetoDeRetorno = tokenGenerator.GenerateToken(userBase, tokenConfiguration);

            return Ok(objRetorno);
        }

        [HttpPost]
        [Authorize(Policy = "Bearer")]
        [Authorize(Policy = "Administrador")]
        [Route("/adicionar-role/{usuarioId}")]
        [ProducesResponseType(typeof(BaseViewModel<bool>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(BaseViewModel<ErroViewModel>), (int)HttpStatusCode.Conflict)]
        [ProducesResponseType(typeof(BaseViewModel<ErroViewModel>), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> AdicionarRoleUsuario(
            string usuarioId,
            [FromBody] RoleViewModel item,
            [FromServices]TokenConfiguration tokenConfiguration,
            [FromServices]SignConfigurationToken signinConfiguration,
            [FromServices]TokenGenerator tokenGenerator,
            [FromServices]IUsuarioAppService usuarioAppService)
        {
            if (item is null || !ModelState.IsValid)
                return BadRequest(ModelState);

            var objRetorno = new BaseViewModel<bool>();

            var role = _mapper.Map<Role>(item);

            await usuarioAppService.AdicionarRoleUsuarioAsync(usuarioId, role);

            objRetorno.ObjetoDeRetorno = true;

            return Ok(objRetorno);
        }

        [HttpPost]
        [Authorize(Policy = "Bearer")]
        [Authorize(Policy = "Administrador")]
        [ProducesResponseType(typeof(BaseViewModel<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(BaseViewModel<ErroViewModel>), (int)HttpStatusCode.Conflict)]
        [ProducesResponseType(typeof(BaseViewModel<ErroViewModel>), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> AdicionarUsuario(
            [FromBody] UsuarioViewModel item,
            [FromServices]IUsuarioAppService usuarioAppService)
        {
            if (item is null || !ModelState.IsValid)
                return BadRequest(ModelState);

            var objRetorno = new BaseViewModel<string>();

            var usuario = _mapper.Map<Usuario>(item);

            objRetorno.ObjetoDeRetorno = await usuarioAppService.AdicionarUsuarioAsync(usuario);

            return Ok(objRetorno);
        }
    }
}