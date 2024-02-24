using Bogus;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using Usuarios.App.Services.Models;
using Xunit;

namespace UsuariosApp.Tests
{
    public class UsuariosTest
    {
        [Fact]
        public async Task<CriarUsuarioResponseModel> CriarUsuarioComSucesso()
        {
            var faker = new Faker();
            var request = new CriarUsuarioRequestModel
            {
                Nome = faker.Person.FullName,
                Email = faker.Internet.Email(),
                Senha = "@Teste1234",
                SenhaConfirmacao = "@Teste1234"
            };

            //Serializando os dados em JSON
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            //Enviando a requisição para a API (ENDPOINT)
            var client = new WebApplicationFactory<Program>().CreateClient();
            var result = await client.PostAsync("/api/Usuarios/Criar", content);

            //Verificando se a resposta foi sucesso
            result.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

            //deserializar os dados obtidos do teste
            var response = JsonConvert.DeserializeObject<CriarUsuarioResponseModel>(result.Content.ReadAsStringAsync().Result);

            //Verificando os dados obtidos pelo teste
            response?.Id.Should().NotBeNull();
            response?.Nome.Should().Be(request.Nome);
            response?.Email.Should().Be(request.Email);
            response?.DataHoraCadastro.Should().NotBeNull();

            return response;
        }

        [Fact]
        public async void EmailDeveSerUnicoParaCadaUsuario()
        {
            // Criando um usuário na API
            var usuario = await CriarUsuarioComSucesso();

            var request = new CriarUsuarioRequestModel
            {
                Nome = usuario.Nome,
                Email = usuario.Email,
                Senha = "@Teste1234",
                SenhaConfirmacao = "@Teste1234"
            };

            //Serializando os dados em JSON
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            //Enviando a requisição para a API (ENDPOINT)
            var client = new WebApplicationFactory<Program>().CreateClient();
            var result = await client.PostAsync("/api/Usuarios/Criar", content);

            //Verificando se a resposta foi sucesso
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            //Verificando a mensagem obtida
            result.Content.ReadAsStringAsync().Result.Should().Contain("O email informado já está cadastrado");
        }

        [Fact]
        public async void AutenticarUsuarioComSucesso()
        {
            // Criando um usuário na API
            var usuario = await CriarUsuarioComSucesso();

            //Enviando os dados de autenticação
            var request = new AutenticarUsuarioRequestModel
            {
                Email = usuario.Email,
                Senha = "@Teste1234"
            };

            //Serializando os dados JSON
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            //Enviando a requisição para a API (ENDPOINT)
            var client = new WebApplicationFactory<Program>().CreateClient();
            var result = await client.PostAsync("/api/Usuarios/Autenticar", content);

            //Verificando se a resposta foi sucesso
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            //deserializar os dados obtidos do teste
            var response = JsonConvert.DeserializeObject<AutenticarUsuarioResponseModel>
                (result.Content.ReadAsStringAsync().Result);

            //verificando os dados obtidos pelo teste
            response?.Id.Should().Be(usuario.Id);
            response?.Nome.Should().Be(usuario.Nome);
            response?.Email.Should().Be(usuario.Email);
            response?.DataHoraAcesso.Should().NotBeNull();
            response?.DataHoraExpiracao.Should().NotBeNull();
            response?.AccessToken.Should().NotBeNull();
        }

        [Fact]
        public async void AcessoNegadoDoUsuario()
        {
            var faker = new Faker();
            //Enviando os dados de autenticação
            var request = new AutenticarUsuarioRequestModel
            {
                Email = faker.Internet.Email(),
                Senha = "@Teste1234"
            };

            //Serializando os dados JSON
            var content = new StringContent
               (JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            //enviando a requisição para a API (ENDPOINT)
            var client = new WebApplicationFactory<Program>().CreateClient();
            var result = await client.PostAsync("/api/Usuarios/Autenticar", content);

            //verificando se a resposta foi acesso negado
            result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

            result.Content.ReadAsStringAsync().Result
                .Should().Contain("Usuário não encontrado.");
        }
    }
}