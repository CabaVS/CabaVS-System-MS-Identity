using CabaVS.IdentityMS.API.Configuration.Constants;
using CabaVS.IdentityMS.API.Controllers.Abstractions;
using CabaVS.IdentityMS.API.DTO;
using CabaVS.IdentityMS.API.Services.Abstractions;
using CabaVS.IdentityMS.Core.Services.Abstractions;
using CabaVS.Shared.AspNetCore.API.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace CabaVS.IdentityMS.API.Controllers
{
    [ApiController]
    [Route("api/token")]
    public class TokenController : SwaggerMetadataControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenGenerator _tokenGenerator;

        public TokenController(IUserService userService, ITokenGenerator tokenGenerator)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _tokenGenerator = tokenGenerator ?? throw new ArgumentNullException(nameof(tokenGenerator));
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(TokenDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDto), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateToken([FromBody] TokenCreateRequestDto tokenCreateRequestDto)
        {
            if (tokenCreateRequestDto == null) return BadRequest(ErrorDto.FromMessage(ErrorMessages.EmptyDtoReceived));

            var (username, password) = tokenCreateRequestDto;
            var user = await _userService.GetUser(username, password);

            if (user == null)
            {
                return BadRequest(ErrorDto.FromMessage(ErrorMessages.InvalidCredentials));
            }

            var tokens = _tokenGenerator.Generate(user);

            var tokenDto = new TokenDto(user, tokens);
            return Ok(tokenDto);
        }

        [HttpPost("validate")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(ErrorDto), (int)HttpStatusCode.BadRequest)]
        public IActionResult ValidateToken([FromBody] TokenValidationRequestDto tokenValidationRequestDto)
        {
            if (tokenValidationRequestDto == null) return BadRequest(ErrorDto.FromMessage(ErrorMessages.EmptyDtoReceived));

            var tokenToValidate = tokenValidationRequestDto.AccessToken;
            if (string.IsNullOrWhiteSpace(tokenToValidate))
            {
                return BadRequest(ErrorDto.FromMessage(ErrorMessages.AccessTokenInvalid));
            }

            var isValid = _tokenGenerator.IsValid(tokenToValidate);
            if (!isValid)
            {
                return BadRequest(ErrorDto.FromMessage(ErrorMessages.AccessTokenInvalid));
            }

            return NoContent();
        }
    }
}