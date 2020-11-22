using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using FSight.Core.Entities;
using FSight.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace FSight.Infrastructure.Data
{
    public class FSightContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            if (!userManager.Users.Any())
            {
                var customerUser = new AppUser
                {
                    FirstName = "Bob",
                    LastName = "Bobitty",
                    UserName = "bobberino1980@gmail.com",
                    Email = "bobberino1980@gmail.com"
                };

                var developerUser = new AppUser
                {
                    FirstName = "Ignas",
                    LastName = "Karpusenkovas",
                    EmployeeNumber = "EN-000002",
                    Email = "ignas.karpusenkovas@fsight.net",
                    UserName = "ignas.karpusenkovas@fsight.net"
                };
                
                var projectManagerUser = new AppUser
                {
                    FirstName = "Jack",
                    LastName = "Jones",
                    EmployeeNumber = "EN-000003",
                    Email = "jack.jones@fsight.net",
                    UserName = "jack.jones@fsight.net"
                };

                var adminUser = new AppUser
                {
                    FirstName = "Tim",
                    LastName = "Corey",
                    UserName = "timothy.corey@fsight.net",
                    Email = "timothy.corey@fsight.net",
                    EmployeeNumber = "EN-000001"
                };

                var customerRole = new AppRole
                {
                    Name = "Customer"
                };
                
                var developerRole = new AppRole
                {
                    Name = "Developer"
                };
                
                var projectManagerRole = new AppRole
                {
                    Name = "ProjectManager"
                };

                var adminRole = new AppRole
                {
                    Name = "Administrator"
                };

                await roleManager.CreateAsync(adminRole);
                await userManager.CreateAsync(adminUser, "V3r!Str0nk");
                await userManager.AddToRoleAsync(adminUser, "Administrator");

                await roleManager.CreateAsync(developerRole);
                await userManager.CreateAsync(developerUser, "Uw1llNev3rGetThi$");
                await userManager.AddToRoleAsync(developerUser, "Developer");

                await roleManager.CreateAsync(projectManagerRole);
                await userManager.CreateAsync(projectManagerUser, "h3LloW0rld!");
                await userManager.AddToRoleAsync(projectManagerUser, "ProjectManager");
                
                await roleManager.CreateAsync(customerRole);
                await userManager.CreateAsync(customerUser, "Pa$$w0rd");
                await userManager.AddToRoleAsync(customerUser, "Customer");
            }
        }
        
        /*public static async Task SeedAsync(FSightContext context, ILoggerFactory loggerFactory)
        {
            try
            {
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
        }*/
    }
}
