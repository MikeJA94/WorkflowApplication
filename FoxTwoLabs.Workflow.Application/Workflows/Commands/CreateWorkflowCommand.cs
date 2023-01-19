using AutoMapper;
using FoxTwoLabs.Workflow.Application.Models;
using FoxTwoLabs.Workflow.Infrastructure.Data;
using Domain = FoxTwoLabs.Workflow.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace FoxTwoLabs.Workflow.Application.Workflows.Commands
{
    // Command
    public class CreateWorkflowCommand : IRequest<WorkflowModel>
    {
        public WorkflowModel Workflow { get; set; }
    }


    // Command Handler
    public class CreateWorkflowCommandHandler : IRequestHandler<CreateWorkflowCommand, WorkflowModel>
    {
        private readonly WorkflowDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateWorkflowCommandHandler(WorkflowDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<WorkflowModel> Handle(CreateWorkflowCommand request, CancellationToken cancellationToken)
        {
            var finalWorkflow = _mapper.Map<Domain.Models.Workflow>(request.Workflow);

            await _dbContext.Workflows.AddAsync(finalWorkflow);
            
            await _dbContext.SaveChangesAsync(1, cancellationToken);

            var workflow = _mapper.Map<WorkflowModel>(finalWorkflow);

            return workflow;
        }
    }
}
