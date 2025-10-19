using MediatR;
using Microsoft.AspNetCore.Mvc;
using Workout.Api.Models;
using Workout.Application.Exercises.Commands.CreateExercise;
using Workout.Application.Exercises.Queries.GetExercises;

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
    public async Task<IActionResult> Create([FromBody] ExerciseModel model)
    {
        var command = new CreateExerciseCommand(
            new CreateExerciseDto
            {
                Name = model.Name,
                Description = model.Description,
                PrimaryMuscleGroup = model.PrimaryMuscleGroup,
                Equipment = model.Equipment,
                MediaUrls = model.MediaUrls
            });

        var exerciseId = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id = exerciseId }, null);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetExercisesQuery();
        var exercises = await _mediator.Send(query);
        return Ok(GetExercisesResponse.FromDomain(exercises));
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        // jen placeholder
        return Ok();
    }
}