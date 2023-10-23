using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartSchool.Data;
using SmartSchool.Models;
using System.Linq;

namespace SmartSchool.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProfessorController : ControllerBase
	{
		private readonly SmartContext _context;

		public ProfessorController(SmartContext context)
		{
			_context = context;
		}

		[HttpGet]
		public IActionResult Get()
		{
			return Ok(_context.Professores);
		}

		[HttpGet("byId/{id}")]
		public IActionResult GetById(int id)
		{
			var professor = _context.Professores.FirstOrDefault(a => a.Id == id);

			if (professor == null) 
				return BadRequest("O Aluno não foi encontrado");

			return Ok(professor);
		}

		[HttpGet("byName")]
		public IActionResult GetByName(string nome)
		{
			var professor = _context.Professores.FirstOrDefault(a =>
			a.Nome.ToLower().Contains(nome.ToLower()));

			if (professor == null) return BadRequest("O Aluno não foi encontrado");

			return Ok(professor);
		}

		[HttpPost]
		public IActionResult Post(Professor professor)
		{
			_context.Add(professor);
			_context.SaveChanges();
			return Ok(professor);
		}

		[HttpPut("{id}")]
		public IActionResult Put(int id, Professor professor)
		{
			var aln = _context.Professores.AsNoTracking().FirstOrDefault(a => a.Id == id);
			if (aln == null)
				return BadRequest("Professor não encontrado!");

			_context.Update(professor);
			_context.SaveChanges();
			return Ok(professor);
		}

		[HttpPatch("{id}")]
		public IActionResult Patch(int id, Professor professor)
		{
			var aln = _context.Professores.AsNoTracking().FirstOrDefault(a => a.Id == id);
			if (aln == null)
				return BadRequest("Professor não encontrado!");

			_context.Update(professor);
			_context.SaveChanges();
			return Ok(professor);
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var professor = _context.Professores.FirstOrDefault(a => a.Id == id);
			if (professor == null)
				return BadRequest("Professor não encontrado!");

			_context.Remove(professor);
			_context.SaveChanges();
			return Ok("professor removido do sistema.");
		}
	}
}
