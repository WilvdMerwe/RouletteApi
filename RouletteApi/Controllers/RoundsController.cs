using Microsoft.AspNetCore.Mvc;
using RouletteApi.Models.Entities;
using RouletteApi.Models.Responses;
using RouletteApi.Services;

namespace RouletteApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoundsController : ControllerBase
{
    private readonly RoundService _roundService;

    public RoundsController(RoundService roundService)
    {
        _roundService = roundService;
    }

    [HttpPost("spin-open-round")]
    public async Task<Response<int>> SpinOpenRound()
    {
        return await _roundService.SpinOpenRound();
    }

    [HttpGet("open-round")]
    public async Task<Response<Round>> GetOpenRound()
    {
        return await _roundService.GetOpenRound();
    }

    [HttpGet]
    public async Task<Response<IEnumerable<Round>>> Get()
    {
        return await _roundService.GetAll();
    }
}
