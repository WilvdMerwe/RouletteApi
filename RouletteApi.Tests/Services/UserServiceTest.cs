using Microsoft.Extensions.Logging.Abstractions;
using RouletteApi.Models.Requests;
using RouletteApi.Repositories;
using RouletteApi.Services;

namespace RouletteApi.Tests.Services;

public class UserServiceTest
{
    // Arrange
    private readonly List<UserCreateRequest> _validMockUserCreateRequests = new()
    {
        new UserCreateRequest
        {
            Email = "c137@counselofricks.com",
            Name = "Rick",
            Surname = "Sanchez"
        },
        new UserCreateRequest
        {
            Email = "morty.smith@gmail.com",
            Name = "Morty",
            Surname = "Smith"
        }
    };

    private readonly List<UserCreateRequest> _invalidMockUserCreateRequests = new()
    {
        new UserCreateRequest
        {
            Email = "c137",
            Name = "Rick",
            Surname = "Sanchez"
        },
        new UserCreateRequest
        {
            Email = "morty.smith@gmail.com",
            Name = "Morty",
            Surname = ""
        }
    };

    [Fact]
    public async void CreateUsers_Failure_Validation()
    {
        // Act
        await CreateMockUsers(_invalidMockUserCreateRequests, false);
    }

    [Fact]
    public async void CreateUsers_Success()
    {
        // Act
        await CreateMockUsers(_validMockUserCreateRequests, true);
    }

    private async Task CreateMockUsers(List<UserCreateRequest> userCreateRequests, bool isSuccessTest)
    {
        using (var context = new RouletteDbContext(Options.MockDbContextOptions))
        {
            var userRepo = new UserRepo(context);

            var userService = new UserService(new NullLogger<UserService>(), userRepo);

            foreach (var user in userCreateRequests)
            {
                var response = await userService.Create(user);

                // Assert
                var passed = response.Success == isSuccessTest;
                Assert.True(passed);
            }
        }
    }

    [Fact]
    public async void GetAllUsers_Success()
    {
        // Act
        using (var context = new RouletteDbContext(Options.MockDbContextOptions))
        {
            var userRepo = new UserRepo(context);

            var userService = new UserService(new NullLogger<UserService>(), userRepo);

            var response = await userService.GetAll();

            // Assert
            Assert.True(response.Success);
            Assert.Equal(_validMockUserCreateRequests.Count, response.Result.Count());
        }
    }

    [Fact]
    public async void UpdateUser_Failure_Validation()
    {
        // Arrange
        var userUpdateRequest = new UserUpdateRequest
        {
            Name = "",
            Surname = "Potter",
        };

        // Act
        using (var context = new RouletteDbContext(Options.MockDbContextOptions))
        {
            var userRepo = new UserRepo(context);

            var userService = new UserService(new NullLogger<UserService>(), userRepo);

            var response = await userService.Update(1, userUpdateRequest);

            // Assert
            Assert.True(!response.Success);
        }
    }

    [Fact]
    public async void UpdateUser_Success()
    {
        // Arrange
        var userUpdateRequest = new UserUpdateRequest
        {
            Name = "Harry",
            Surname = "Potter",
        };

        // Act
        using (var context = new RouletteDbContext(Options.MockDbContextOptions))
        {
            var userRepo = new UserRepo(context);

            var userService = new UserService(new NullLogger<UserService>(), userRepo);

            var response = await userService.Update(1, userUpdateRequest);

            // Assert
            Assert.True(response.Success);
        }
    }

    [Fact]
    public async void DeleteUser_Success()
    {
        // Act
        using (var context = new RouletteDbContext(Options.MockDbContextOptions))
        {
            var userRepo = new UserRepo(context);

            var userService = new UserService(new NullLogger<UserService>(), userRepo);

            var response = await userService.DeleteById(2);

            // Assert
            Assert.True(response.Success);
        }
    }
}
