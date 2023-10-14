using Microsoft.AspNetCore.Mvc;
using SmartSchool.Models;
using System.Collections.Generic;

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
	}
}
