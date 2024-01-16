using Microsoft.AspNetCore.Identity;
using Final.Enums;
namespace Final.Areas.Identity.Data
{
    public static class ContextSeed
{
    public static async Task SeedRolesAsync(UserManager<LumosTutorUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        //Seed Roles
        await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Admin.ToString()));
        await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Tutor.ToString()));
        await roleManager.CreateAsync(new IdentityRole(Enums.Roles.Student.ToString()));
    }
    public static async Task SeedAdminAsync (UserManager<LumosTutorUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new LumosTutorUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                Name = "admin",
                Address = "Danang",
                PhoneNumber = "0789948756",
                //DOB = "",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "123Pa$$word.");
                    await userManager.AddToRoleAsync(defaultUser, Enums.Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Enums.Roles.Tutor.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Enums.Roles.Student.ToString());
                }

            }
        }
}
}