using DAL.Context;
using DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace DAL.Data
{
    public class Seed
    {
        public static async Task SeedUsers(DataContext context)
        {
            if (await context.Users.AnyAsync()) return;
            var role = await context.Roles.FirstOrDefaultAsync(r => r.Name == "admin");
            if (role == null)
            {
                role = new AppRole { Name = "admin" };
                context.Roles.Add(role);
                await context.SaveChangesAsync();  
            }

            var adminUser = await context.Users.FirstOrDefaultAsync(u => u.UserName == "admin");
            if (adminUser == null)
            {
                adminUser = new AppUser
                {
                    UserName = "admin",
                    KnownAs = "Admin",
                    DateOfBirth = DateOnly.FromDateTime(DateTime.UtcNow),
                    Gender = "Male",
                    City = "Admin City",
                    Country = "Admin Country",
                    Introduction = "Admin user",
                    Interests = "Administration",
                    LookingFor = "Managing Users"
                };

                
                using var hmac = new HMACSHA512();
                adminUser.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("admin")); 
                adminUser.PasswordSalt = hmac.Key;

                
                context.Users.Add(adminUser);
                await context.SaveChangesAsync();  

                
                var userRole = new IdentityUserRole<int>
                {
                    UserId = adminUser.Id,
                    RoleId = role.Id
                };

                context.UserRoles.Add(userRole);
                await context.SaveChangesAsync();  
            }
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "UserSeedData.json");
            var userData = await File.ReadAllTextAsync(filePath);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var users = JsonSerializer.Deserialize<List<AppUser>>(userData, options);

            if(users == null) return;

            foreach (var user in users)
            {
                using var hmac = new HMACSHA512();

                user.UserName = user.UserName.ToLower();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));
                user.PasswordSalt = hmac.Key;

                context.Users.Add(user);
            }

            await context.SaveChangesAsync();
        }
    }
}
