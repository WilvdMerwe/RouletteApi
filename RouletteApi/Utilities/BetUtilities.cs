using RouletteApi.Enums;
using RouletteApi.Models.Entities;

namespace RouletteApi.Utilities;

public static class BetUtilities
{
    public static double CalculateBetPayout(Bet bet, int resultNumber)
    {
        var betNumbers = bet.Numbers.Split(',').Select(int.Parse).ToList();
        var betAmount = bet.Amount;

        if (!betNumbers.Contains(resultNumber))
            return betAmount;

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
                return betAmount * 5;

            case BetType.Split:
            case BetType.Zero:
                return betAmount * 17;

            case BetType.StraightUp:
                return betAmount * 35;
        }

        return 0;
    }

    public static Color GetColorByNumber(int number)
    {
        if (number == 0)
            return Color.Green;

        var isEven = number % 2 == 0;

        return isEven ? Color.Red : Color.Black;
    }
}
