using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using RikaWebApp.Data;

namespace RikaWebApp.Services;

public class UserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly ApplicationDbContext _context;


    public UserService(UserManager<ApplicationUser> userManager, AuthenticationStateProvider authenticationStateProvider, ApplicationDbContext applicationDbContext)
    {
        _userManager = userManager;
        _authenticationStateProvider = authenticationStateProvider;
        _context = applicationDbContext;
    }


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

    public async Task<bool> IsExternalLoginAsync(ApplicationUser user)
    {
        var logins = await _userManager.GetLoginsAsync(user);
        return logins.Any();
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

        if (udpatedUser.Email != existingUser.UserName)
        {
            existingUser.UserName = udpatedUser.Email;
        }


        var result = await _userManager.UpdateAsync(existingUser);

        if (result.Succeeded)
        {
            await _context.SaveChangesAsync();
        }

        return result;
    }



}