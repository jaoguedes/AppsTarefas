using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AppTarefas.Models
{
    public class Status
    {
        
        public Guid StatusId { get; set; }

        [DisplayName("Nome")]
        [Required(ErrorMessage ="Este campo é obrigatorio!")]
        public string StatusNome { get; set; }
      
        public IEnumerable<Tarefa>? Tarefas { get; set; }
    }
}
