using MobilApplikation.Models;

namespace MobilApplikation.Data
{
    public static class DbSeeder
    {
        public static void Seed(AppDbContext context)
        {
            if (!context.Concerts.Any())
            {
                var concert = new Concert
                {
                    Title = "Rock Night",
                    Description = "Live rock music",
                    Performances = new List<Performance>
                    {
                        new Performance
                        {
                            DateTime = DateTime.Now.AddDays(3),
                            Location = "Stockholm"
                        }
                    }
                };
                context.Concerts.Add(concert);
                context.SaveChanges();
            }
        }
    }
}
