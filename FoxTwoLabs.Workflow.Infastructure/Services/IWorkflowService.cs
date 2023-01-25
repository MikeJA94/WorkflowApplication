using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxTwoLabs.Workflow.Infrastructure.Services
{
    public interface IWorkflowService
    {
        // TODO add supported methods
        public bool IsWorkflowValid(int workflowId);
    }
}
