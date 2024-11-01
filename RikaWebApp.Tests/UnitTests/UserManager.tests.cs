﻿
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Moq;
using RikaWebApp.Data;

namespace RikaWebApp.Tests.UnitTests;

public  class UserManager
{




    [Fact]

    public async Task CreateUser_ShouldReturnError_IfUserNameIsNull()
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

        var mockUserManager = new Mock<UserManager<ApplicationUser>>(
        Mock.Of<IUserStore<ApplicationUser>>(),
        null, null, null, null, null, null, null, null
        );

        mockUserManager
    .   Setup(um => um.CreateAsync(It.Is<ApplicationUser>(u => u.UserName == null), password))
        .ReturnsAsync(IdentityResult.Failed(new IdentityError { }));

        // Act
        var result = await mockUserManager.Object.CreateAsync(user, password);

        // Assert
        Assert.False(result.Succeeded);
    }

    [Fact]

    public async Task CreateUser_ShouldReturnError_IfEmailIsNull()
    {
        //Arrange

        var user = new ApplicationUser
        {
            Name = "Carl",
            Gender = "-",
            Age = 0,
            Email = null,
            UserName = "carl@domain.com"
        };

        var password = "BytMig123!";

        var mockUserManager = new Mock<UserManager<ApplicationUser>>(
        Mock.Of<IUserStore<ApplicationUser>>(),
        null, null, null, null, null, null, null, null
        );

        mockUserManager
        .Setup(um => um.CreateAsync(It.Is<ApplicationUser>(u => u.Email == null), password))
        .ReturnsAsync(IdentityResult.Failed(new IdentityError { }));

        // Act
        var result = await mockUserManager.Object.CreateAsync(user, password);

        // Assert
        Assert.False(result.Succeeded);
    }
}
