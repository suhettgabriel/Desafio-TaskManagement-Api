using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Application.DTOs
{
    public class CreateTaskDto : IValidatableObject
    {
        [Required(ErrorMessage = "O título é obrigatório.")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "O título deve ter entre 3 e 200 caracteres.")]
        public required string Title { get; set; }

        public string? Description { get; set; }

        [Required(ErrorMessage = "A data de vencimento é obrigatória.")]
        public DateTime DueDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DueDate.Date <= DateTime.Now.Date)
            {

                yield return new ValidationResult(
                    "A data de vencimento deve ser uma data futura.",
                    new[] { nameof(DueDate) });
            }
        }
    }
}