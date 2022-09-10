using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexRM.Generated.Infrastructure.EFCore
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext()
        {
            Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseNpgsql("Host=my_host;Database=my_db;Username=my_user;Password=my_pw");
        }
    }
}
