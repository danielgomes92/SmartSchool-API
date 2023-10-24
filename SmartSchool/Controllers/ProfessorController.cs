using Microsoft.AspNetCore.Mvc;
using SmartSchool.Data;
using SmartSchool.Models;

namespace SmartSchool.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProfessorController : ControllerBase
	{
		private readonly IRepository _repo;
		public ProfessorController(IRepository repo)
		{
			_repo = repo;
		}

		[HttpGet]
		public IActionResult GetProfessores()
		{
			var result = _repo.GetAllProfessores(true);
			return Ok(result);
		}

		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			var professor = _repo.GetProfessorById(id, false);

			if (professor == null)
				return BadRequest("O Aluno não foi encontrado");

			return Ok(professor);
		}

		[HttpPost]
		public IActionResult Post(Professor professor)
		{
			_repo.Add(professor);
			if (_repo.SaveChanges())
				return Ok(professor);

			return BadRequest("Professor não cadastrado");
		}

		[HttpPut("{id}")]
		public IActionResult Put(int id, Professor professor)
		{
			var prof = _repo.GetProfessorById(id, false);
			if (prof == null)
				return BadRequest("Professor não encontrado");

			_repo.Update(professor);
			if (_repo.SaveChanges())
				return Ok(professor);

			return BadRequest("Professor não atualizado");
		}

		[HttpPatch("{id}")]
		public IActionResult Patch(int id, Professor professor)
		{
			var prof = _repo.GetProfessorById(id);
			if (prof == null)
				return BadRequest("Professor não encontrado");

			_repo.Update(professor);
			if (_repo.SaveChanges())
				return Ok(professor);

			return BadRequest("Professor não atualizado");
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var professor = _repo.GetProfessorById(id, false);
			if (professor == null)
				return BadRequest("Professor não encontrado");

			_repo.Delete(professor);
			if (_repo.SaveChanges())
				return Ok("Professor removido do sistema.");

			return BadRequest("Professor não deletado");
		}
	}
}
