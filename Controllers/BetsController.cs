using Microsoft.AspNetCore.Mvc;
using RouletteApi.Models.Entities;
using RouletteApi.Models.Requests;
using RouletteApi.Models.Responses;
using RouletteApi.Services;

namespace RouletteApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BetsController : ControllerBase
    {
        private readonly BetService _betService;

        public BetsController(BetService betService)
        {
            _betService = betService;
        }

        [HttpGet("round/{roundId}")]
        public async Task<Response<IEnumerable<Bet>>> GetByRoundId(int roundId)
        {
            return await _betService.GetByRoundId(roundId);
        }

        [HttpGet("users/{userId}")]
        public async Task<Response<IEnumerable<Bet>>> GetByUserId(int userId)
        {
            return await _betService.GetByUserId(userId);
        }

        [HttpGet]
        public async Task<Response<IEnumerable<Bet>>> GetAll()
        {
            return await _betService.GetAll();
        }

        [HttpPost("place-bet")]
        public async Task<Response> PlaceBet(BetRequest request)
        {
            return await _betService.PlaceBet(request);
        }

    }
}
