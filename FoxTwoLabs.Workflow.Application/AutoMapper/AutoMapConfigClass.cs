

using AutoMapper;
using FoxTwoLabs.Workflow.Application.Models;
using System.Collections.Generic;
using Domain = FoxTwoLabs.Workflow.Domain.Models;

namespace FoxTwoLabs.Workflow.Application.AutoMapper
{
    public class AutoMapperProfile : Profile
    {

        public AutoMapperProfile()
        {
           CreateMap<Domain.Models.Workflow, WorkflowModel>();
           CreateMap<WorkflowModel, Domain.Models.Workflow>();
        }
    }
}
