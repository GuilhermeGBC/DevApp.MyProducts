using DevApp.Business.Models.Fornecedores;
using System;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using DevApp.Infra.Data.Context;
using DevApp.Business.Core.Models;

namespace DevApp.Infra.Data.Repository
{
   public class FornecedorRepository : Repository<Fornecedor>, IFornecedorRepository
    {


        public FornecedorRepository(AppDbContext context) : base(context) { }

        public async Task<Fornecedor> ObterFornecedorEndereco(Guid id)
        {
            return await Db.Fornecedores.AsNoTracking()
                .Include(f => f.Endereco)
                .FirstOrDefaultAsync(f => f.Id == id); //Retornar fornecedor mais o endereço populado.
        }

        public async Task<Fornecedor> ObterFornecedorProdutosEndereco(Guid id)
        {
            return await Db.Fornecedores.AsNoTracking()
                .Include(f => f.Endereco)
                .Include(f => f.Produtos) //Joins
                .FirstOrDefaultAsync(f => f.Id == id);
        }
        //public Fornecedor Teste()
        //{
        //    return Buscar(f => f.Ativo && f.Documento == "" && f.Nome == "").Result.FirstOrDefault(); //Exemplo de query com algumas condições.
        //}

        //public override async Task Remover(Guid id)
        //{
        //    var fornecedor = ObterPorId(id);
        //    fornecedor.Ativo = false;

        //    await Atualizar(fornecedor);
        //}
    }
}
