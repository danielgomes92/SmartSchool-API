using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.Data;
using SmartSchool.Models;
using System.Linq;

namespace SmartSchool.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AlunoController : ControllerBase
	{
		private readonly SmartContext _context;

		public AlunoController(SmartContext context)
		{
			_context = context;
		}

		[HttpGet]
		public IActionResult GetAlunos()
		{
			return Ok(_context.Alunos);
		}

		[HttpGet("{id:int}")]
		public IActionResult GetById(int id)
		{
			var aluno = _context.Alunos.FirstOrDefault(a => a.Id == id);

			if (aluno == null) return BadRequest("O Aluno não foi encontrado");

			return Ok(aluno);
		}

		[HttpGet("byName")]
		public IActionResult GetByName(string nome, string sobrenome)
		{
			var aluno = _context.Alunos.FirstOrDefault(a =>
			a.Nome.ToLower().Contains(nome.ToLower()) && a.Sobrenome.ToLower().Contains(sobrenome.ToLower()));

			if (aluno == null) return BadRequest("O Aluno não foi encontrado");

			return Ok(aluno);
		}

		[HttpPost]
		public IActionResult Post(Aluno aluno)
		{
			_context.Add(aluno);
			_context.SaveChanges();
			return Ok(aluno);
		}

		[HttpPut("{id}")]
		public IActionResult Put(int id, Aluno aluno)
		{
			var aln = _context.Alunos.AsNoTracking().FirstOrDefault(a => a.Id == id);
			if (aln == null)
				return BadRequest("Aluno não encontrado!");

			_context.Update(aluno);
			_context.SaveChanges();
			return Ok(aluno);
		}

		[HttpPatch("{id}")]
		public IActionResult Patch(int id, Aluno aluno)
		{
			var aln = _context.Alunos.AsNoTracking().FirstOrDefault(a => a.Id == id);
			if (aln == null) 
				return BadRequest("Aluno não encontrado!");

			_context.Update(aluno);
			_context.SaveChanges();
			return Ok(aluno);
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var aluno = _context.Alunos.FirstOrDefault(a => a.Id == id);
			if (aluno == null)
				return BadRequest("Aluno não encontrado!");

			_context.Remove(aluno);
			_context.SaveChanges();
			return Ok("aluno removido do sistema.");
		}
	}
}
