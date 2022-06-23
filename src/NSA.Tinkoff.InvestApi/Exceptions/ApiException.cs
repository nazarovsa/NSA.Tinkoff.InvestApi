namespace NSA.Tinkoff.InvestApi.Exceptions;

public class ApiException : Exception
{
    public ApiException(string message) : base(message)
    {
    }

    public ApiException(Exception inner) : base(inner.Message, inner)
    {
    }

    public ApiException(string message, Exception inner) : base(message, inner)
    {
    }
}