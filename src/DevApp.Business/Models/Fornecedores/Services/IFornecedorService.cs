using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevApp.Business.Models.Fornecedores.Services
{
    public interface IFornecedorService : IDisposable
    {
        Task Adicionar(Fornecedor fornecedor);

        Task Atualizar(Fornecedor fornecedor);
                                                //Ações que modificam o estado da entidade.
        Task Remover(Guid id);


        Task AtualizarEndereco(Endereco endereco); //Endereco do fornecedor
    }
}
