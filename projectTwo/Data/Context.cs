using Microsoft.EntityFrameworkCore;
using projectTwo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectTwo.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
            
        }
        public DbSet<BusinessTravel> BusinessTravel { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<EducationField> EducationField { get; set; }
        public DbSet<JobRole> JobRole { get; set; }
        public DbSet<User> User { get; set; }

    }


}
