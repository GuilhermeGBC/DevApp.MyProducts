using DevApp.Business.Core.Notificacoes;
using DevApp.Business.Core.Services;
using DevApp.Business.Models.Fornecedores.Validations;
using System;
using System.Threading.Tasks;

namespace DevApp.Business.Models.Produtos.Services
{
    public class ProdutoService : BaseService, IProdutoService
    {

        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository, INotificador notificador) : base(notificador)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task Adicionar(Produto produto)
        {
            if (!ExecutarValidacao(new ProdutoValidation(), produto)) return;

            //if (true)
            //    Notificar("Mensagem de erro!"); Podemos colocar mensagem de erro caso ocorra algum erro na validação

            await _produtoRepository.Adicionar(produto);
        }

        public async Task Atualizar(Produto produto)
        {
            if (!ExecutarValidacao(new ProdutoValidation(), produto)) return;
            await _produtoRepository.Atualizar(produto);
        }

        public async Task Remover(Guid id)
        {
            await _produtoRepository.Remover(id);
        }

        public void Dispose()
        {
            _produtoRepository?.Dispose();
        }
    }
}
