using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AppTarefas.Models
{
    public class Tarefa
    {
        public Guid TarefaId { get; set; }
        [DisplayName("Nome da Tarefa")]
        [Required(ErrorMessage = "Este campo é obrigatorio!")]
        public string TarefaNome { get; set; }
        [DisplayName("Data de Inicio")]
        [Required(ErrorMessage = "Este campo é obrigatorio!")]
        public DateTime DataInicio { get; set; } = DateTime.Now;
        [DisplayName("Data de Fim")]
        
        public DateTime? DataFim { get; set; }
        public Guid CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }
        public Guid? StatusId { get; set; }
        public Status? Status { get; set; }

    }
}
