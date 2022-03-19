using DevApp.Business.Models.Produtos;
using DevApp.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;

namespace DevApp.Infra.Data.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {

        public ProdutoRepository(AppDbContext context) : base(context) { }


        public async Task<Produto> ObterProdutoFornecedor(Guid id) //Obter um produto e fornecedor
        {
            return await Db.Produtos.AsNoTracking().Include(f => f.Fornecedor)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Produto>> ObterProdutosFornecedores() //Todos os produtos de fornecedores
        {
            return await Db.Produtos.AsNoTracking().Include(f => f.Fornecedor)
                .OrderBy(p => p.Nome).ToListAsync();
        }

        public async Task<IEnumerable<Produto>> ObterProdutosPorFornecedor(Guid fornecedorId) //Apenas os produtos
        {
            return await Buscar(p => p.FornecedorId == fornecedorId);
        }
    }
}
