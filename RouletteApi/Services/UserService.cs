using RouletteApi.Models.Entities;
using RouletteApi.Models.Requests;
using RouletteApi.Models.Responses;
using RouletteApi.Repositories;
using RouletteApi.Validators;

namespace RouletteApi.Services;

public class UserService : Service
{
    private readonly UserRepo _userRepo;

    public UserService(
        ILogger<UserService> logger,
        UserRepo userRepo
        ) : base (logger)
    {
        _userRepo = userRepo;
    }

    public async Task<Response<double>> Deposit(int userId, double amount)
    {
        var response = new Response<double>();

        try
        {
            if (amount < 0)
            {
                response.Message = "Amount should be more than 0";
                return response;
            }

            var userResponse = await GetById(userId);
            if (!userResponse.Success)
            {
                response.Message = userResponse.Message;
                return response;
            }

            var user = userResponse.Result;

            user.Balance += amount;

            await _userRepo.UpdateAsync(user);

            response.Result = user.Balance;

            response.Success = true;
        }
        catch (Exception ex)
        {
            response.Message = "Could not deposit";
            LogException(response.Message, ex);
        }

        return response;
    }

    public async Task<Response<int>> Create(UserCreateRequest request)
    {
        var response = new Response<int>();

        try
        {
            var validator = new UserCreateValidator();
            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                response.Message = validationResult.Errors.FirstOrDefault().ErrorMessage;
                return response;
            }

            var doesEmailExist = await _userRepo.DoesEmailExist(request.Email);
            if (doesEmailExist)
            {
                response.Message = "A user with provided email already exists";
                return response;
            }

            var user = new User
            {
                Email = request.Email,
                Name = request.Name,
                Surname = request.Surname
            };

            response.Result = await _userRepo.CreateAsync(user);

            response.Success = true;
        }
        catch (Exception ex)
        {
            response.Message = "Could not create user";
            LogException(response.Message, ex);
        }

        return response;
    }

    public async Task<Response> DeleteById(int id)
    {
        var response = new Response();

        try
        {
            var userResponse = await GetById(id);
            if (!userResponse.Success)
            {
                response.Message = userResponse.Message;
                return response;
            }

            var user = userResponse.Result;

            await _userRepo.DeleteAsync(user);

            response.Success = true;
        }
        catch (Exception ex)
        {
            response.Message = "Could not delete user";
            LogException(response.Message, ex);
        }

        return response;
    }

    public async Task<Response<IEnumerable<User>>> GetAll()
    {
        var response = new Response<IEnumerable<User>>();

        try
        {
            response.Result = await _userRepo.GetAllAsync();

            response.Success = true;
        }
        catch (Exception ex)
        {
            response.Message = "Could not get users";
            LogException(response.Message, ex);
        }

        return response;
    }

    public async Task<Response<User>> GetById(int id)
    {
        var response = new Response<User>();

        try
        {
            var user = await _userRepo.GetAsync(id);
            if (user is null)
            {
                response.Message = "User does not exist";
                return response;
            }

            response.Result = user;

            response.Success = true;
        }
        catch (Exception ex)
        {
            response.Message = "Could not get user";
            LogException(response.Message, ex);
        }

        return response;
    }

    public async Task<Response<User>> Update(int id, UserUpdateRequest request)
    {
        var response = new Response<User>();

        try
        {
            var validator = new UserUpdateValidator();
            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                response.Message = validationResult.Errors.FirstOrDefault().ErrorMessage;
                return response;
            }

            var userResponse = await GetById(id);
            if (!userResponse.Success)
            {
                response.Message = userResponse.Message;
                return response;
            }

            var user = userResponse.Result;

            user.Name = request.Name;
            user.Surname = request.Surname;
            user.Updated = DateTime.UtcNow;

            await _userRepo.UpdateAsync(user);

            response.Result = user;

            response.Success = true;
        }
        catch (Exception ex)
        {
            response.Message = "Could not update user";
            LogException(response.Message, ex);
        }

        return response;
    }
}
