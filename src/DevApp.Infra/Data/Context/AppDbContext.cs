using DevApp.Business.Models.Fornecedores;
using DevApp.Business.Models.Produtos;
using DevApp.Infra.Data.Mappings;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevApp.Infra.Data.Context
{
   public class AppDbContext : DbContext
   {
        public AppDbContext() : base(nameOrConnectionString:"DefaultConnection")
        {
            Configuration.ProxyCreationEnabled = false; //Desabilitar o proxy ------------Não usamos pra nada
            Configuration.LazyLoadingEnabled = false;   //Desabilitar lazyloading
        }

        public DbSet<Produto> Produtos { get; set; }

        public DbSet<Endereco> Enderecos { get; set; }

        public DbSet<Fornecedor> Fornecedores { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties<string>()
                .Configure(p => p
                .HasColumnType("varchar") //Toda propriedade que for tipo string, será passada para varchar com tamanho máximo em 100.
                .HasMaxLength(100));


            modelBuilder.Configurations.Add(new FornecedorConfig());
            modelBuilder.Configurations.Add(new EnderecoConfig());
            modelBuilder.Configurations.Add(new ProdutoConfig());

            base.OnModelCreating(modelBuilder);

        }
    }
}
