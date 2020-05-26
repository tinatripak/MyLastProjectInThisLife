using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication8.Models
{
     public class TodoContext : DbContext
        {
            public TodoContext(DbContextOptions<TodoContext> options)
               : base(options)
            {

            }

            public DbSet<Films> TodoItems { get; set; }
        }
    
}
