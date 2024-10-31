
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Moq;
using RikaWebApp.Data;

namespace RikaWebApp.Tests.UnitTests;

public  class UserManager
{
    /*
            Testfall
        Testfall 1

        Beskrivning: Testa att alla fält är ifyllda korrekt och att felmeddelande visas när fält är felaktigt ifyllda.

        Förutsättning: Användaren har gått in på signup-sidan för att registrera ett nytt konto.

        Steg:

        Lämna fältet User Name tomt och försök registrera.
        Lämna fältet Email tomt och försök registrera.
        Lämna fältet Password tomt och försök registrera.
        Lämna fältet Confirm Password tomt och försök registrera
        Mata in en ogiltig e-postadress och försök registrera.
        Mata in ett ogiltigt password och försök registrera.
        Mata in confirm password som inte matchar password och försök registrera.
        Lämna checkbox för terms tom och försök registrera.

        Förväntat resultat:

        När "User Name" är tomt ska felmeddelande visas "User Name is required".
        När "Email" är tomt ska felmeddelande visas "Email is required".
        När "Password" är tomt ska felmeddelande visas "Password is reuqired".
        När "Confirm Password" är tomt ska felmeddelande visas "You must confirm your password".
        När en ogiltig e-postadress matats in ska felmeddelande visas "Enter a valid email.".
        När ett ogiltigt lösenord matats in ska felmeddelande visas "Password must be at least 8 characters long and include one uppercase letter, one lowercase letter, one number, and one special character." (alternativt bara "Enter a valid password").
        När confirm password inte matchar password ska felmeddelande visas "You must confirm your password".
        När checkbox för terms lämnas tom ska felmeddelande visas "You must agree to our terms & conditions".
     
     
     
     */




    [Fact]

    public async Task CreateUser_ShouldReturnError_IfUserNameIsMissing()
    {
        //Arrange

        var user = new ApplicationUser
        {
            Name = "Carl",
            Gender = "-",
            Age = 0,
            Email = "carl@domain.com",
            UserName = null
        };

        var password = "BytMig123!";

        var mockUserStore = new Mock<IUserStore<ApplicationUser>>();
        var userManager = new UserManager<ApplicationUser>(
            mockUserStore.Object,
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null
        );


        //Act

        var result = userManager.CreateAsync(user, password);


        //Assert

        Assert.False(result.IsCompletedSuccessfully);
    }
}
