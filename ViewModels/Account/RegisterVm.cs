using System.ComponentModel.DataAnnotations;

namespace Lumia.ViewModels.Account
{
    public class RegisterVm
    {
        [Required]
        [MinLength(1)]
        [MaxLength(255)]
        public string Name { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(255)]
        public string Surname { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(255)]
        public string UserName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password),Compare(nameof(Password))]

        public string ComfirmPasword { get; set; }

    }
}
