using AutoMapper;
using DevApp.Business.Models.Fornecedores;
using DevApp.Business.Models.Fornecedores.Services;
using DevApp.Business.Models.Produtos;
using DevApp.Business.Models.Produtos.Services;
using DevApp.Infra.Data.Repository;
using DevApp.Mvc.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace DevApp.Mvc.Controllers
{
    public class FornecedoresController : BaseController
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IFornecedorService _fornecedorService;
        private readonly IMapper _mapper;


        public FornecedoresController(IFornecedorRepository fornecedorRepository, IFornecedorService fornecedorService, IMapper mapper)
        {
            _fornecedorRepository = fornecedorRepository;
            _fornecedorService = fornecedorService;
            _mapper = mapper;
        }

        //GET
        [HttpGet]
        [Route("lista-de-fornecedores")]
        public async Task<ActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos()));
        }

        [Route("dados-do-fornecedor/{id:guid}")] //Exibindo os detalhes dos fornecedores.
        public async Task<ActionResult> Details(Guid id)
        {
            var fornecedorVM = await ObterFornecedorEndereco(id);

            if (fornecedorVM == null) return HttpNotFound();

            return View(fornecedorVM);
        }

        //GET
        [HttpGet]
        [Route("novo-fornecedor")]
        public ActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [Route("novo-fornecedor")]
        public async Task<ActionResult> Create(FornecedorViewModel fornecedorViewModel)
        {
            if (!ModelState.IsValid) return View(fornecedorViewModel);

            var fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);
            await _fornecedorService.Adicionar(fornecedor);

            //TODO:
            //E se não der certo?


            return RedirectToAction("Index");
        }

        //GET
        [HttpGet]
        [Route("editar-fornecedor/{id:guid}")]
        public async Task<ActionResult> Edit(Guid id)
        {
            var fornecedorVM = await ObterFornecedorProdutosEndereco(id);

            if(fornecedorVM == null)
            {
                return HttpNotFound();
            }

            return View(fornecedorVM);
        }

        [HttpPost]
        [Route("editar-fornecedor/{id:guid}")]
        public async Task<ActionResult> Edit(Guid id, FornecedorViewModel fornecedorViewModel)
        {
            if (id != fornecedorViewModel.Id) return HttpNotFound();

            if (!ModelState.IsValid) return View(fornecedorViewModel);

            var fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel); // fornecedorVM = entidade Fornecedor, mapeando a entidade ViewModel para a entidade base(Fornecedor)
            await _fornecedorService.Atualizar(fornecedor);

            //TODO:
            //E se não der certo?

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("excluir-fornecedor/{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var fornecedorVM = await ObterFornecedorEndereco(id);

            if (id == null) return HttpNotFound();

            return View(fornecedorVM);
        }

        
        [Route("excluir-fornecedor/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirm(Guid id)
        {
            var fornecedor = await ObterFornecedorEndereco(id);

            if (fornecedor == null) return HttpNotFound();

            await _fornecedorService.Remover(id);

            //TODO:
            //E se não der certo?

            return RedirectToAction("Index");
        }

        private async Task<FornecedorViewModel> ObterFornecedorEndereco(Guid id)
        {
            return _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterFornecedorEndereco(id));
        }

        private async Task<FornecedorViewModel> ObterFornecedorProdutosEndereco(Guid id)
        {
            return _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterFornecedorProdutosEndereco(id));
        }
    }
}