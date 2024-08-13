
namespace Global.Common.Models
{
    /// <summary>
    /// Represents a response to provide detailed information.
    /// </summary>
    /// <typeparam name="T">The type derived from <see cref="IDictionary{TKey, TValue}"/> which will contain the verbose response information.</typeparam>
    public interface IVerboseResponse<T> where T : IDictionary<string, string>
    {
        /// <summary>
        /// Builds detailed information of the response as an <see cref="IDictionary{TKey, TValue}"/> of string key-value pairs.
        /// </summary>
        /// <returns>An <typeparamref name="T"/> containing verbose response information.</returns>
         T BuildVerbose();
    }
}
