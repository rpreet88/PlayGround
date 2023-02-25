using System.Net;

namespace DraftSimulator;

public abstract class DraftException : Exception
{
    public DraftException(string message)
        : base(message)
    {  }

    public abstract HttpStatusCode StatusCode { get; }
}

public class InvalidRequest : DraftException
{
    public InvalidRequest()
        : base("Unable to create draft due to invalid request.")
    {  }

    public override HttpStatusCode StatusCode => 
            HttpStatusCode.BadRequest;
}

public class GeneralFailure : DraftException
{
    public GeneralFailure()
        : base("Request Failed due to internal errors.")
    {  }

    public override HttpStatusCode StatusCode => 
            HttpStatusCode.InternalServerError;
}