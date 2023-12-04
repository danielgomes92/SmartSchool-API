using System;

namespace SmartSchool.V1.Dtos
{
    public class AlunoDto
    {
		/// <summary>
		/// Identificação do Aluno
		/// </summary>
		public int Id { get; set; }
		/// <summary>
		/// Número de registro do Aluno
		/// </summary>
		public int Matricula { get; set; }
		/// <summary>
		/// Nome Completo do Aluno
		/// </summary>
		public string Nome { get; set; }
		/// <summary>
		/// Telefone do Aluno
		/// </summary>
		public string Telefone { get; set; }
		/// <summary>
		/// Idade do Aluno
		/// </summary>
		public int Idade { get; set; }
		/// <summary>
		/// Início do Período
		/// </summary>
		public DateTime DataInicio { get; set; }
		/// <summary>
		/// Se o Aluno se encontra ativo / inativo
		/// </summary>
		public bool Ativo { get; set; }
	}
}