using System.ComponentModel.DataAnnotations;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.DTOs
{
    public class UpdateTaskDto
    {
        [Required(ErrorMessage = "O título é obrigatório.")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "O título deve ter entre 3 e 200 caracteres.")]
        public required string Title { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "O status é obrigatório.")]
        [EnumDataType(typeof(TaskItemStatus), ErrorMessage = "Status inválido. Use 'Pendente', 'EmAndamento' ou 'Concluido'.")]
        public TaskItemStatus Status { get; set; }

        [Required(ErrorMessage = "A data de vencimento é obrigatória.")]
        public DateTime DueDate { get; set; }
    }
}