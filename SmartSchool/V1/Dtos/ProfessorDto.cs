using System;

namespace SmartSchool.V1.Dtos
{
	public class ProfessorDto
    {
		/// <summary>
		/// Identificação do Professor
		/// </summary>
		public int Id { get; set; }
		/// <summary>
		/// Número de registro do Professor
		/// </summary>
		public int Registro { get; set; }
		/// <summary>
		/// Nome do Professor
		/// </summary>
		public string Nome { get; set; }
		/// <summary>
		/// Sobrenome do Professor
		/// </summary>
		public string Sobrenome { get; set; }
		/// <summary>
		/// Telefone do Professor
		/// </summary>
		public string Telefone { get; set; }
		/// <summary>
		/// Início do Período
		/// </summary>
        public DateTime DataInicio { get; set; }
		/// <summary>
		/// Fim do período
		/// </summary>
        public DateTime? DataFim { get; set; }
		/// <summary>
		/// Se o professor se encontra ativo / inativo
		/// </summary>
        public bool Ativo { get; set; }
    }
}