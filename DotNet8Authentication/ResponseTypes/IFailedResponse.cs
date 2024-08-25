using System.Collections;

namespace DotNet8Authentication.ResponseTypes;

public interface IFailedResponse : IResponse
{
    IEnumerable<string> Errors { get; }
}

public interface IFailedResponse<TError> : IResponse
{
    IEnumerable<TError> Errors { get; }
}