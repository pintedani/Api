using Microsoft.EntityFrameworkCore;

public static class SeedData
{
    public static void Seed(ModelBuilder builder)
    {
        builder.Entity<HouseEntity>().HasData(
            new HouseEntity{
                Id = 1,
                Address = "12 Valley of Kings",
                Country = "Switzerland",
                Description = "A superb detached Victorian property",
                Price = 900000
            }
        ); 
    }
}