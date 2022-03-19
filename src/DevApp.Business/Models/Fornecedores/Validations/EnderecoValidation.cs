using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevApp.Business.Models.Fornecedores.Validations
{
    class EnderecoValidation : AbstractValidator<Endereco>
    {
        public EnderecoValidation()
        {
            RuleFor(e => e.Logradouro)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido!")
                .Length(2,200).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres!");

            RuleFor(e => e.Bairro)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido!")
                .Length(2, 200).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres!");

            RuleFor(e => e.Cep)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido!")
                .Length(8).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres!");

            RuleFor(e => e.Cidade)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido!")
                .Length(2, 200).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres!");

            RuleFor(e => e.Estado)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido!")
                .Length(2, 50).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres!");

            RuleFor(e => e.Numero)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido!")
                .Length(1, 50).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres!");

        }
    }
}
