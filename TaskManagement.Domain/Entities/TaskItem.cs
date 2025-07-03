using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Domain.Entities
{
    public class TaskItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        public string? Description { get; set; }

        [Required]
        public TaskItemStatus Status { get; set; }

        public DateTime DueDate { get; set; }
    }
}