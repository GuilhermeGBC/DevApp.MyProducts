using DevApp.Business.Models.Produtos;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevApp.Infra.Data.Mappings
{
    class ProdutoConfig : EntityTypeConfiguration<Produto>
    {
        public ProdutoConfig()
        {
            HasKey(p => p.Id);

            Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(200);

            Property(p => p.Descricao)
                .IsRequired()
                .HasMaxLength(1000);

            Property(p => p.Imagem)
                .IsRequired()
                .HasMaxLength(100);

            //Relacionamento: precisa ter um fornecedor pra ter um produto
            HasRequired(p => p.Fornecedor)
                .WithMany(f => f.Produtos) //Um fornecedor tem muitos produtos.. WithManyPrincipal é para apenas 1.
                .HasForeignKey(f => f.FornecedorId); //Chave estrangeira do FornecedorId

            ToTable("Produtos");
        }
    }
}
