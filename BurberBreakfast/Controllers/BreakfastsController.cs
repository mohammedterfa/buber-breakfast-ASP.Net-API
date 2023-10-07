using Microsoft.AspNetCore.Mvc;
using BurberBreakfast.Contracts.Breakfast;
using BurberBreakfast.Models;
using BurberBreakfast.Services.Breakfasts;
using ErrorOr;
using BurberBreakfast.ServicesErrors;

namespace BurberBreakfast.Controllers;
[ApiController]
[Route("[controller]")]
public class BreakfastsController : ControllerBase
{
    private readonly IBreakfastService _breakfastService;

    public BreakfastsController(IBreakfastService breakfastService)
    {
        _breakfastService = breakfastService;
    }

    [HttpPost()]
    public IActionResult CreateBreakfast(CreateBreakfastRequest request)
    {
        var breakfast = new Breakfast(
            Guid.NewGuid(), 
            request.Name, 
            request.Description, 
            request.StartDateTime, 
            request.EndDateTime, 
            DateTime.UtcNow, 
            request.Savory, 
            request.Sweet);
        //ToDO: Save to DB
        _breakfastService.CreateBreakfast(breakfast);

        var response = new BreakfastResponse(
            breakfast.Id,
            breakfast.Name,
            breakfast.Description,
            breakfast.StartDateTime,
            breakfast.EndDateTime,
            breakfast.LastModifiedDateTime,
            breakfast.Savory,
            breakfast.Sweet
        );
        return CreatedAtAction(
            nameof(GetBreakfast),
            new {id = breakfast.Id},
            response);
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetBreakfast(Guid id)
    {
        ErrorOr<Breakfast> getBreakfastResult = _breakfastService.GetBreakfast(id);

        if(getBreakfastResult.IsError &&
            getBreakfastResult.FirstError == Errors.Breakfast.NotFound){
            return NotFound();
        }
        var breakfast = getBreakfastResult.Value;

        var response = new BreakfastResponse(
            breakfast.Id,
            breakfast.Name,
            breakfast.Description,
            breakfast.StartDateTime,
            breakfast.EndDateTime,
            breakfast.LastModifiedDateTime,
            breakfast.Savory,
            breakfast.Sweet
        );
        return Ok(response);
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpsertBreakfast(Guid id, UpsertBreakfastRequest request)
    {
        var breakfast = new Breakfast(
            id,
            request.Name,
            request.Description,
            request.StartDateTime,
            request.EndDateTime,
            DateTime.UtcNow,
            request.Savory,
            request.Sweet
        );

        _breakfastService.UpsertBreakfast(breakfast);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteBreakfast(Guid id)
    {

        _breakfastService.DeleteBreakfast(id);
        return NoContent();
    }
}   