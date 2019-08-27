using System.ComponentModel.DataAnnotations;

namespace hsl.api.Models
{
    public class RegistrationViewModel
    {
        [Required] [EmailAddress] public string Email { set; get; }
        [Required] public string DisplayName { set; get; }
        [Required] [DataType(DataType.Password)] public string Password { set; get; }
        public string Gender { set; get; }
    }
}