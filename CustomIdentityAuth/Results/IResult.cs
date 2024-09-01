namespace CustomIdentityAuth.Results;

public interface IResult
{
    bool Succeeded { get; }
    string? Message { get; }
    IEnumerable<string>? ErrorMessages { get; }
}