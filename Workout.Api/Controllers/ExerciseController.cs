using MediatR;
using Microsoft.AspNetCore.Mvc;
using Workout.Api.Models;
using Workout.Application.Exercises.Commands.CreateExercise;

namespace Workout.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExercisesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ExercisesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateExerciseRequest request)
    {
        // ModelState zkontroluje atributy Required, MaxLength atd.
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var command = new CreateExerciseCommand(
            new CreateExerciseDto
            {
                Name = request.Name,
                Description = request.Description,
                PrimaryMuscleGroup = request.PrimaryMuscleGroup,
                Equipment = request.Equipment,
                MediaUrls = request.MediaUrls
            });

        var exerciseId = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id = exerciseId }, null);
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        // jen placeholder
        return Ok();
    }
}