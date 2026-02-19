using FuscaFilmes.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace FuscaFilmes.API.DbContexts
{
    public class Contexto : DbContext
    {
        public  DbSet<Filme> Filmes { get; set; }
        public DbSet<Diretor> Diretores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        
            => options.UseSqlite("Data Source=FuscaFilmes.db");        
        
    }
}