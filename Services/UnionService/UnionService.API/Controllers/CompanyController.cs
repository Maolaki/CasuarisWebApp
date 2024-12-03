﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using UnionService.Application.UseCases;
using Microsoft.AspNetCore.Authorization;
using UnionService.API.Filters;

namespace UnionService.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CompanyController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost("add")]
        [Authorize, ServiceFilter(typeof(EnsureAuthenticatedUserFilter))]
        public async Task<IActionResult> AddCompany([FromBody] AddCompanyCommand command)
        {
            await _mediator.Send(command);
            return Ok("Company added successfully.");
        }

        [HttpPut("update")]
        [Authorize, ServiceFilter(typeof(EnsureAuthenticatedUserFilter))]
        public async Task<IActionResult> UpdateCompany([FromBody] UpdateCompanyCommand command)
        {
            if (User.Identity!.Name != command.username)
                return BadRequest("User is not authenticated.");

            await _mediator.Send(command);
            return Ok("Company updated successfully.");
        }

        [HttpDelete("remove")]
        [Authorize, ServiceFilter(typeof(EnsureAuthenticatedUserFilter))]
        public async Task<IActionResult> RemoveCompany([FromBody] RemoveCompanyCommand command)
        {
            if (User.Identity!.Name != command.username)
                return BadRequest("User is not authenticated.");

            await _mediator.Send(command);
            return Ok("Company removed successfully.");
        }

        [HttpPost("add-worker")]
        [Authorize, ServiceFilter(typeof(EnsureAuthenticatedUserFilter))]
        public async Task<IActionResult> AddCompanyWorker([FromBody] AddCompanyWorkerCommand command)
        {
            if (User.Identity!.Name != command.username)
                return BadRequest("User is not authenticated.");

            await _mediator.Send(command);
            return Ok("Worker added to company successfully.");
        }

        [HttpPost("add-datetime-checker")]
        [Authorize, ServiceFilter(typeof(EnsureAuthenticatedUserFilter))]
        public async Task<IActionResult> AddCompanyDateTimeChecker([FromBody] AddCompanyDateTimeCheckerCommand command)
        {
            if (User.Identity!.Name != command.username)
                return BadRequest("User is not authenticated.");

            await _mediator.Send(command);
            return Ok("DateTime checker added to company successfully.");
        }

        [HttpDelete("remove-datetime-checker")]
        [Authorize, ServiceFilter(typeof(EnsureAuthenticatedUserFilter))]
        public async Task<IActionResult> RemoveCompanyDateTimeChecker([FromBody] RemoveCompanyDateTimeCheckerCommand command)
        {
            if (User.Identity!.Name != command.username)
                return BadRequest("User is not authenticated.");

            await _mediator.Send(command);
            return Ok("DateTime checker removed successfully.");
        }

        [HttpGet("get-companies")]
        [Authorize, ServiceFilter(typeof(EnsureAuthenticatedUserFilter))]
        public async Task<IActionResult> GetCompanies([FromBody] GetCompaniesQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}