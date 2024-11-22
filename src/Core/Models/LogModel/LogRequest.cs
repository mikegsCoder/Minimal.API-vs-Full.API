namespace Core.Models.LogModel;

public class LogRequest
{
    public required string Message { get; init; }
    public required int Severity { get; init; }
}