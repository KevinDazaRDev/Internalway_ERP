using System.ComponentModel.DataAnnotations;

namespace Amway.Application.Dtos
{
    public class ClientUpdateDto
    {
        [Required]
        public long Id { get; set; }

        [Required]
        [StringLength(150)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(150)]
        public string LastName { get; set; } = null!;

        [Required]
        [EmailAddress]
        [StringLength(200)]
        public string Email { get; set; } = null!;

        [StringLength(50)]
        public string? Phone { get; set; }

        [StringLength(50)]
        public string? DocumentType { get; set; }

        [StringLength(50)]
        public string? DocumentNumber { get; set; }
    }
}
