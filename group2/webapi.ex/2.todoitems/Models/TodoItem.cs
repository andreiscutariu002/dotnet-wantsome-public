namespace FirstApi.Models
{
    using System.ComponentModel.DataAnnotations;

    public class TodoItem
    {
        public long Id { get; set; }

        [Required]
        [StringLength(10)]
        public string Name { get; set; }

        public bool IsComplete { get; set; }
    }
}
