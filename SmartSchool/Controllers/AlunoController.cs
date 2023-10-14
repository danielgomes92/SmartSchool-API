using Microsoft.AspNetCore.Mvc;
using SmartSchool.Models;
using System.Collections.Generic;
using System.Linq;

namespace SmartSchool.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AlunoController : ControllerBase
	{
		public List<Aluno> Alunos = new List<Aluno>()
		{
			new Aluno()
			{
				Id = 1,
				Name = "Daniel",
				Sobrenome = "Oliveira",
				Telefone = "912341234",
				AlunoDisciplinas = null
			},
			new Aluno()
			{
				Id = 2,
				Name = "Marion",
				Sobrenome = "Defante",
				Telefone = "988884444",
				AlunoDisciplinas = null
			},
			new Aluno()
			{
				Id = 3,
				Name = "Bartolomew",
				Sobrenome = "Kuma",
				Telefone = "932236446",
				AlunoDisciplinas = null
			}
		};

		[HttpGet]
		public IActionResult GetAlunos()
		{
			return Ok(Alunos);
		}

		[HttpGet("{id:int}")]
		public IActionResult GetById(int id)
		{
			var aluno = Alunos.FirstOrDefault(a => a.Id == id);

			if (aluno == null) return BadRequest("O Aluno não foi encontrado");

			return Ok(aluno);
		}

		[HttpGet("byName")]
		public IActionResult GetByName(string nome, string sobrenome)
		{
			var aluno = Alunos.FirstOrDefault(a =>
			a.Name.ToLower().Contains(nome.ToLower()) && a.Sobrenome.ToLower().Contains(sobrenome.ToLower()));

			if (aluno == null) return BadRequest("O Aluno não foi encontrado");

			return Ok(aluno);
		}
	}
}
