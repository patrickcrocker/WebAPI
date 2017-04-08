using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class SampleData
    {
        internal static async Task InitializeMyContexts(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
            {
                throw new ArgumentNullException("serviceProvider");
            }

            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<TreasureContext>();
                await db.Database.EnsureCreatedAsync();
            }

            await InitializeContext(serviceProvider);
        }

        private static async Task InitializeContext(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<TreasureContext>();

                if (DataExists<Treasure>(db))
                    return;

                AddData<Treasure>(db, new Treasure() { ID = 1, Name = "Test Item 1 - TreasureContext ", AcquiredDate = new DateTime(2017,03,13) });

                AddData<Treasure>(db, new Treasure() { ID = 2, Name = "Test Item 2 - TreasureContext ", AcquiredDate = new DateTime(2017, 02, 14) });

                await db.SaveChangesAsync();
            }
        }



        private static bool DataExists<TData>(DbContext db) where TData : class
        {
            var existingData = db.Set<TData>().ToList();

            if (existingData.Count > 0)
                return true;

            return false;
        }



        private static void AddData<TData>(DbContext db, object item) where TData : class
        {
            db.Entry(item).State = EntityState.Added;
        }
    }
}
