using Microsoft.AspNetCore.Identity;
using Store.Data.Entities.IdentityEntities;

namespace Store.Repository
{
    public class StoreIdentityContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (userManager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Mohamed Yasser",
                    Email = "mohamed.yasser16011@gmail.com",
                    UserName = "mohamedYasser",
                    Address = new Address { 
                    FirstName = "Mohamed",
                    LastName = "Yasser",
                    City = "Alexandria",
                    State = "Alexandria",
                    PostalCode = "123456",
                    Street = "123"
                    }
                };
                await userManager.CreateAsync(user,"Password123!");
            } 
        }
    }
}
