using DevApp.Business.Models.Fornecedores;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevApp.Infra.Data.Mappings
{
    class FornecedorConfig : EntityTypeConfiguration<Fornecedor>
    {
        public FornecedorConfig()
        {//FLUENT API
            HasKey(f => f.Id); //Chave primária

            Property(f => f.Nome) //Não pode ser nulo no banco
                .IsRequired()
                .HasMaxLength(200);

            Property(f => f.Documento)
                .IsRequired()
                .HasMaxLength(14) //Indice no banco é para que faça o mapeamento de forma mais rápida, vai indexar os dados da coluna
                .HasColumnAnnotation("IX_Documento",
                   new IndexAnnotation(new IndexAttribute { IsUnique = true })); //Com isso não conseguimos cadastrar dois documentos iguais no banco.
                //.IsFixedLength();

            HasRequired(f => f.Endereco) //Requerindo que exista um endereço para o fornecedor, não se pode cadastrar um fornecedor sem endereço, caso não fosse obrigatório, poderiamos utilizar HasOptional.
                .WithRequiredPrincipal(e => e.Fornecedor); //Definindo que este endereço tem um fornecedor e que ele é o principal.

            ToTable("Fornecedores");
        }
    }
}
