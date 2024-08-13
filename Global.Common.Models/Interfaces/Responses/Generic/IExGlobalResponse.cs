
namespace Global.Common.Models
{
    /// <summary>
    /// Represents a sucessful response with status, <see cref="Exception"/> and message handling capabilities.
    /// The response has the type of <see cref="Exception"/> as the associated exception.
    /// </summary>
    public interface IExGlobalResponse : IGlobalResponse<Exception> { }
}
