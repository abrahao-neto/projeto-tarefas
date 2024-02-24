using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TarefasApp.Domain.Entities;
using TarefasApp.Domain.Interfaces.Services;
using TarefasApp.Services.Models.Categorias;
using TarefasApp.Services.Models.Tarefas;

namespace TarefasApp.Services.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TarefasController : ControllerBase
    {
        private readonly ITarefaDomainService? _tarefaDomainService;
        private readonly ICategoriaDomainService? _categoriaDomainService;

        public TarefasController(ITarefaDomainService? tarefaDomainService, ICategoriaDomainService? categoriaDomainService)
        {
            _tarefaDomainService = tarefaDomainService;
            _categoriaDomainService = categoriaDomainService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(TarefaGetModel), (int) HttpStatusCode.Created)]
        public IActionResult Post([FromBody] TarefaPostModel model)
        {
            var tarefa = new Tarefa
            {
                Id = Guid.NewGuid(),
                Nome = model.Nome,
                DataHora = model.DataHora,
                Descricao = model.Descricao,
                CategoriaId = model.CategoriaId
            };

            _tarefaDomainService?.Add(tarefa);

            var categoria = _categoriaDomainService?.GetById(model.CategoriaId.Value);
            var result = CopyToModel(tarefa, categoria);            
            return StatusCode(201, result);
        }

        [HttpPut]
        [ProducesResponseType(typeof(TarefaGetModel), (int) HttpStatusCode.OK)]
        public IActionResult Put([FromBody] TarefaPutModel model)
        {
            var tarefa = new Tarefa
            {
                Id = model.Id,
                Nome = model.Nome,
                DataHora = model.DataHora,
                Descricao = model.Descricao,
                CategoriaId = model.CategoriaId
            };

            _tarefaDomainService?.Update(tarefa);

            var categoria = _categoriaDomainService?.GetById(tarefa.CategoriaId.Value);
            var result = CopyToModel(tarefa, categoria);
            return StatusCode(200, result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(TarefaGetModel), (int) HttpStatusCode.OK)]
        public IActionResult Delete(Guid id) 
        {
            var tarefa = _tarefaDomainService?.GetById(id);
            _tarefaDomainService?.Delete(tarefa);

            var categoria = _categoriaDomainService?.GetById(tarefa.CategoriaId.Value);
            var result = CopyToModel(tarefa, categoria);
            return StatusCode(200, result);
        }

        [HttpGet("{dataMin}/{dataMax}")]
        [ProducesResponseType(typeof(List<TarefaGetModel>), (int) HttpStatusCode.OK)]
        public IActionResult Get(DateTime dataMin, DateTime dataMax)
        {
            var tarefas = _tarefaDomainService?.GetAll(dataMin, dataMax);
            
            var model = new List<TarefaGetModel>();
            foreach (var item in tarefas)
            {
                model.Add(CopyToModel(item, item.Categoria));
            }

            return StatusCode(200, model);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TarefaGetModel), (int) HttpStatusCode.OK)]
        public IActionResult Get(Guid id)
        {
            var tarefa = _tarefaDomainService?.GetById(id);
            var categoria = _categoriaDomainService?.GetById(tarefa.CategoriaId.Value);

            var model = CopyToModel(tarefa, categoria);
            return StatusCode(200, model);
        }

        /// <summary>
        /// Método auxiliar para copiar os dados de tarefa e categoria para TarefaGetModel
        /// </summary>
        private TarefaGetModel CopyToModel(Tarefa tarefa, Categoria categoria)
        {
            var model = new TarefaGetModel
            {
                Id = tarefa.Id,
                Nome = tarefa.Nome,
                DataHora = tarefa.DataHora,
                Descricao = tarefa.Descricao,
                Categoria = new CategoriaGetModel
                {
                    Id = categoria.Id,
                    Nome = categoria.Nome,
                    Descricao = categoria.Descricao
                }
            };

            return model;
        }
    }
}
