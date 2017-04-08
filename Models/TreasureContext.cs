using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class TreasureContext : DbContext
    {
        public TreasureContext(DbContextOptions<TreasureContext> options)
            : base(options)
        {
        }

        public DbSet<WebAPI.Models.Treasure> Treasure { get; set; }
    }
}
