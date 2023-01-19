using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoxTwoLabs.Workflow.Application.Models;
using FoxTwoLabs.Workflow.Application.Workflows.Commands;
using FoxTwoLabs.Workflow.Application.Workflows.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MyWebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WorkflowController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly ILogger<WorkflowController> _logger;

        public WorkflowController(IMediator mediator, ILogger<WorkflowController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(WorkflowModel[]), 200)]
        public async  Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetWorkflowsQuery());

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] WorkflowModel workflowModel)
        {
            var result = await _mediator.Send(new CreateWorkflowCommand
            {
                Workflow = workflowModel
            });

            return Ok(result);
        }
    }
}
