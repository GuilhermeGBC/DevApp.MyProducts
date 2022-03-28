using AutoMapper;
using DevApp.Business.Models.Fornecedores;
using DevApp.Business.Models.Produtos;
using DevApp.Mvc.ViewModels;
using System.Reflection;
using System.Linq;
using System;

namespace DevApp.Mvc.App_Start
{
    public class AutoMapperConfig //Configuração para encontrar qualquer classe que herde de profile do automapper e na inicialização já fazer o mapeamento para conhecer os perfis de mapeamento.
    {
        public static MapperConfiguration GetMapperConfiguration()
        {
            var profiles = Assembly.GetExecutingAssembly() //Pegar todos os assemblys na execução da aplicação
                .GetTypes() //Tipos dos assemblys
                .Where(x => typeof(Profile).IsAssignableFrom(x)); //Assemblys do tipo Profile

            //profiles será uma coleção de objetos que herdam de profile

            return new MapperConfiguration(configure: cfg =>
            {
                foreach (var profile in profiles)
                    cfg.AddProfile(Activator.CreateInstance(profile) as Profile);
            });
        }
    }

    public class AutoMapperProfile : Profile //Quando herdamos de profile, estamos "ensinando" como mapear essas classes de uma para a outra.
    {
        public AutoMapperProfile()
        {
            CreateMap<Fornecedor, FornecedorViewModel>().ReverseMap(); //Mapeando de um lado para o outro e vice-versa.
            CreateMap<Endereco, EnderecoViewModel>().ReverseMap();
            CreateMap<Produto, ProdutoViewModel>().ReverseMap();
        }
    }
}