using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DevApp.Mvc.ViewModels;
using DevApp.Business.Models.Produtos;
using DevApp.Business.Models.Produtos.Services;
using DevApp.Infra.Data.Repository;
using DevApp.Business.Core.Notificacoes;
using AutoMapper;
using DevApp.Business.Models.Fornecedores;

namespace DevApp.Mvc.Controllers
{
    public class ProdutosController : BaseController
    {   //Readonly: uma vez que instanciamos, não pode ser alterado.
        private readonly IProdutoRepository _produtoRepository;  //Ler dados do banco
        private readonly IProdutoService _produtoService; //Para persistência(salvar no banco)
        //private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IMapper _mapper;

        public ProdutosController(IProdutoRepository produtoRepository,
                                  IProdutoService produtoService,  //Configuração das dependências para que possa resolver os objetos em tempó de execução.
                                  //IFornecedorRepository fornecedorRepository,
                                  IMapper mapper)
                                  //INotificador notificador) : base()
        {
            _produtoRepository = produtoRepository;
            _produtoService = produtoService;
            //_fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
        }

        // GET: Produtos
        [Route("lista-de-produtos")]
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var produtoVM = _mapper.Map<IEnumerable<ProdutoViewModel>>(await _produtoRepository.ObterProdutosFornecedores());

            return View(produtoVM);
        }

        // GET: Produtos/Details/5
        [Route("detalhes-do-produto/{id:guid}")]
        [HttpGet]
        public async Task<ActionResult> Details(Guid id)
        {
            var produtoViewModel = await ObterProduto(id);    //_produtoRepository.ObterPorId(id);

            if (produtoViewModel == null)
            {
                return HttpNotFound();
            }
            return View(produtoViewModel);
        }

        // GET: Produtos/Create
        [Route("novo-produto")]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Produtos/Create
        [Route("novo-produto")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProdutoViewModel produtoViewModel) //NÃO UTILIZANDO BIND!!!
        {
            if (ModelState.IsValid)
            {
                await _produtoService.Adicionar(_mapper.Map<Produto>(produtoViewModel));               
                return RedirectToAction("Index");
            }

            return View(produtoViewModel);
        }

        // GET: Produtos/Edit/5
        [Route("editar-produto/{id:guid}")]
        [HttpGet]
        public async Task<ActionResult> Edit(Guid id)
        {
            //ProdutoViewModel produtoViewModel = await db.ProdutoViewModels.FindAsync(id);
            var produtoViewModel = await ObterProduto(id);

            if (produtoViewModel == null)
            {
                return HttpNotFound();
            }

            return View(produtoViewModel);
        }

        // POST: Produtos/Edit/5
        [Route("editar-produto/{id:guid}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProdutoViewModel produtoViewModel)
        {
            if (ModelState.IsValid)
            {
                await _produtoService.Atualizar(_mapper.Map<Produto>(produtoViewModel));
                //db.Entry(produtoViewModel).State = EntityState.Modified;
                //await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(produtoViewModel);
        }

        // GET: Produtos/Delete/5
        [Route("excluir-produto/{id:guid}")]
        [HttpGet]
        public async Task<ActionResult> Delete(Guid id)
        {
           var produtoViewModel = await ObterProduto(id);

            if (produtoViewModel == null)
            {
                return HttpNotFound();
            }
            return View(produtoViewModel);
        }

        // POST: Produtos/Delete/5
        [Route("excluir-produto/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            var produtoViewModel = ObterProduto(id);

            if (produtoViewModel == null)
            {
                return HttpNotFound();
            }
            await _produtoService.Remover(id);
            //_produtoRepository.Remove(produtoViewModel);
            //await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private async Task<ProdutoViewModel> ObterProduto(Guid id) //Retorna uma viewmodel de um produto mapeado.
        {
            var produto = _mapper.Map<ProdutoViewModel>(await _produtoRepository.ObterProdutoFornecedor(id));
            
            return produto;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _produtoRepository.Dispose();
                _produtoService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
