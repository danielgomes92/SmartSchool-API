using System.Collections.Generic;

namespace SmartSchool.Models
{
	public class Aluno
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Sobrenome { get; set; }
		public string Telefone { get; set; }
		public IEnumerable<AlunoDisciplina> AlunoDisciplinas { get; set; }
	}
}
