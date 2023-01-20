


namespace FoxTwoLabs.Workflow.Domain.Models
{
    using FoxTwoLabs.Workflow.Domain.Enums;
    using System.ComponentModel.DataAnnotations;

        public class Task : EntityBase
        {
            public int Id { get; set; }

            [MaxLength(240)]
            public string Name { get; set; }

            [MaxLength(500)]
            public string Description { get; set; }

            public TaskType Type { get; set; }

    }
}
