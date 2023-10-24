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
		private readonly IRepository _repo;

		public AlunoController(IRepository repo)
		{
			_repo = repo;
		}

		[HttpGet]
		public IActionResult GetAlunos()
		{
			var result = _repo.GetAllAlunos(true);
			return Ok(result);
		}

		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			var aluno = _repo.GetAlunoById(id, false);

			if (aluno == null) return BadRequest("O Aluno não foi encontrado");

			return Ok(aluno);
		}

		[HttpPost]
		public IActionResult Post(Aluno aluno)
		{
			_repo.Add(aluno);
			if (_repo.SaveChanges())
				return Ok(aluno);

			return BadRequest("Aluno não cadastrado");
		}

		[HttpPut("{id}")]
		public IActionResult Put(int id, Aluno aluno)
		{
			var aln = _repo.GetAlunoById(id);
			if (aln == null)
				return BadRequest("Aluno não encontrado!");

			_repo.Update(aluno);
			if (_repo.SaveChanges())
				return Ok(aluno);

			return BadRequest("Aluno não atualizado");
		}

		[HttpPatch("{id}")]
		public IActionResult Patch(int id, Aluno aluno)
		{
			var aln = _repo.GetAlunoById(id);
			if (aln == null) 
				return BadRequest("Aluno não encontrado!");

			_repo.Update(aluno);
			if (_repo.SaveChanges())
				return Ok(aluno);

			return BadRequest("Aluno não cadastrado");
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var aluno = _repo.GetAlunoById(id);
			if (aluno == null)
				return BadRequest("Aluno não encontrado!");

			_repo.Delete(aluno);
			if (_repo.SaveChanges())
				return Ok("aluno removido do sistema.");

			return BadRequest("Aluno não deletado");
		}
	}
}
