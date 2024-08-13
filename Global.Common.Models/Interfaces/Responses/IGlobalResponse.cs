
namespace Global.Common.Models
{
    /// <summary>
    /// Represents a response with status and message handling capabilities.
    /// </summary>
    public interface IGlobalResponse : IVerboseResponse<Dictionary<string, string>>
    {
        /// <summary>
        /// Gets the status of the response.
        /// </summary>
        ResponseStatus Status { get; }

        /// <summary>
        /// Get amount of messages
        /// </summary>
        int MessageCount { get; }

        /// <summary>
        /// Creates a shadow clone of the messages stored in the response.
        /// </summary>
        /// <returns>A shadow clone of the messages stored in the response.</returns>
        IDictionary<string, string> ShadowCloneMessages();

        /// <summary>
        /// Tries to retrieve a message associated with the specified <paramref name="messageKey"/>.
        /// </summary>
        /// <param name="messageKey">The key of the message to retrieve.</param>
        /// <param name="message">When this method returns, contains the message associated with the specified <paramref name="messageKey"/>, if the key was found; otherwise, null.</param>
        /// <returns>True if the message was successfully retrieved; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentException">Thrown if <paramref name="messageKey"/> is null or empty.</exception>
        bool TryGetMessage(string messageKey, out string? message);

        /// <summary>
        /// Adds a message to the response transactionally, attempting to add both the message and its stack trace if <paramref name="addCurrentStackTrace"/> is <c>true</c>.
        /// If either the message or the stack trace addition fails, no messages are added.
        /// If <paramref name="addCurrentStackTrace"/> is <c>false</c>, only the message is added.
        /// </summary>
        /// <param name="messageKey">The key associated with the message.</param>
        /// <param name="message">The message to add.</param>
        /// <param name="keyExistAction">The action to take if the <paramref name="messageKey"/> already exists in the messages of response (default is Rename).</param>
        /// <param name="addCurrentStackTrace">Determines whether to add the current stack trace of the message (default is <c>false</c>).</param>
        /// <returns>True if the messages were added successfully; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="messageKey"/> ir null or empty, or <paramref name="message"/> is null.</exception>
        bool AddMessageTransactionally(
            string messageKey,
            string message,
            KeyExistAction keyExistAction = KeyExistAction.Rename,
            bool addCurrentStackTrace = false);

        /// <summary>
        /// Try to add multiple messages to the response. Each item in <paramref name="items"/> will be attempted to be added using <see cref="AddMessageTransactionally"/>, 
        /// which will try to add them transactionally, attempting to add both the message and its stack trace if <paramref name="addCurrentStackTrace"/> is <c>true</c>.
        /// If either the message or the stack trace addition fails, no messages are added.
        /// If <paramref name="addCurrentStackTrace"/> is <c>false</c>, only the message is added.
        /// </summary>
        /// <param name="keyExistAction">The action to take when encountering an existing key.</param>
        /// <param name="addCurrentStackTrace">Determines whether to add the source stack trace of the message (default is <c>false</c>) for each item in <paramref name="items"/>.</param>
        /// <param name="items">The messages to add with their respective keys.</param>
        /// <returns>An array of boolean values indicating whether each message was successfully added, 
        /// where the index position corresponds to the add operation result of the item contained in the same index position in <paramref name="items"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="items"/> is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown if messageKey is null or empty, or if message is null for any item in <paramref name="items"/>.</exception>
        bool[] TryAddMessages(
            KeyExistAction keyExistAction = KeyExistAction.Rename,
            bool addCurrentStackTrace = false,
            params (string messageKey, string message)[] items);

        /// <summary>
        /// Adds a <paramref name="exception"/> message to the response transactionally, attempting to add both the <paramref name="exception"/> message and the <paramref name="exception"/> stack trace if <paramref name="addExceptionStackTrace"/> is <c>true</c>.
        /// If either the <paramref name="exception"/> message or the <paramref name="exception"/> stack trace addition fails, no messages are added.
        /// </summary>
        /// <typeparam name="TEx">The type of exception to be added.</typeparam>
        /// <param name="exception">The exception to be added.</param>
        /// <param name="addExceptionStackTrace">Determines whether to include the <paramref name="exception"/> stack trace (default is <c>true</c>).</param>
        /// <returns>True if the messages were added successfully; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="exception"/> is null.</exception>
        bool AddMessageTransactionally<TEx>(
            TEx exception,
            bool addExceptionStackTrace = true)
            where TEx : Exception;

        /// <summary>
        /// Adds messages from another <see cref="IGlobalResponse"/> instance.
        /// </summary>
        /// <param name="response">The other <see cref="IGlobalResponse"/> instance to add messages from.</param>
        /// <param name="messageKeyExistAction">The action to take if a key of any key-value pair in the messages of <paramref name="response"/> already exists in the messages of the current response.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="response"/> is null.</exception>
        void AddMessagesFrom(
            IGlobalResponse response,
            KeyExistAction messageKeyExistAction);
    }
}
