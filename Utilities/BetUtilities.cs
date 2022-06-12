using RouletteApi.Enums;

namespace RouletteApi.Utilities;

public static class BetUtilities
{
    public static Color GetColorByNumber(int number)
    {
        if (number == 0)
            return Color.Green;

        var isEven = number % 2 == 0;

        return isEven ? Color.Red : Color.Black;
    }
}
