using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using FSight.Core.Entities;
using Microsoft.Extensions.Logging;

namespace FSight.Infrastructure.Data
{
    public class FSightContextSeed
    {
        public static async Task SeedAsync(FSightContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.ProjectManagers.Any())
                {
                    var projectManagerData =
                        File.ReadAllText("../FSight.Infrastructure/Data/SeedData/projectManagers.json");
                    var projectManagers = JsonSerializer.Deserialize<List<ProjectManager>>(projectManagerData);

                    foreach (var item in projectManagers)
                    {
                        context.ProjectManagers.Add(item);
                    }

                    await context.SaveChangesAsync();
                }
                
                if (!context.Projects.Any())
                {
                    var projectData = File.ReadAllText("../FSight.Infrastructure/Data/SeedData/projects.json");
                    var projects = JsonSerializer.Deserialize<List<Project>>(projectData);

                    foreach (var item in projects)
                    {
                        context.Projects.Add(item);
                        // context.ChangeTracker.TrackGraph(item, node =>
                        //     node.Entry.State = !node.Entry.IsKeySet ? EntityState.Added : EntityState.Unchanged);
                    }

                    await context.SaveChangesAsync();
                }
                if (!context.Developers.Any())
                {
                    var devData = File.ReadAllText("../FSight.Infrastructure/Data/SeedData/developers.json");
                    var developers = JsonSerializer.Deserialize<List<Developer>>(devData);

                    foreach (var item in developers)
                    {
                        context.Developers.Add(item);
                        // context.ChangeTracker.TrackGraph(item, node =>
                        //     node.Entry.State = !node.Entry.IsKeySet ? EntityState.Added : EntityState.Unchanged);
                    }

                    await context.SaveChangesAsync();
                }

                
                if (!context.Customers.Any())
                {
                    var customerData =
                        File.ReadAllText("../FSight.Infrastructure/Data/SeedData/customers.json");
                    var customers = JsonSerializer.Deserialize<List<Customer>>(customerData);
                    
                    foreach (var item in customers)
                    {
                        context.Customers.Add(item);
                    }
                    
                    await context.SaveChangesAsync();
                }
                
                
                
                if (!context.Tickets.Any())
                {
                    var ticketData = File.ReadAllText("../FSight.Infrastructure/Data/SeedData/tickets.json");
                    var tickets = JsonSerializer.Deserialize<List<Ticket>>(ticketData);

                    foreach (var item in tickets)
                    {
                        context.Tickets.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.Comments.Any())
                {
                    var commentData = File.ReadAllText("../FSight.Infrastructure/Data/SeedData/comments.json");
                    var comments = JsonSerializer.Deserialize<List<Comment>>(commentData);

                    foreach (var item in comments)
                    {
                        context.Comments.Add(item);
                    }
                    
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<FSightContextSeed>();
                logger.LogError($"Context Seed failed: { ex.Message }");
            }
        }
    }
}
