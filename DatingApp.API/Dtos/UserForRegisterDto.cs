using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "A senha informada deve ter entre 6 e 20 caracteres")]
        public string Password { get; set; }
    }
}
