using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrilhaApiDesafio.Data;
using TrilhaApiDesafio.Models;
using Microsoft.Extensions.Configuration;


namespace TrilhaApiDesafio.Controllers
    {
       
        [Route("api/[controller]")] // Specify a route for the controller
        [ApiController]
        public class TarefaController : ControllerBase

        {
        private readonly IConfiguration _configuration;

        public TarefaController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private readonly OrganizadorContext _context;

            public TarefaController(OrganizadorContext context)
            {
                _context = context;
            }

            [HttpGet("{id}")]
            public IActionResult ObterPorId(int id)
            {
                // TODO: Retrieve the task by its ID using Entity Framework
                var tarefa = _context.Tarefas.FirstOrDefault(t => t.Id == id);

                if (tarefa == null)
                {
                    return NotFound(); // Task not found
                }

                return Ok(tarefa); // Task found, return it with OK status
            }

            [HttpGet("ObterTodos")]
            public IActionResult ObterTodos()
            {
                // TODO: Retrieve all tasks from the database using Entity Framework
                var tarefas = _context.Tarefas.ToList();

                return Ok(tarefas);
            }

            [HttpGet("ObterPorTitulo")]
            public IActionResult ObterPorTitulo(string titulo)
            {
                // TODO: Retrieve tasks with the specified title using Entity Framework
                var tarefas = _context.Tarefas.Where(t => t.Titulo.Contains(titulo)).ToList();

                return Ok(tarefas);
            }

            [HttpGet("ObterPorData")]
            public IActionResult ObterPorData(DateTime data)
            {
                var tarefas = _context.Tarefas.Where(x => x.Data.Date == data.Date).ToList();
                return Ok(tarefas);
            }

            [HttpGet("ObterPorStatus")]
            public IActionResult ObterPorStatus(EnumStatusTarefa status)
            {
                // TODO: Retrieve tasks with the specified status using Entity Framework
                var tarefas = _context.Tarefas.Where(x => x.Status == status).ToList();

                return Ok(tarefas);
            }

            [HttpPost]
            public IActionResult Criar(Tarefa tarefa)
            {
                if (tarefa.Data == DateTime.MinValue)
                    return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

                // TODO: Add the received task to Entity Framework and save the changes
                _context.Tarefas.Add(tarefa);
                _context.SaveChanges();

                return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
            }

            [HttpPut("{id}")]
            public IActionResult Atualizar(int id, Tarefa tarefa)
            {
                var tarefaBanco = _context.Tarefas.Find(id);

                if (tarefaBanco == null)
                    return NotFound(); // Task not found

                if (tarefa.Data == DateTime.MinValue)
                    return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

                // TODO: Update the information of the 'tarefaBanco' variable with the task received as a parameter
                tarefaBanco.Titulo = tarefa.Titulo;
                tarefaBanco.Descricao = tarefa.Descricao;
                tarefaBanco.Data = tarefa.Data;
                tarefaBanco.Status = tarefa.Status;

                // TODO: Update the 'tarefaBanco' in Entity Framework and save the changes
                _context.Entry(tarefaBanco).State = EntityState.Modified;
                _context.SaveChanges();

                return Ok();
            }

            [HttpDelete("{id}")]
            public IActionResult Deletar(int id)
            {
                var tarefaBanco = _context.Tarefas.Find(id);

                if (tarefaBanco == null)
                    return NotFound(); // Task not found

                // TODO: Remove the task found using Entity Framework and save the changes
                _context.Tarefas.Remove(tarefaBanco);
                _context.SaveChanges();

                return NoContent();
            }
        }
    }

