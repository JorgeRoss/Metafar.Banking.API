using Asp.Versioning;
using MediatR;
using Metafar.Banking.API.Helpers;
using Metafar.Banking.Application.DTO.Entities;
using Metafar.Banking.Application.UseCases.Cards.Commands.CreateCardTokenCommand;
using Metafar.Banking.Cross.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Metafar.Banking.API.Controllers.v1
{
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class CardsController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly IMediator _mediator;

        public CardsController(IOptions<AppSettings> appSettings, IMediator mediator)
        {
            _appSettings = appSettings.Value;
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        [SwaggerOperation(
            Summary = "Authenticate by card number",
            Description = "This endpoint will return token",
            OperationId = "Authenticate",
            Tags = new string[] { "Create" })]
        [SwaggerResponse(200, "Successful", typeof(Response<bool>))]
        [SwaggerResponse(404, "Notfound")]
        public async Task<IActionResult> Authenticate([FromBody] CreateCardTokenCommand command)
        {
            var response = await _mediator.Send(command);
            if (response.IsSuccess)
            {
                if (response.Data != null)
                {
                    response.Data.Token = BuildToken(response);
                    return Ok(response);
                }
                else
                {
                    return NotFound(response);
                }                    
            }

            return BadRequest(response);
        }

        private string BuildToken(Response<CardTokenDto> cardTokenDto)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, cardTokenDto.Data.UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.Audience
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }
    }
}
