

namespace Global.Common.Models
{
    /// <summary>
    /// Represents a response with status, exception, value and message handling capabilities.
    /// </summary>
    /// <typeparam name="T">The type of value associated with the response.</typeparam>
    /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
    public interface IGlobalValueResponse<T, TEx> : IGlobalResponse<TEx> where TEx : Exception
    {
        /// <summary>
        /// Gets a value indicating whether the response has an value associated.
        /// </summary>
        bool HasValue { get; }

        /// <summary>
        /// Gets the value associated with the response.
        /// </summary>
        T? Value { get; }
    }
}
