
namespace Global.Common.Models
{
    /// <summary>
    /// Factory class for creating default instances of <see cref="IDictionary{TKey, TValue}"/>
    /// </summary>
    internal static class MessagesFactory
    {
        /// <summary>
        /// Creates a new instance of <see cref="Dictionary{TKey, TValue}"/> for messages.
        /// </summary>
        /// <returns>A new instance of <see cref="Dictionary{TKey, TValue}"/> for messages.</returns>
        public static Dictionary<string, string> Create()
        {
            return new Dictionary<string, string>(GlobalResponseConstants.DefaultMessageAmount);
        }
    }
}
