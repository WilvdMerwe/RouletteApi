namespace RouletteApi.Models;

public class RouletteResponse
{
    public bool Success { get; set; }

    public string Message { get; set; }
}

public class RouletteResponse<T>
{
    public bool Success { get; set; }

    public T Result { get; set; }

    public string Message { get; set; }
}

