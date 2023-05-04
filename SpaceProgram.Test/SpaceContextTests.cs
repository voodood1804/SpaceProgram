using Microsoft.EntityFrameworkCore;
using SpaceProgram.Application.infrastructure;

namespace SpaceProgram.Test
{
    [Collection("Sequential")]
    public class SpaceContextTests
    {
        private SpaceContext GetDatabase(bool deleteDb = false)
        {
            var db = new SpaceContext(new DbContextOptionsBuilder()
                .UseSqlite("Data Source = SpaceProgram.db")
                .UseLazyLoadingProxies()
                .Options);
            if (deleteDb)
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }
            return db;
        }

        [Fact]
        public void CreateDatabaseSuccessTest()
        {
            using var db = GetDatabase(deleteDb: true);
        }

        [Fact]
        public void SeedDatabaseTest()
        {
            using var db = GetDatabase(deleteDb: true);
            db.Seed();
        }
    }
}