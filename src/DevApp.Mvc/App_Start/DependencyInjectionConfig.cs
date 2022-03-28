using DevApp.Business.Core.Notificacoes;
using DevApp.Business.Models.Fornecedores;
using DevApp.Business.Models.Fornecedores.Services;
using DevApp.Business.Models.Produtos;
using DevApp.Business.Models.Produtos.Services;
using DevApp.Infra.Data.Context;
using DevApp.Infra.Data.Repository;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace DevApp.Mvc.App_Start
{
    public class DependencyInjectionConfig
    {
        public static void RegisterDIContainer()
        {
            var container = new Container();

            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle(); //aplicação web

            InitializeContainer(container); //Cria os objetos: repositorio, serviço, automapper, etc.

            container.RegisterMvcControllers(assemblies: Assembly.GetExecutingAssembly()); //Registrando controllers, valida se estão configuradas para o SimpleInjector identificando todas as controllers que herdam de Controller

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container)); //
        }

        private static void InitializeContainer(Container container)
        {
            container.Register<AppDbContext>(Lifestyle.Scoped);
            container.Register<IProdutoRepository, ProdutoRepository>(Lifestyle.Scoped);
            container.Register<IProdutoService, ProdutoService>(Lifestyle.Scoped);
            container.Register<IFornecedorRepository, FornecedorRepository>(Lifestyle.Scoped);
            container.Register<IEnderecoRepository, EnderecoRepository>(Lifestyle.Scoped);
            container.Register<IFornecedorService, FornecedorService>(Lifestyle.Scoped);
            container.Register<INotificador, Notificador>(Lifestyle.Scoped);

            container.RegisterSingleton(instanceCreator:() => AutoMapperConfig.GetMapperConfiguration().CreateMapper(container.GetInstance));
        }
    }
}