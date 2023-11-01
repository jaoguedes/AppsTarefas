using System.ComponentModel.DataAnnotations;

namespace AppTarefas.Models
{
    public class Categoria
    {
        public Guid CategoriaId { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatorio!")]
        public string CategoriaNome { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatorio!")]
        public string CategoriaCor { get; set; }

        public IEnumerable<Tarefa>? Tarefas { get; set; }
    }
}
