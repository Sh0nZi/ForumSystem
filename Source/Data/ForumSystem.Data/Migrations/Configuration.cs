namespace ForumSystem.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using ForumSystem.Data.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        private UserManager<ApplicationUser> userManager;

        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;

            // TODO: Remove in production
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            this.userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            this.SeedRoles(context);
            this.SeedAdmin(context);

            if (!context.Posts.Any())
            {
                var userId = context.Users.FirstOrDefault(u => u.Email.StartsWith("Admin")).Id;
                var tags = new Tag[]
                {
                    new Tag { Name = "C#" },
                    new Tag { Name = "JavaScript" },
                    new Tag { Name = "Other" }
                };
                for (int i = 1; i <= 10; i++)
                {
                    var post = new Post()
                    {
                        AuthorId = userId,
                        Content = string.Format("asdasdasd{0}", i),
                        Title = string.Format("Pesho{0}", i),
                        Tags = tags
                    };
                    context.Posts.Add(post);
                    if (i % 5 == 0)
                    {
                        context.SaveChanges();
                    }
                }
            }
        }

        private void SeedRoles(ApplicationDbContext context)
        {
            context.Roles.AddOrUpdate(x => x.Name, new IdentityRole("Admin"));
            context.SaveChanges();
        }

        private void SeedAdmin(ApplicationDbContext context)
        {
            if (!context.Users.Any())
            { 
                var user = new ApplicationUser
                {
                    Email = "Admin@admin.com",
                    UserName = "Admin@admin.com"
                };
                this.userManager.Create(user, "123456");
                this.userManager.AddToRole(user.Id, "Admin");
            }
        }
    }
}