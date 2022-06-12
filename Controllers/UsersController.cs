using Microsoft.AspNetCore.Mvc;
using RouletteApi.Models;
using RouletteApi.Models.Entities;
using RouletteApi.Models.Requests;
using RouletteApi.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RouletteApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    // GET: api/users
    [HttpGet]
    public async Task<RouletteResponse<IEnumerable<User>>> Get()
    {
        return await _userService.GetAll();
    }

    // GET api/users/{id}
    [HttpGet("{id}")]
    public async Task<RouletteResponse<User>> Get(int id)
    {
        return await _userService.GetById(id);
    }

    // GET api/users/{id}
    [HttpPut("{id}")]
    public async Task<RouletteResponse<User>> Update(int id, [FromBody] UserUpdateRequest request)
    {
        return await _userService.Update(id, request);
    }

    // POST api/users
    [HttpPost("create")]
    public async Task<RouletteResponse> Post([FromBody] UserCreateRequest request)
    {
        return await _userService.Create(request);
    }

    // DELETE api/users/{id}
    [HttpDelete("{id}")]
    public async Task<RouletteResponse> Delete(int id)
    {
        return await _userService.DeleteById(id);
    }
}
