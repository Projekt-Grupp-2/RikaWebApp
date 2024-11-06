using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using RikaWebApp.Data;

namespace RikaWebApp.Services;

public class UserService(UserManager<ApplicationUser> userManager, AuthenticationStateProvider authenticationStateProvider, ApplicationDbContext applicationDbContext)
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly AuthenticationStateProvider _authenticationStateProvider = authenticationStateProvider;
    private readonly ApplicationDbContext _context = applicationDbContext;



    public async Task<ApplicationUser> GetUserAsync()
    {
        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        if (user.Identity == null || !user.Identity.IsAuthenticated)
        {
            return null!;
        }

        var currentUser = await _userManager.GetUserAsync(user);

        return currentUser ?? null!;
    }



    public async Task<IdentityResult> UpdateUserAsync(ApplicationUser udpatedUser)
    {
        var existingUser = await GetUserAsync();
        if (existingUser == null)
        {
            return IdentityResult.Failed(new IdentityError { Description = "User was not found" });
        }

        existingUser.Name = udpatedUser.Name;
        existingUser.Email = udpatedUser.Email;
        existingUser.Gender = udpatedUser.Gender;
        existingUser.Age = udpatedUser.Age;
        existingUser.UserProfileImage = udpatedUser.UserProfileImage;


        var result = await _userManager.UpdateAsync(existingUser);
        if (result.Succeeded)
        {
            await _context.SaveChangesAsync();
        }

        return result;
    }
}