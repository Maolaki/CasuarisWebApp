using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using TaskService.Application.UseCases;
using Microsoft.AspNetCore.Authorization;
using TaskService.API.Filters;
using TaskService.Application.DTOs;

namespace TaskService.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public TaskController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("add")]
        [Authorize, ServiceFilter(typeof(EnsureAuthenticatedUserFilter))]
        public async Task<ActionResult> AddTask([FromBody] AddTaskCommand command)
        {
            if (User.Identity!.Name != command.username)
                return BadRequest("User is not authenticated.");

            await _mediator.Send(command);
            return Ok();
        }

        [HttpPut("update")]
        [Authorize, ServiceFilter(typeof(EnsureAuthenticatedUserFilter))]
        public async Task<ActionResult> UpdateTask([FromBody] UpdateTaskCommand command)
        {
            if (User.Identity!.Name != command.username)
                return BadRequest("User is not authenticated.");

            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete("remove")]
        [Authorize, ServiceFilter(typeof(EnsureAuthenticatedUserFilter))]
        public async Task<ActionResult> RemoveTask([FromBody] RemoveTaskCommand command)
        {
            if (User.Identity!.Name != command.username)
                return BadRequest("User is not authenticated.");

            await _mediator.Send(command);
            return Ok();
        }

        [HttpGet("all")]
        [Authorize, ServiceFilter(typeof(EnsureAuthenticatedUserFilter))]
        public async Task<ActionResult<IEnumerable<TaskInfoDTO>>> GetAllTasksInfo([FromQuery] GetAllTasksInfoQuery query)
        {
            if (User.Identity!.Name != query.username)
                return BadRequest("User is not authenticated.");

            var tasks = await _mediator.Send(query);
            return Ok(tasks);
        }

        [HttpGet("data")]
        [Authorize, ServiceFilter(typeof(EnsureAuthenticatedUserFilter))]
        public async Task<ActionResult<TaskDataDTO>> GetTaskData([FromQuery] GetTaskDataQuery query)
        {
            if (User.Identity!.Name != query.username)
                return BadRequest("User is not authenticated.");

            var taskData = await _mediator.Send(query);
            return Ok(taskData);
        }
    }
}
