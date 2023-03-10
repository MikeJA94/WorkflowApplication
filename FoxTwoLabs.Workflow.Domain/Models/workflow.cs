


namespace FoxTwoLabs.Workflow.Domain.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

        public class Workflow : EntityBase
        {
            public int Id { get; set; }

            [MaxLength(240)]
            public string Name { get; set; }

            [MaxLength(500)]
            public string Description { get; set; }
         
            // Definition of workflow
            public string Map{ get; set; }


        Workflow ()
        {

        }
    }
}
