using DevApp.Business.Core.Models;
using DevApp.Business.Models.Fornecedores;
using System;

namespace DevApp.Business.Models.Produtos
{
    public class Produto : Entity
    {


        public Guid FornecedorId { get; set; }

        public string Nome { get; set; }

        public string Descricao { get; set; }

        public string Imagem { get; set; }

        public decimal Valor { get; set; }

        public DateTime DataCadastro { get; set; }

        public bool Ativo { get; set; }


        public Fornecedor Fornecedor { get; set; }
    }
}
