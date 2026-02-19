using FuscaFilmes.API.DbContexts;

var builder = WebApplication.CreateBuilder(args);

using (var contexto = new Contexto())
{
    contexto.Database.EnsureCreated();
}

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/diretor", () =>
{
    var contexto = new Contexto();
    var diretores = contexto.Diretores.ToList();
    return diretores;
})

.WithOpenApi();

app.Run();


