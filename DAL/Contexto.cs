using Microsoft.EntityFrameworkCore;
using RegistroLogin.BLL;
using RegistroLogin.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroLogin.DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Usuario> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"data source = data\usuariocontrol.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario { UsuarioId = 1, Alias = "Joselito", Nombres = "Para usuarios", Email = "josemanuel@gmail.com", Activo = true , Clave= UsuariosBLL.SHA1("1234") }
                );
        }
    }
}