using DevApp.Business.Core.Validations.Documentos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevApp.Business.Models.Fornecedores.Validations
{  //Classe especifica para validação do fornecedor.
    public class FornecedorValidation : AbstractValidator<Fornecedor>
    {

        public FornecedorValidation()
        {
            RuleFor(f => f.Nome)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido!")
                .Length(2,200)
                .WithMessage("O campo {PropertyName} deve ter entre {MinLenght} e {MaxLength} caracteres!"); 

            When(f => f.TipoFornecedor == TipoFornecedor.PessoaFisica, () => { //Validando quando for pessoa física/jurídica. Comparação.
                RuleFor(f => f.Documento.Length).Equal(toCompare: CpfValidacao.TamanhoCpf)
                .WithMessage("O Campo {PropertyName} precisa ter {ComparisonValue} caracteres e foi fornecido {Propertyvalue}"); //Criando a validação a partir das classes criadas no ValidacaoDocs

                RuleFor(f => CpfValidacao.Validar(f.Documento)).Equal(toCompare: true)
                .WithMessage("O documento fornecido é inválido");
            });

            When(f => f.TipoFornecedor == TipoFornecedor.PessoaJuridica, () => { //Validando quando for pessoa física/jurídica. Comparação.
                RuleFor(f => f.Documento.Length).Equal(toCompare: CnpjValidacao.TamanhoCnpj)
                .WithMessage("O Campo {PropertyName} precisa ter {ComparisonValue} caracteres e foi fornecido {Propertyvalue}");

                RuleFor(f => CnpjValidacao.Validar(f.Documento)).Equal(toCompare: true) //f.Documento, pegando o valor e validando, Equal = resultado do valor que estamos validando e compara com true
                .WithMessage("O documento fornecido é inválido");
            });
        }
    }
}
