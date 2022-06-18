using RouletteApi.Enums;
using RouletteApi.Models.Entities;
using RouletteApi.Models.Responses;
using RouletteApi.Repositories;
using RouletteApi.Utilities;

namespace RouletteApi.Services;

public class RoundService : Service
{
    private readonly RoundRepo _roundRepo;
    private readonly UserRepo _userRepo;

    public RoundService(
        ILogger<RoundService> logger,
        RoundRepo roundRepo,
        UserRepo userRepo
        ) : base(logger)
    {
        _roundRepo = roundRepo;
        _userRepo = userRepo;
    }

    public async Task<Response<int>> SpinOpenRound()
    {
        var response = new Response<int>();

        try
        {
            var roundResponse = await GetOpenRound();
            if (!roundResponse.Success)
            {
                response.Message = roundResponse.Message;
                return response;
            }

            var round = roundResponse.Result;

            round.ResultNumber = Random.Shared.Next(37);
            round.Status = RoundStatus.Closed;

            await _roundRepo.UpdateAsync(round);

            foreach (var userRound in round.UserRounds)
            {
                var user = await _userRepo.GetAsync(userRound.UserId);

                foreach (var bet in userRound.Bets)
                    user.Balance += BetUtilities.CalculateBetPayout(bet, round.ResultNumber);

                await _userRepo.UpdateAsync(user);
            }

            round.Status = RoundStatus.Completed;

            await _roundRepo.UpdateAsync(round);

            response.Result = round.ResultNumber;

            response.Success = true;
        }
        catch (Exception ex)
        {
            response.Message = "Could not spin open round";
            LogException(response.Message, ex);
        }

        return response;
    }

    public async Task<Response<Round>> GetOpenRound()
    {
        var response = new Response<Round>();

        try
        {
            var openRound = await _roundRepo.GetOpenRound();
            if (openRound is null)
            {
                response.Message = "No open round found";
                return response;
            }

            response.Result = openRound;

            response.Success = true;
        }
        catch (Exception ex)
        {
            response.Message = "Could not get rounds";
            LogException(response.Message, ex);
        }

        return response;
    }

    public async Task<Response<IEnumerable<Round>>> GetAll()
    {
        var response = new Response<IEnumerable<Round>>();

        try
        {
            response.Result = await _roundRepo.GetAllAsync();

            response.Success = true;
        }
        catch (Exception ex)
        {
            response.Message = "Could not get rounds";
            LogException(response.Message, ex);
        }

        return response;
    }
}
