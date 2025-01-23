using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommer_web_api.Model;
using Microsoft.EntityFrameworkCore;

namespace ecommer_web_api.data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options): base(options) {}
        public DbSet <Category> Categories { get; set; }
    }
}