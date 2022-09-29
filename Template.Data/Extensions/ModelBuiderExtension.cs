using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Domain.Entities;
using Template.Domain.Models;

namespace Template.Data.Extensions
{
    public static class ModelBuiderExtension
    {
        public static ModelBuilder ApplyGlobalConfigurations(this ModelBuilder builder)
        {
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                foreach (var item in entityType.GetProperties())
                {
                    switch (item.Name)
                    {
                        case nameof(Entity.Id):
                            item.IsKey();
                            break;
                        case nameof(Entity.DateUpdated):
                            item.IsNullable = true;
                            break;
                        case nameof(Entity.DateCreated):
                            item.IsNullable = false;
                            item.SetDefaultValue(DateTime.Now);
                            break;
                        case nameof(Entity.IsDeleted):
                            item.IsNullable = false;
                            item.SetDefaultValue(false);
                            break;
                        default:
                            break;
                    }
                }
            }

            return builder;
        }

        public static ModelBuilder SeedData(this ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasData(
                new User
                {
                    Id = Guid.Parse("5ba6645d-767a-45e0-a47f-5f274c3c7595"),
                    Name = "User Default",
                    Email = "userdefault@template.com",
                    DateCreated = new DateTime(2020, 2, 2),
                    IsDeleted = false,
                    DateUpdated = null
                }
                );
            return builder;
        }
    }
}
