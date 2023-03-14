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

public class DraftNotFound : DraftException
{
    public DraftNotFound()
        : base("unable to find specified draft.")
    {  }

    public override HttpStatusCode StatusCode => 
            HttpStatusCode.NotFound;
}

public class TeamNotFound : DraftException
{
    public TeamNotFound()
        : base("unable to find specified team.")
    {  }

    public override HttpStatusCode StatusCode => 
            HttpStatusCode.NotFound;
}

public class PlayerNotFound : DraftException
{
    public PlayerNotFound()
        : base("unable to find specified player.")
    {  }

    public override HttpStatusCode StatusCode => 
            HttpStatusCode.NotFound;
}

public class GeneralFailure : DraftException
{
    public GeneralFailure()
        : base("Request Failed due to internal errors.")
    {  }

    public override HttpStatusCode StatusCode => 
            HttpStatusCode.InternalServerError;
}