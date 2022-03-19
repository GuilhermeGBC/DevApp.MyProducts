using DevApp.Business.Core.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevApp.Business.Core.Services
{
   public abstract class BaseService 
    {
        //Executa qualquer validação de qualquer entidade utilizando programação genérica

                                                                        //TV tem que ser classe/herdar de AbstractValidator da entidade genérica que passei onde TE herda de Entity
        protected bool ExecutarValidacao<TV, TE>(TV validacao, TE entidade) where TV: AbstractValidator<TE> where TE : Entity//Receber validação e a entidade e devolver o resultado.
        {
            var validator = validacao.Validate(entidade);

            return validator.IsValid;
        }
    }
}
