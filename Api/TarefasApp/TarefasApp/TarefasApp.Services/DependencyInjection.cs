using Microsoft.EntityFrameworkCore;
using TarefasApp.Domain.Entities;
using TarefasApp.Domain.Interfaces.Repositories;
using TarefasApp.Domain.Interfaces.Services;
using TarefasApp.Domain.Services;
using TarefasApp.Infra.Data.Contexts;
using TarefasApp.Infra.Data.Repositories;

namespace TarefasApp.Services
{
    /// <summary>
    /// Classe para mapeamento das injeções de dependência do projeto
    /// </summary>
    public static class DependencyInjection
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddTransient<ICategoriaDomainService, CategoriaDomainService>();
            services.AddTransient<ITarefaDomainService, TarefaDomainService>();

            services.AddTransient<ICategoriaRepository, CategoriaRepository>();
            services.AddTransient<ITarefaRepository, TarefaRepository>();

            using (var dataContext = new DataContext())
            {
                if (dataContext.Set<Categoria>().FirstOrDefault(c => c.Nome.Equals("TRABALHO")) == null)
                    dataContext.Add(new Categoria { Id = Guid.NewGuid(), Nome = "TRABALHO", Descricao = "Tarefas de trabalho." });

                if (dataContext.Set<Categoria>().FirstOrDefault(c => c.Nome.Equals("FAMÍLIA")) == null)
                    dataContext.Add(new Categoria { Id = Guid.NewGuid(), Nome = "FAMÍLIA", Descricao = "Tarefas de família." });

                if (dataContext.Set<Categoria>().FirstOrDefault(c => c.Nome.Equals("AMIGOS")) == null)
                    dataContext.Add(new Categoria { Id = Guid.NewGuid(), Nome = "AMIGOS", Descricao = "Tarefas de amigos." });

                if (dataContext.Set<Categoria>().FirstOrDefault(c => c.Nome.Equals("LAZER")) == null)
                    dataContext.Add(new Categoria { Id = Guid.NewGuid(), Nome = "LAZER", Descricao = "Tarefas de lazer." });

                if (dataContext.Set<Categoria>().FirstOrDefault(c => c.Nome.Equals("OUTROS")) == null)
                    dataContext.Add(new Categoria { Id = Guid.NewGuid(), Nome = "OUTROS", Descricao = "Tarefas gerais." });

                dataContext.SaveChanges();
            }
        }
    }
}
