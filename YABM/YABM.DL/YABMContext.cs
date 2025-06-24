using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace YABM.DL
{

    /// <summary>
    /// read about contexts here : https://learn.microsoft.com/en-us/ef/core/get-started/overview/first-app?tabs=netcore-cli
    /// </summary>
    public class YABMContext : DbContext
    {

        public DbSet<Boat> Boats { get; set; }

        public string DbPath { get; }

        public YABMContext()
        {
            DbPath = GetDefaultDbPath();
        }

        public void AddSomeBoats()
        {
            this.Boats.Add(new Boat()
            {
                Name = "Red Boat",
                Description = "My favourite red boat"
            });

            this.Boats.Add(new Boat()
            {
                Name = "Fantastic Boat",
                Description = "I love this boat."
            });
            this.Boats.Add(new Boat()
            {
                Name = "Long boat",
                Description = "A truly incredible looooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooong boat"
            });
            this.SaveChanges();
        }
        public static string GetDefaultDbPath()
        {
            return "yabm.db";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }

}
