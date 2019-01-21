using ESX.Api.Models.ModelView;
using ESX.Api.ObjectResult;
using ESX.Domain.Core.Exceptions;
using ESX.Domain.Shared.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ESX.Api.Filters
{
    public class ErrorFilter : ExceptionFilterAttribute
    {
        private readonly IUnitOfWork _unitOfWork;

        public ErrorFilter(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public override void OnException(ExceptionContext context)
        {
            var transaction = _unitOfWork.GetTransaction();
            if (transaction != null)
            {
                try
                {
                    _unitOfWork.RollBack();
                }
                catch
                {
                }
            }

            bool coreError = context.Exception is DomainException;
            var mvcController = context.ActionDescriptor.RouteValues["controller"];
            var mvcAction = context.ActionDescriptor.RouteValues["action"];
            var errorMessage = $"{context.Exception.Message} {context.Exception.InnerException?.Message}";

            //if(context.ActionDescriptor.)
            var result = new BaseViewModel<ErroViewModel>
            {
                Mensagem = "Erro",
                Sucesso = false,
                ObjetoDeRetorno = new ErroViewModel()
                {
                    Core = coreError,
                    Controller = mvcController,
                    Action = mvcAction,
                    Mensagem = errorMessage
                }
            };

            if (coreError)
            {
                context.Result = new ConflictObjectResult(result);
            }
            else
                context.Result = new InternalServerErrorObjectResult(result);
        }
    }
}