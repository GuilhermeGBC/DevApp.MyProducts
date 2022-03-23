using DevApp.Business.Core.Models;
using DevApp.Business.Core.Notificacoes;
using FluentValidation;
using FluentValidation.Results;

namespace DevApp.Business.Core.Services
{
    public abstract class BaseService
    {
        //Executa qualquer validação de qualquer entidade utilizando programação genérica
        private readonly INotificador _notificador;

        protected BaseService(INotificador notificador)
        {
            _notificador = notificador;
        }

        protected void Notificar(ValidationResult validationResult) //Toda vez que executarmos uma validação, chamamos essa notificação
        {
            foreach (var error in validationResult.Errors) //Lista de erros
            {
                Notificar(error.ErrorMessage);
            }
        }
        protected void Notificar(string mensagem) //Sobrecarga do método acima
        {
            _notificador.Handle(new Notificacao(mensagem)); //Criando e manipulando nova notificação
        }

        //TV tem que ser classe/herdar de AbstractValidator da entidade genérica que passei onde TE herda de Entity
        protected bool ExecutarValidacao<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : Entity //Receber validação e a entidade e devolver o resultado.
        {
            var validator = validacao.Validate(entidade); //ValidationResult do prório asp.net(representação do erro em data annotation) e também do fluent validation

            if (validator.IsValid) return true;

            Notificar(validator);

            return false;
        }
    }
}
