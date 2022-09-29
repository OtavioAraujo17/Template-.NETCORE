using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.Domain.Entities;

namespace Template.Data.Extensions
{
    public static class ModelBuiderExtension
    {
        public static ModelBuilder SeedData(this ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasData(
                new User { Id = Guid.Parse("5ba6645d-767a-45e0-a47f-5f274c3c7595"), Name = "User Default", Email = "userdefault@template.com" }
                );
            return builder;
        }
    }
}
