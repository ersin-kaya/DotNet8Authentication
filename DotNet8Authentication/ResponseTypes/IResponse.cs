namespace DotNet8Authentication.ResponseTypes;

public interface IResponse
{
    bool Succeeded { get; }
    string Message { get; }
}