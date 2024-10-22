using System.Net;

namespace Core.Utilities.Results.Abstract
{
    public interface IResult
    {
        string Message { get; }
        bool Success { get; }
        HttpStatusCode StatusCode { get; }
    }
}
