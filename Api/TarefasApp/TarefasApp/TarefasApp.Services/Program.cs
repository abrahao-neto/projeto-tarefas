using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Services;
using System.Text;
using TarefasApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//registrando as injeções de dependência
DependencyInjection.Configure(builder.Services);

//configuração de autenticação com o JWT
builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("579e9d5-af1d-47f4-a6e2-d1a8db42a6b1"))
    };
});

//configuração da política de CORS do projeto
builder.Services.AddCors(cfg => cfg.AddPolicy("defaultPolicy", builder =>
{
    builder.WithOrigins("http://localhost:4200") //servidor que poderá acessar a API
           .AllowAnyMethod() //permissão para acessar os métodos POST, PUT, DELETE e GET
           .AllowAnyHeader(); //permissão para enviar parametros no cabeçalho
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//registrando a política de CORS
app.UseCors("defaultPolicy");

app.Run();



