using Backend.Common;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("/api/hello")]
public class HelloWorldController : ControllerBase
{
    [HttpGet]
    public ActionResult<ApiResponse<string>> Get()
    {
        return Ok(ApiResponse<string>.Ok("Hello world"));
    }

}
