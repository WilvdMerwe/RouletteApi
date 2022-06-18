using RouletteApi.Enums;
using RouletteApi.Models.Entities;

namespace RouletteApi.Utilities;

public static class BetUtilities
{
    public static double CalculateBetPayout(Bet bet, int resultNumber)
    {
        var betNumbers = bet.Numbers.Split(',').Select(int.Parse).ToList();
        var betAmount = bet.Amount;

        var isBetTypeOutside = IsBetTypeOutside(bet.Type);
        if (isBetTypeOutside)
            betNumbers = GetNumbersByOutsideBetType(bet.Type);

        if (!betNumbers.Contains(resultNumber))
            return 0;

        switch (bet.Type)
        {
            case BetType.AnyBlack:
            case BetType.AnyRed:
            case BetType.Even:
            case BetType.Odd:
            case BetType.OneToEighteen:
            case BetType.NineteenToThirtySix:
                return betAmount;

            case BetType.FirstTwelve:
            case BetType.SecondTwelve:
            case BetType.ThirdTwelve:
            case BetType.ColumnOne:
            case BetType.ColumnTwo:
            case BetType.ColumnThree:
                return betAmount * 2;


            case BetType.DoubleStreet:
                return betAmount * 5;

            case BetType.TopLine:
                return betAmount * 6;

            case BetType.Corner:
                return betAmount * 8;

            case BetType.Street:
            case BetType.Trio:
                return betAmount * 11;

            case BetType.Split:
            case BetType.Zero:
                return betAmount * 17;

            case BetType.StraightUp:
                return betAmount * 35;
        }

        return 0;
    }

    private static bool IsBetTypeOutside(BetType betType)
    {
        switch (betType)
        {
            case BetType.AnyBlack:
            case BetType.AnyRed:
            case BetType.Even:
            case BetType.Odd:
            case BetType.OneToEighteen:
            case BetType.NineteenToThirtySix:
            case BetType.FirstTwelve:
            case BetType.SecondTwelve:
            case BetType.ThirdTwelve:
            case BetType.ColumnOne:
            case BetType.ColumnTwo:
            case BetType.ColumnThree:
                return true;
        }

        return false;
    }

    private static List<int> GetNumbersByOutsideBetType(BetType betType)
    {
        switch (betType)
        {
            case BetType.AnyBlack:
            case BetType.Odd:
                return new List<int>
                {
                    1,3,5,7,9,11,13,15,17,19,21,23,25,27,29,31,33,35
                };

            case BetType.AnyRed:
            case BetType.Even:
                return new List<int>
                {
                    2,4,6,8,10,12,14,16,18,20,22,24,26,28,30,32,34,36
                };

            case BetType.OneToEighteen:
                return new List<int>
                {
                    1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18
                };

            case BetType.NineteenToThirtySix:
                return new List<int>
                {
                    19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36
                };

            case BetType.FirstTwelve:
                return new List<int>
                {
                    1,2,3,4,5,6,7,8,9,10,11,12
                };

            case BetType.SecondTwelve:
                return new List<int>
                {
                    13,14,15,17,18,19,20,21,22,23,24
                };

            case BetType.ThirdTwelve:
                return new List<int>
                {
                    25,26,27,28,29,30,31,32,33,34,35,36
                };

            case BetType.ColumnOne:
                return new List<int>
                {
                    1,4,7,10,13,16,19,22,25,28,31,34
                };

            case BetType.ColumnTwo:
                return new List<int>
                {
                    2,5,8,11,14,17,20,23,26,29,32,35
                };

            case BetType.ColumnThree:
                return new List<int>
                {
                    3,6,9,12,15,18,21,24,27,30,33,36
                };
        }

        return new List<int>();
    }
}
