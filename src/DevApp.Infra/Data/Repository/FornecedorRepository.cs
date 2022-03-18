using DevApp.Business.Models.Fornecedores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevApp.Infra.Data.Repository
{
    class FornecedorRepository<TEntity> : Repository<Fornecedor>, IFornecedorRepository
    {
        
        public Task<Fornecedor> ObterFornecedorEndereco(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Fornecedor> ObterFornecedorProdutosEndereco(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
