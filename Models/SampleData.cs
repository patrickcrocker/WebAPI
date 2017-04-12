using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI
{
    public class SampleData
    {
        internal static void InitializeMyContexts(IServiceProvider serviceProvider)
        {

            if (serviceProvider == null)
            {
                throw new ArgumentNullException("serviceProvider");
            }
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<TreasureContext>();
                db.Database.EnsureCreated();

            }
            InitializeContext(serviceProvider);
        }

        private static void InitializeContext(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<TreasureContext>();
                if (DataExists<Treasure>(db))
                    return;

                AddData<Treasure>(db, new Treasure() { Id = 1, Name = "Test Data 1 - TestContext " });
                AddData<Treasure>(db, new Treasure() { Id = 2, Name = "Test Data 2 - TestContext " });
                db.SaveChanges();
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
