namespace DotNet8Authentication.Results;

public interface IServiceResult : IResult
{
}

public interface IServiceResult<out T> : IServiceResult
{
    T? Data { get; }
}

// IServiceResult<out T>:
// This interface defines an additional property for the data (Data) returned with a generic T type.
// The out keyword enables covariance, allowing this interface to be used more flexibly.
    
// public class Animal { }
// public class Dog : Animal { }
// IServiceResult<Dog> dogResult = null;
// IServiceResult<Animal> animalResult = dogResult;

// The out keyword in C# makes a generic type parameter covariant, 
// allowing a more specific type (e.g., Dog) to be assigned to a more general type (e.g., Animal). 
// Without out, such assignments would result in a compilation error.

// However, using out requires that the type parameter T is only used as a return type 
// and not as a method parameter or for modifying the object. 
// This is because covariance ensures that the type is only used in a "read-only" context, 
// which prevents potential type safety issues when passing objects of derived types.