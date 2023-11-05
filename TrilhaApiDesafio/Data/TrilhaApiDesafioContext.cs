using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Data
{
    public class OrganizadorContext : DbContext
    {
        public OrganizadorContext(DbContextOptions<OrganizadorContext> options)  : base(options)
        {
        }

        public DbSet<TrilhaApiDesafio.Models.Tarefa> Tarefas { get; set; } = default!;
    }
}
