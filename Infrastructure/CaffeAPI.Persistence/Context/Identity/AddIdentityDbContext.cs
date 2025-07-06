using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaffeAPI.Persistence.Context.Identity
{
    public class AddIdentityDbContext : IdentityDbContext<AppIdentityUser, AppIdentityRole, string>
    {
        public AddIdentityDbContext(DbContextOptions<AddIdentityDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<AppIdentityUser>(b =>
            {
                b.ToTable("Users");
            });
            builder.Entity<AppIdentityRole>(b =>
            {
                b.ToTable("Roles");
            });
        }
    }
}
