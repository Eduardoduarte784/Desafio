using AgendaConsultorio.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgendaConsultorio.Infra.Data.Configuration
{
    public class ConsultaConfiguration : IEntityTypeConfiguration<Consulta>
    {
        public void Configure(EntityTypeBuilder<Consulta> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Paciente).IsRequired(true);
            builder.Property(x => x.DataNascimento).IsRequired(true);
            builder.Property(x => x.DataInicial).IsRequired(true);
            builder.Property(x => x.DataFinal).IsRequired(true);
            builder.Property(x => x.Observacoes);
        }
    }
}
