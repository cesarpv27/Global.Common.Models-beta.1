
namespace Global.Common.Models
{
    /// <summary>
    /// Represents a response with status, <see cref="Exception"/>, value and message handling capabilities.
    /// The response has the type of <see cref="Exception"/> as the associated exception.
    /// </summary>
    /// <typeparam name="T">The type of value associated with the response.</typeparam>
    public interface IExGlobalValueResponse<T> : IGlobalValueResponse<T, Exception> { }
}
