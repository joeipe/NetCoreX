using System.ComponentModel.DataAnnotations;

namespace NetCoreX.ViewModel
{
    public class ContactVM
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public required string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public required string LastName { get; set; }

        [Required]
        [RegularExpression(@"^(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[0-2])/\d{4}$", ErrorMessage = "Date must be in dd/MM/yyyy format")]
        public required string DoB { get; set; }
    }
}
