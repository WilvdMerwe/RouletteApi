using Microsoft.EntityFrameworkCore;
using RouletteApi.Enums;
using RouletteApi.Models.Entities;
using RouletteApi.Models.Requests;
using RouletteApi.Models.Responses;
using RouletteApi.Repositories;
using RouletteApi.Validators;

namespace RouletteApi.Services;

public class BetService : Service
{
    private readonly BetRepo _betRepo;
    private readonly RoundRepo _roundRepo;
    private readonly UserRepo _userRepo;

    public BetService(
        ILogger<BetService> logger, 
        BetRepo betRepo,
        RoundRepo roundRepo,
        UserRepo userRepo
        ) : base(logger)
    {
        _betRepo = betRepo;
        _roundRepo = roundRepo;
        _userRepo = userRepo;
    }

    public async Task<Response<IEnumerable<Bet>>> GetAll()
    {
        var response = new Response<IEnumerable<Bet>>();

        try
        {
            response.Result = await _betRepo.GetAllAsync();

            response.Success = true;
        }
        catch (Exception ex)
        {
            response.Message = "Could not get bets";
            LogException(response.Message, ex);
        }

        return response;
    }

    public async Task<Response<IEnumerable<Bet>>> GetByUserId(int userId)
    {
        var response = new Response<IEnumerable<Bet>>();

        try
        {
            var userBets = await _betRepo.GetByUserId(userId);

            response.Result = userBets;

            response.Success = true;
        }
        catch (Exception ex)
        {
            response.Message = "Could not get bets for user";
            LogException(response.Message, ex);
        }

        return response;
    }

    public async Task<Response> PlaceBet(BetRequest request)
    {
        var response = new Response();

        try
        {
            var validator = new BetRequestValidator();
            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                response.Message = validationResult.Errors.FirstOrDefault().ErrorMessage;
                return response;
            }

            var user = await _userRepo.GetAsync(request.UserId);
            if (user is null)
            {
                response.Message = "User not found";
                return response;
            }

            var round = await _roundRepo
                .FindBy(r => r.Status == RoundStatus.Open)
                .FirstOrDefaultAsync();

            if (round is null)
            {
                var roundId = await _roundRepo.CreateAsync(new Round
                {
                    Status = RoundStatus.Open,
                    MinimumBet = 1,
                });

                round = await _roundRepo.GetAsync(roundId);
            }

            var userCurrentRound = await _roundRepo
                .FindBy(r => r.Status == RoundStatus.Open)
                .Include(r => r.UserRounds.Where(ur => ur.UserId == request.UserId))
                .FirstOrDefaultAsync();

            var bet = new Bet
            {
                Type = request.Type,
                Numbers = string.Join(",", request.Numbers),
                Amount = request.Amount
            };

            userCurrentRound.UserRounds.Add(new UserRound
            {
                RoundId = userCurrentRound.Id,
                UserId = request.UserId,
                Bets = new List<Bet> { bet }
            });

            await _roundRepo.SaveChangesAsync();

            response.Success = true;
        }
        catch (Exception ex)
        {
            response.Message = "Could not place bet";
            LogException(response.Message, ex);
        }

        return response;
    }
}
