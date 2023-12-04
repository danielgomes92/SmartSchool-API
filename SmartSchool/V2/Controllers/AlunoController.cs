using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.Data;
using SmartSchool.Models;
using SmartSchool.V2.Dtos;
using System.Collections.Generic;

namespace SmartSchool.V2.Controllers
{
	/// <summary>
	/// Versão 2 do meu controlador de Alunos
	/// </summary>
    [ApiController]
	[ApiVersion("2.0")]
	[Route("api/v{version:apiVersion}/[controller]")]
	public class AlunoController : ControllerBase
	{
		private readonly IRepository _repo;
		private readonly IMapper _mapper;

		public AlunoController(IRepository repo, IMapper mapper)
		{
			_mapper = mapper;
			_repo = repo;
		}

		/// <summary>
		/// Método responsável por retornar todos os alunos
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public IActionResult GetAlunos()
		{
			var alunos = _repo.GetAllAlunos(true);
			
			return Ok(_mapper.Map<IEnumerable<AlunoDto>>(alunos));
		}

		/// <summary>
		/// Método responsável por retornar apenas um Aluno por meio de ID
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet("{id}")]
		public IActionResult GetById(int id)
		{
			var aluno = _repo.GetAlunoById(id, false);

			if (aluno == null) return BadRequest("O Aluno não foi encontrado");

			var alunoDto = _mapper.Map<AlunoDto>(aluno);

			return Ok(alunoDto);
		}

		/// <summary>
		/// Método responsável por Registrar um Aluno
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		public IActionResult Post(AlunoRegistrarDto model)
		{
			var aluno = _mapper.Map<Aluno>(model);

			_repo.Add(aluno);
			if (_repo.SaveChanges())
				return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));

			return BadRequest("Aluno não cadastrado");
		}

		/// <summary>
		/// Método responsável por atualizar as informações de um Aluno
		/// </summary>
		/// <param name="id"></param>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPut("{id}")]
		public IActionResult Put(int id, AlunoRegistrarDto model)
		{
			var aluno = _repo.GetAlunoById(id);
			if (aluno == null)
				return BadRequest("Aluno não encontrado!");

			_mapper.Map(model, aluno);

			_repo.Update(aluno);
			if (_repo.SaveChanges())
				return Created($"/api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));

			return BadRequest("Aluno não atualizado");
		}

		/// <summary>
		/// Método responsável por deletar um Aluno específico
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
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