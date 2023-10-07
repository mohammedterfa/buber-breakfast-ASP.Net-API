using Microsoft.AspNetCore.Mvc;

namespace BurberBreakfast.Controllers;

public class ErrorsController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        return Problem();
    }
}