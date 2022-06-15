namespace RouletteApi.Models.Responses;

public class Response
{
    public bool Success { get; set; }

    public string Message { get; set; }
}

public class Response<T>
{
    public bool Success { get; set; }

    public T Result { get; set; }

    public string Message { get; set; }
}

