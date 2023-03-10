using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Metrics;
using System.Net;
using System.Numerics;
using System.Reflection.Emit;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using FoxTwoLabs.Workflow.Domain;
using Models = FoxTwoLabs.Workflow.Domain.Models;

namespace FoxTwoLabs.Workflow.Infrastructure.Data
{
    public class WorkflowDbContext : DbContext
    {
        private readonly ILogger<WorkflowDbContext> logger;
        
        public WorkflowDbContext(DbContextOptions<WorkflowDbContext> options, ILogger<WorkflowDbContext> logger) : base(options)
        {
            this.logger = logger;
        }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Models.Workflow>()
                .Property(e => e.Map)
                .HasColumnType("text");
            }

           
        //Entities
        public DbSet<Models.Workflow> Workflows { get; set; }

       
        public async Task<int> SaveChangesAsync(int currentUserId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var result = 0;
            try
            {
                // add additional information...
                AddTimestamps(currentUserId);
                result = await base.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                this.logger.LogDebug("{@ex}", ex);
            }

            return result;

        }

        private void AddTimestamps(int currentUserId)
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is EntityBase && (x.State == EntityState.Added || x.State == EntityState.Modified));


            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((EntityBase)entity.Entity).DateCreated = DateTimeOffset.UtcNow;
                    ((EntityBase)entity.Entity).CreatedById = currentUserId;
                }
                else
                {
                    try
                    {
                        entity.Property("CreatedById").IsModified = false;
                        entity.Property("DateCreated").IsModified = false;
                    }
                    catch (Exception ex)
                    {
                        Console.Write("No BaseEntity exists" + ex.Message);
                    }
                }
                ((EntityBase)entity.Entity).DateUpdated = DateTimeOffset.UtcNow;
                ((EntityBase)entity.Entity).UpdatedById = currentUserId;
            }
        }
    }


}
