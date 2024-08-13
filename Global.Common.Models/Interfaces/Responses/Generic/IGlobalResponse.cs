
namespace Global.Common.Models
{
    /// <summary>
    /// Represents a response with status, exception and message handling capabilities.
    /// </summary>
    /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
    public interface IGlobalResponse<TEx> : IGlobalResponse where TEx : Exception
    {
        /// <summary>
        /// Gets a value indicating whether the reponse has an exception associated.
        /// </summary>
        bool HasException { get; }

        /// <summary>
        /// Gets the exception associated with the response.
        /// </summary>
        TEx? Exception { get; }
    }
}
