using AgendaConsultorio.Domain.Entity;
using AgendaConsultorio.Infra.Data.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgendaConsultorio.Infra.Data.Context
{
    public class AgendaConsultorioContext : DbContext
    {
        public AgendaConsultorioContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Consulta> Consulta { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ConsultaConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
