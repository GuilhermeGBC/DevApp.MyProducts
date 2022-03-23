using DevApp.Business.Core.Notificacoes;
using DevApp.Business.Core.Services;
using DevApp.Business.Models.Fornecedores.Validations;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DevApp.Business.Models.Fornecedores.Services
{
    public class FornecedorService : BaseService, IFornecedorService
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IEnderecoRepository _enderecoRepository;

        public FornecedorService(IFornecedorRepository fornecedorRepository,
                                 IEnderecoRepository enderecoRepository,
                                 INotificador notificador) : base(notificador)
        {
            _fornecedorRepository = fornecedorRepository;
            _enderecoRepository = enderecoRepository;
        }

        public async Task Adicionar(Fornecedor fornecedor)
        {
            if (!ExecutarValidacao(new FornecedorValidation(), fornecedor) //Quando executar a validação, internamente já notificamos o erro que ficara disponível na lista para pegarmos em outro momento.
                || !ExecutarValidacao(new EnderecoValidation(), entidade: fornecedor.Endereco)) return;

            if (await FornecedorExistente(fornecedor)) return;

            await _fornecedorRepository.Adicionar(fornecedor);
        }

        public async Task Atualizar(Fornecedor fornecedor)
        {
            if (!ExecutarValidacao(new FornecedorValidation(), fornecedor)) return;

            if (await FornecedorExistente(fornecedor)) return;

            await _fornecedorRepository.Atualizar(fornecedor);
        }

        public async Task AtualizarEndereco(Endereco endereco)
        {
            if (!ExecutarValidacao(new EnderecoValidation(), endereco)) return;

            await _enderecoRepository.Atualizar(endereco);
        }

        public async Task<bool> FornecedorExistente(Fornecedor fornecedor)
        {
            var fornecedorAtual = await _fornecedorRepository.Buscar(f => f.Documento == fornecedor.Documento && f.Id != fornecedor.Id);

            if (!fornecedorAtual.Any()) return false;//Se não encontrar nenhum fornecedor, por que a validação é se o fornecedor existe.

            Notificar("Já existe um fornecedor com este documento informadoi"); //Caso exista
            return true;
        }

        public async Task Remover(Guid id)
        {
            var fornecedor = await _fornecedorRepository.ObterFornecedorProdutosEndereco(id);

            if (fornecedor.Produtos.Any()) //Não deletar um fornecedor se tiver produtos.
            {
                Notificar("O fornecedor possui produtos cadastrados!");
                return;
            }

            if (fornecedor.Endereco != null)
            {
                await _enderecoRepository.Remover(fornecedor.Endereco.Id); //Validar o endereço para removê-lo
            }

            await _enderecoRepository.Remover(id); //Após remover o endereço, remove o fornecedor
        }

        public void Dispose()
        {
            _enderecoRepository?.Dispose(); //Não ficar com o objeto alocado na memória; Caso a instância não exista, não chama o método.
            _fornecedorRepository?.Dispose();
        }
    }
}
