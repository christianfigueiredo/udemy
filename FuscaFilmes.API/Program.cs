using FuscaFilmes.API.DbContexts;
using FuscaFilmes.API.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

using (var contexto = new Contexto())
{
    contexto.Database.EnsureCreated();
}

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.AllowTrailingCommas = true;
    options.SerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/diretores", () =>
{
    using var contexto = new Contexto();
    var diretores = contexto.Diretores.Include(d => d.Filmes).ToList();
    return diretores;
})

.WithOpenApi();

app.MapPost("/diretores", (Diretor diretor) =>
{
    using var contexto = new Contexto();
    contexto.Diretores.Add(diretor);
    contexto.SaveChanges();   
})
.WithOpenApi();

app.MapPut("/diretores/{id}", (int Diretorid, Diretor diretorNovo) =>
{
    using var contexto = new Contexto();
    var diretorDb = contexto.Diretores.Find(Diretorid);
    if (diretorDb != null){
        diretorDb.Nome = diretorNovo.Nome;
        if(diretorNovo.Filmes.Count > 0)
    {
        diretorDb.Filmes.Clear();
        foreach (var filme in diretorNovo.Filmes)
        {
            diretorDb.Filmes.Add(filme);
        }
    }
    }            
    contexto.SaveChanges(); 
   
})
.WithOpenApi();

app.MapDelete("/diretores/{id}", (int id) =>
{
    using var contexto = new Contexto();
    var diretor = contexto.Diretores.Find(id);
    if (diretor != null)
        contexto.Diretores.Remove(diretor);
    contexto.SaveChanges();
})
.WithOpenApi();


app.Run();


