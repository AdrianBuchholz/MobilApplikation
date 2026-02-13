using MobilApplikation.Models;

namespace MobilApplikation.Data
{
    public static class DbSeeder
    {
        public static void Seed(AppDbContext context)
        {
            if (!context.Concerts.Any())
            {
                var concerts = new List<Concert>
                {
                    new Concert
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
                    },
                    new Concert
                    {
                        Title = "Jazz Evening",
                        Description = "Smooth jazz with special guests",
                        Performances = new List<Performance>
                        {
                            new Performance
                            {
                                DateTime = DateTime.Now.AddDays(7),
                                Location = "Gothenburg"
                            },
                            new Performance
                            {
                                DateTime = DateTime.Now.AddDays(9),
                                Location = "Malmö"
                            }
                        }
                    },
                    new Concert
                    {
                        Title = "Classical Sunset",
                        Description = "Orchestral pieces at dusk",
                        Performances = new List<Performance>
                        {
                            new Performance
                            {
                                DateTime = DateTime.Now.AddDays(14),
                                Location = "Uppsala"
                            }
                        }
                    }
                };

                context.Concerts.AddRange(concerts);
                context.SaveChanges();
            }
        }
    }
}
