using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web;
using System.Collections.Generic;

namespace DevApp.Mvc.ViewModels
{
    public class ProdutoViewModel
    {
        public ProdutoViewModel()
        {
            Id = Guid.NewGuid();

        }

        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")] //Representa de qual fornecedor é o produto
        [DisplayName("Fornecedor")]
        public Guid FornecedorId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Nome { get; set; }

        [DisplayName("Descrição")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(1000, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Descricao { get; set; }

        //[DisplayName("Imagem do Produto")]
        //public HttpPostedFileBase ImagemUpload { get; set; } //Representa a imagem que subiu em formato binário

        public string Imagem { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public decimal Valor { get; set; }

        [ScaffoldColumn(false)] //Não iremos exibir esse campo, pois ele é definido depois
        public DateTime DataCadastro { get; set; }

        [DisplayName("Ativo?")]
        public bool Ativo { get; set; }


        //public FornecedorViewModel Fornecedor { get; set; } //Representa o produto

        //public IEnumerable<FornecedorViewModel> Fornecedores { get; set; } //Lista que ajudará a exibir o dropdownlist de todos fornecedores
    }
}