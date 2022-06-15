namespace RouletteApi.Enums;

public enum BetType
{
    #region x1
    AnyBlack,
    AnyRed,
    Even,
    Odd,
    OneToEighteen,
    NineteenToThirtySix,
    #endregion x1

    #region x2
    FirstTwelve,
    SecondTwelve,
    ThirdTwelve,
    ColumnOne,
    ColumnTwo,
    ColumnThree,
    #endregion x2

    #region x5
    DoubleStreet,
    #endregion x5

    #region x6
    TopLine,
    #endregion x6

    #region x8
    Corner,
    #endregion x8

    #region x11
    Street,
    Trio,
    #endregion x11

    #region x17
    Split,
    Zero,
    #endregion x17

    #region x35
    StraightUp
    #endregion x35
}
