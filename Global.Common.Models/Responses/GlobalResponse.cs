
namespace Global.Common.Models
{
    /// <inheritdoc cref="IGlobalResponse"/>
    public class GlobalResponse : IGlobalResponse
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalResponse"/> class.
        /// </summary>
        protected GlobalResponse()
        {
            Messages = MessagesFactory.Create();
            Messages.Add(GlobalResponseConstants.ResponseStackTraceKey, Environment.StackTrace);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalResponse"/> class with the specified <paramref name="status"/>.
        /// </summary>
        /// <param name="status">The status of the response.</param>
        public GlobalResponse(ResponseStatus status) : this()
        {
            Status = status;
        }

        #endregion

        #region Properties

        /// <inheritdoc/>
        public virtual ResponseStatus Status { get; protected set; }

        /// <summary>
        /// Get or set the messages
        /// </summary>
        protected IDictionary<string, string> Messages { get; set; }

        /// <inheritdoc/>
        public virtual int MessageCount { get { return Messages.Count;  } }

        #endregion

        #region IVerboseResponse implementation

        /// <inheritdoc/>
        public virtual Dictionary<string, string> BuildVerbose()
        {
            var verboseResponse = Messages.ShadowClone();
            verboseResponse.AddOrRenameKey(nameof(Status), 
                $"{typeof(ResponseStatus).FullName ?? GlobalResponseConstants.DefaultTypeFullNameUndefinedMessage.WrapInSingleQuotationMarks()}.{Status}");

            return verboseResponse;
        }

        #endregion

        #region Public methods

        /// <inheritdoc/>
        public virtual bool TryGetMessage(string messageKey, out string? message)
        {
            AssertHelper.AssertNotNullNotEmptyOrThrow(messageKey, nameof(messageKey));

            return Messages.TryGetValue(messageKey, out message);
        }

        /// <inheritdoc/>
        public virtual IDictionary<string, string> ShadowCloneMessages()
        {
            return Messages.ShadowClone();
        }

        /// <inheritdoc/>
        public virtual bool AddMessageTransactionally(
            string messageKey,
            string message,
            KeyExistAction keyExistAction = KeyExistAction.Rename,
            bool addCurrentStackTrace = false)
        {
            AssertHelper.AssertNotNullNotEmptyOrThrow(messageKey, nameof(messageKey));
            AssertHelper.AssertNotNullOrThrow(message, nameof(message));

            if (addCurrentStackTrace)
            {
                var currentStackTraceKey = $"{messageKey}_{GlobalResponseConstants.DefaultStackTraceKeySuffix}";
                return AddMessageAndStackTraceTransactionally(messageKey, message, currentStackTraceKey, Environment.StackTrace, keyExistAction);
            }

            return AddMessage(messageKey, message, out _, keyExistAction);
        }

        /// <inheritdoc/>
        public virtual bool[] TryAddMessages(
            KeyExistAction keyExistAction = KeyExistAction.Rename,
            bool addCurrentStackTrace = false,
            params (string messageKey, string message)[] items)
        {
            AssertHelper.AssertNotNullOrThrow(items, nameof(items));

            var results = new bool[items.Length];

            for (int i = 0; i < items.Length; i++)
                results[i] = AddMessageTransactionally(items[i].messageKey, items[i].message, keyExistAction, addCurrentStackTrace);

            return results;
        }

        /// <inheritdoc/>
        public virtual bool AddMessageTransactionally<TEx>(
            TEx exception,
            bool addExceptionStackTrace = true)
            where TEx : Exception
        {
            AssertHelper.AssertNotNullOrThrow(exception, nameof(exception));

            var exceptionMessageKey = ResponseHelper.CreateDefaultExceptionMessageKey(exception);
            var exceptionMessage = exception.GetAllMessagesFromExceptionHierarchy();
            var keyExistAction = KeyExistAction.Rename;

            if (addExceptionStackTrace)
                return AddMessageAndStackTraceTransactionally(
                    exceptionMessageKey,
                    exceptionMessage,
                    ResponseHelper.CreateDefaultExceptionStackTraceKey(exception),
                    exception.GetAllStackTracesFromExceptionHierarchy() ?? GlobalResponseConstants.DefaultExceptionStackTraceUndefinedMessage,
                    keyExistAction);

            return AddMessage(exceptionMessageKey, exceptionMessage, out _, keyExistAction);
        }

        /// <inheritdoc/>
        public virtual void AddMessagesFrom(
            IGlobalResponse response,
            KeyExistAction messageKeyExistAction)
        {
            AssertHelper.AssertNotNullOrThrow(response, nameof(response));

            AddMessages(response.ShadowCloneMessages(), messageKeyExistAction);
        }

        #endregion

        #region Protected methods

        /// <summary>
        /// Adds a message to the response transactionally, attempting to add both the message and its stack trace.
        /// If either the message or the stack trace addition fails, no messages are added.
        /// </summary>
        /// <param name="keyForMessage">The key associated with the message.</param>
        /// <param name="keyForStackTrace">The key associated with the stack traces.</param>
        /// <param name="message">The message to add.</param>
        /// <param name="stackTrace">The sourceStackTrace to add.</param>
        /// <param name="keyExistAction">The action to take if the <paramref name="keyForMessage"/> already exists in the messages of the current response.</param>
        /// <returns>True if the message and stack trace were added successfully; otherwise, false.</returns>
        /// <exception cref="ArgumentException">Thrown if <paramref name="keyForMessage"/> or <paramref name="keyForStackTrace"/> are null or empty.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="message"/>, or if <paramref name="stackTrace"/> are null.</exception>
        protected virtual bool AddMessageAndStackTraceTransactionally(
            string keyForMessage,
            string message,
            string keyForStackTrace,
            string stackTrace,
            KeyExistAction keyExistAction)
        {
            AssertHelper.AssertNotNullNotEmptyOrThrow(keyForMessage, nameof(keyForMessage));
            AssertHelper.AssertNotNullNotEmptyOrThrow(keyForStackTrace, nameof(keyForStackTrace));
            AssertHelper.AssertNotNullOrThrow(message, nameof(message));
            AssertHelper.AssertNotNullOrThrow(stackTrace, nameof(stackTrace));

            if (!AddMessage(keyForMessage, message, out _, keyExistAction))
                return false;
            if (!AddMessage(keyForStackTrace, stackTrace, out _, KeyExistAction.Rename))
            {
                Messages.Remove(keyForMessage);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Adds a message to the response.
        /// </summary>
        /// <param name="messageKey">The key of the message.</param>
        /// <param name="message">The message to add.</param>
        /// <param name="addedKey">The key of the added message.</param>
        /// <param name="keyExistAction">The action to take if the <paramref name="messageKey"/> already exists in the messages of the current response.</param>
        /// <returns>True if the message is successfully added; otherwise, false.</returns>
        /// <exception cref="ArgumentException">Thrown if <paramref name="messageKey"/> is null or empty.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="message"/> is null.</exception>
        protected virtual bool AddMessage(
            string messageKey,
            string message,
            out string? addedKey,
            KeyExistAction keyExistAction)
        {
            AssertHelper.AssertNotNullNotEmptyOrThrow(messageKey, nameof(messageKey));
            AssertHelper.AssertNotNullOrThrow(message, nameof(message));

            if(message.Equals(GlobalResponseConstants.ResponseStackTraceKey))
                keyExistAction = KeyExistAction.Update;

            return Messages.Add(messageKey, message, out addedKey, keyExistAction);
        }

        /// <summary>
        /// Adds messages to the response.
        /// </summary>
        /// <param name="messages">The messages to add.</param>
        /// <param name="messageKeyExistAction">The action to take if a key of any key-value pair in <paramref name="messages"/> already exists in the messages of the current response.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="messages"/> is null.</exception>
        protected virtual void AddMessages(
            IDictionary<string, string> messages,
            KeyExistAction messageKeyExistAction)
        {
            AssertHelper.AssertNotNullOrThrow(messages, nameof(messages));

            if (messages.TryGetValue(GlobalResponseConstants.ResponseStackTraceKey, out _)
                && Messages.TryGetValue(GlobalResponseConstants.ResponseStackTraceKey, out _))
                Messages.Remove(GlobalResponseConstants.ResponseStackTraceKey);

            Messages.TryAddRange(messages, messageKeyExistAction);
        }

        #endregion
    }
}
