using System;
using System.ComponentModel.DataAnnotations;

namespace FoxTwoLabs.Workflow.Domain
{
    public abstract class EntityBase
    {
        // Internal constructor prevents consumption outside of assembly
        internal EntityBase()
        {
        }

        public DateTimeOffset DateCreated { get; set; }

        public int? CreatedById { get; set; }

        public DateTimeOffset? DateUpdated { get; set; }

        public int? UpdatedById { get; set; }


        [MaxLength(1000)]
        public string Notes { get; set; }

       


    }
}
