using System.ComponentModel.DataAnnotations;

namespace PS_EMS.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public required string FirstName { get; set; }

        [Required]
        [StringLength(30)]
        public required string LastName { get; set; }

        [Required]
        [StringLength(50)]
        public required string Email { get; set; }

        [StringLength(12)]
        public string? PhoneNumber { get; set; }
        
        [Required]
        public required Gender Gender { get; set; }

        [Required]
        public required Department Department { get; set; }

        [Required]
        public required DateTime DateOfBirth { get; set; }
    }
}
