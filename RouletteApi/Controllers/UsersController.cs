using Microsoft.AspNetCore.Mvc;
using RouletteApi.Models.Entities;
using RouletteApi.Models.Requests;
using RouletteApi.Models.Responses;
using RouletteApi.Services;

namespace RouletteApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;

    public UsersController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPut("{id}/deposit/{amount}")]
    public async Task<Response<double>> Deposit(int id, double amount)
    {
        return await _userService.Deposit(id, amount);
    }

    [HttpGet]
    public async Task<Response<IEnumerable<User>>> Get()
    {
        return await _userService.GetAll();
    }

    [HttpGet("{id}")]
    public async Task<Response<User>> Get(int id)
    {
        return await _userService.GetById(id);
    }

    [HttpPut("{id}")]
    public async Task<Response<User>> Update(int id, UserUpdateRequest request)
    {
        return await _userService.Update(id, request);
    }

    [HttpPost("create")]
    public async Task<Response<int>> Create(UserCreateRequest request)
    {
        return await _userService.Create(request);
    }

    [HttpDelete("{id}")]
    public async Task<Response> Delete(int id)
    {
        return await _userService.DeleteById(id);
    }
}
