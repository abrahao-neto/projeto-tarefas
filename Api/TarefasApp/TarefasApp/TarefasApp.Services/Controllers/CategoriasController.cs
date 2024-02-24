using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TarefasApp.Domain.Interfaces.Services;
using TarefasApp.Services.Models.Categorias;

namespace TarefasApp.Services.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaDomainService? _categoriaDomainService;

        public CategoriasController(ICategoriaDomainService? categoriaDomainService)
        {
            _categoriaDomainService = categoriaDomainService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<CategoriaGetModel>), (int) HttpStatusCode.OK)]
        public IActionResult Get()
        {
            //consultar todas as categorias do banco de dados
            var categorias = _categoriaDomainService?.GetAll();

            //crindo uma lista para retornar os dados
            var lista = new List<CategoriaGetModel>();
            foreach (var item in categorias)
            {
                //adicionando na lista
                lista.Add(new CategoriaGetModel
                {
                    Id = item.Id,
                    Nome = item.Nome,
                    Descricao = item.Descricao
                });
            }

            //retornando os dados
            return StatusCode(200, lista);
        }
    }
}
