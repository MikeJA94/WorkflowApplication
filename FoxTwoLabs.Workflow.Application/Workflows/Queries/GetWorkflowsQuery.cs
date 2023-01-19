using AutoMapper;
using FoxTwoLabs.Workflow.Application.Models;
using FoxTwoLabs.Workflow.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;

namespace FoxTwoLabs.Workflow.Application.Workflows.Queries
{

    public class GetWorkflowsQuery : IRequest<IEnumerable<WorkflowModel>>
    {
        public int Id { get; set; }

    }

    // Query Handler
    public class GetWorkflowsQueryHandler : IRequestHandler<GetWorkflowsQuery, IEnumerable<WorkflowModel>>
    {

        private readonly WorkflowDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetWorkflowsQueryHandler(WorkflowDbContext dbContext, IMapper mapper)//IRepository<Customer> repository)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


       public async Task<IEnumerable<WorkflowModel>> Handle(GetWorkflowsQuery request, CancellationToken cancellationToken)
        {
            var workflows = await this._dbContext.Workflows.Where( wf => wf.Id != 0).ToListAsync();
            
            var finalWorkflows = _mapper.Map<List<WorkflowModel>>(workflows);
            return finalWorkflows;
        }
        
    }



}
