using Microsoft.EntityFrameworkCore;
using RouletteApi.Models;
using RouletteApi.Models.Entities;
using RouletteApi.Models.Requests;
using RouletteApi.Services.Interfaces;
using RouletteApi.Validation;

namespace RouletteApi.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly RouletteContext _dbContext;

        public UserService(ILogger<UserService> logger, RouletteContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<RouletteResponse> Create(UserCreateRequest request)
        {
            var response = new RouletteResponse();

            try
            {
                var validator = new UserCreateValidator();
                var validationResult = validator.Validate(request);

                if (!validationResult.IsValid)
                {
                    response.Message = validationResult.Errors.FirstOrDefault().ErrorMessage;

                    return response;
                }

                var user = new User
                {
                    Email = request.Email,
                    Name = request.Name,
                    Surname = request.Surname
                };

                await _dbContext.Users.AddAsync(user);
                await _dbContext.SaveChangesAsync();

                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Could not create user";

                _logger.LogError("{Message} - {Exception} - {StackTrace}", response.Message, ex.Message, ex.StackTrace);
            }

            return response;
        }

        public async Task<RouletteResponse> DeleteById(int id)
        {
            var response = new RouletteResponse();

            try
            {
                var user = await _dbContext.Users.FindAsync(id);
                if (user is null)
                {
                    response.Message = "User not found";
                    return response;
                }

                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();

                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Could not delete user";

                _logger.LogError("{Message} - {Exception} - {StackTrace}", response.Message, ex.Message, ex.StackTrace);
            }

            return response;
        }

        public async Task<RouletteResponse<IEnumerable<User>>> GetAll()
        {
            var response = new RouletteResponse<IEnumerable<User>>();

            try
            {
                response.Result = await _dbContext.Users.ToListAsync();

                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Could not get users";

                _logger.LogError("{Message} - {Exception} - {StackTrace}", response.Message, ex.Message, ex.StackTrace);
            }

            return response;
        }

        public async Task<RouletteResponse<User>> GetById(int id)
        {
            var response = new RouletteResponse<User>();

            try
            {
                var user = await _dbContext.Users.FindAsync(id);

                if (user is null)
                {
                    response.Message = "User not found";
                    return response;
                }

                response.Result = user;

                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Could not get user";

                _logger.LogError("{Message} - {Exception} - {StackTrace}", response.Message, ex.Message, ex.StackTrace);
            }

            return response;
        }

        public async Task<RouletteResponse<User>> Update(int id, UserUpdateRequest request)
        {
            var response = new RouletteResponse<User>();

            try
            {
                var validator = new UserUpdateValidator();
                var validationResult = validator.Validate(request);

                if (!validationResult.IsValid)
                {
                    response.Message = validationResult.Errors.FirstOrDefault().ErrorMessage;

                    return response;
                }

                var userResponse = await GetById(1);
                if (!userResponse.Success)
                {
                    response.Message = userResponse.Message;
                    return response;
                }

                var user = userResponse.Result;

                user.Name = request.Name;
                user.Surname = request.Surname;

                await _dbContext.SaveChangesAsync();

                response.Result = user;

                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = "Could not get user";

                _logger.LogError("{Message} - {Exception} - {StackTrace}", response.Message, ex.Message, ex.StackTrace);
            }

            return response;
        }
    }
}
