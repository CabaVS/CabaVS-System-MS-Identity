using System.ComponentModel.DataAnnotations;

namespace CabaVS.IdentityMS.API.DTO
{
    public class TokenValidationRequestDto
    {
        [Required]
        public string AccessToken { get; set; }
    }
}