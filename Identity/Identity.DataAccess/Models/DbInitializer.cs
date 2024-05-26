using System.Security.Cryptography;
using System.Text;
using Identity.DataAccess.Models.Entities;

namespace Identity.DataAccess.Models;

public static class DbInitializer
{
    private static UserEntity[] SeedUsersData => CreateSeedUsersData();

    public static async Task SeedDatabase(IdentityDatabaseContext context)
    {
        await context.Users.AddRangeAsync(SeedUsersData);
        await context.SaveChangesAsync();
    }

    private static UserEntity[] CreateSeedUsersData()
    {
        return
        [
            new UserEntity
            {
                Username = "jonas123",
                PasswordHash = HashPassword("jonas123"),
                Email = "jonas123@gmail.com",
                Roles = ["member"],
            },
            new UserEntity
            {
                Username = "GreatAdmin",
                PasswordHash = HashPassword("administrator"),
                Email = "admin@admin.com",
                Roles = ["member", "admin"],
            },
            new UserEntity
            {
                Username = "dessert-fan-3000",
                PasswordHash = HashPassword("desserts"),
                Email = "dessert@gmail.com",
                Roles = ["member"],
            },
            new UserEntity
            {
                Username = "vegetarian-and-proud",
                PasswordHash = HashPassword("vegetarian"),
                Email = "veggies@carrots.com",
                Roles = ["member", "admin"],
            },
        ];
    }
    
    // Copied the hash password function to avoid referencing the profile for development purposes only.
   private static byte[] HashPassword(string input) => Rfc2898DeriveBytes.Pbkdf2(Encoding.ASCII.GetBytes(input), Encoding.ASCII.GetBytes("salt"), 5000, HashAlgorithmName.SHA512, 20);
}