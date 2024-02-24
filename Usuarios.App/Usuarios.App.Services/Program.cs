using UsuariosApp.Domain.Interfaces;
using UsuariosApp.Domain.Services;
using UsuariosApp.Infra.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//configura��o da pol�tica de CORS do projeto
builder.Services.AddCors(cfg => cfg.AddPolicy("defaultPolicy", builder =>
{
    builder.WithOrigins("http://localhost:4200") //servidor que poder� acessar a API
           .AllowAnyMethod() //permiss�o para acessar os m�todos POST, PUT, DELETE e GET
           .AllowAnyHeader(); //permiss�o para enviar parametros no cabe�alho
}));

builder.Services.AddTransient<IUsuarioDomainService, UsuarioDomainService>();
builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

//registrando a pol�tica de CORS
app.UseCors("defaultPolicy");

app.Run();

public partial class Program { }

