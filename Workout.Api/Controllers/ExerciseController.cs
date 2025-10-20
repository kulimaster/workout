using MediatR;
using Microsoft.AspNetCore.Mvc;
using Workout.Api.Models;
using Workout.Application.Exercises.Commands.CreateExercise;
using Workout.Application.Exercises.Commands.DeleteExercise;
using Workout.Application.Exercises.Commands.UpdateExercise;
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
          new ExerciseDto( 
                model.Name,
                model.Description,
                model.PrimaryMuscleGroup,
                model.Equipment,
                model.MediaUrls
            )
        );

        var exerciseId = await _mediator.Send(command);
        return Ok(exerciseId);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetExercisesQuery();
        var exercises = await _mediator.Send(query);
        return Ok(GetExercisesResponse.FromDomain(exercises));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteExerciseCommand(id));
        return NoContent();
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] ExerciseModel model)
    {
        var command = new UpdateExerciseCommand(
            id,
            new ExerciseDto(
                model.Name,
                model.Description,
                model.PrimaryMuscleGroup,
                model.Equipment,
                model.MediaUrls
            )
        );
        await _mediator.Send(command);
        return Ok();
    }
}