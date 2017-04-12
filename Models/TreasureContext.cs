using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI
{
    public class TreasureContext : DbContext
    {
        public TreasureContext(DbContextOptions<TreasureContext> options)
            : base(options)
        {
        }

        public DbSet<Treasure> Treasure { get; set; }
    }
}
