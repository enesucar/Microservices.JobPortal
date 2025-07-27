namespace CareerWay.Shared.AspNetCore.Logging.Models;

public enum LoggingLevel : int
{
    None,
    Informational = 100,
    Successful = 200,
    Redirection = 300,
    ClientError = 400,
    ServerError = 500
}
