
namespace Global.Common.Models
{
    /// <inheritdoc cref="IGlobalResponse{TEx}"/>
    public class GlobalResponse<TEx> : GlobalResponse, IGlobalResponse<TEx> where TEx : Exception
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalResponse{TEx}"/> class.
        /// </summary>
        protected GlobalResponse() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalResponse{TEx}"/>  class with the specified <paramref name="status"/>.
        /// </summary>
        /// <param name="status">The status of the response.</param>
        public GlobalResponse(ResponseStatus status) : base(status) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalResponse{TEx}"/> class with the specified <paramref name="exception"/> 
        /// and the status set to <see cref="ResponseStatus.Failure"/>.
        /// </summary>
        /// <param name="exception">The exception associated with the response.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="exception"/> is null.</exception>
        public GlobalResponse(TEx exception) : this(ResponseStatus.Failure)
        {
            AssertHelper.AssertNotNullOrThrow(exception, nameof(exception));

            Exception = exception;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalResponse{TEx}"/> class using the status and messages from the <paramref name="globalResponse"/> specified.
        /// The new instance contains the same status and messages of the <paramref name="globalResponse"/> specified, 
        /// but the message keys may have been modified with suffix. 
        /// </summary>
        /// <param name="globalResponse">The response from where the new instance will be generated.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="globalResponse"/> is null.</exception>
        protected internal GlobalResponse(IGlobalResponse globalResponse) : this()
        {
            AssertHelper.AssertNotNullOrThrow(globalResponse, nameof(globalResponse));

            Status = globalResponse.Status;
            AddMessagesFrom(globalResponse, KeyExistAction.Rename);
        }

        #endregion

        #region Properties

        /// <inheritdoc/>
        public bool HasException => Exception != default;

        /// <inheritdoc/>
        public virtual TEx? Exception { get; protected set; }

        #endregion

        #region IVerboseResponse implementation

        /// <inheritdoc/>
        public override Dictionary<string, string> BuildVerbose()
        {
            var verboseResponse = base.BuildVerbose();
            verboseResponse.AddOrRenameKey(nameof(HasException), HasException.ToString());
            if (HasException)
            {
                verboseResponse.AddOrRenameKey(ResponseHelper.CreateDefaultExceptionMessageKey(Exception!),
                    Exception!.GetAllMessagesFromExceptionHierarchy());
                verboseResponse.AddOrRenameKey(ResponseHelper.CreateDefaultExceptionStackTraceKey(Exception!),
                    Exception!.GetAllStackTracesFromExceptionHierarchy() ?? GlobalResponseConstants.DefaultExceptionStackTraceUndefinedMessage);
            }

            return verboseResponse;
        }

        #endregion
    }
}
