using CabaVS.Shared.AspNetCore.API.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mime;

namespace CabaVS.IdentityMS.API.Controllers.Abstractions
{
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ErrorDto), (int)HttpStatusCode.InternalServerError)]
    public abstract class SwaggerMetadataControllerBase : ControllerBase
    {
    }
}