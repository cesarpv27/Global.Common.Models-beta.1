
namespace Global.Common.Models
{
    /// <inheritdoc cref="IGlobalValueResponse{T, TEx}"/>
    public class GlobalValueResponse<T, TEx> : GlobalResponse<TEx>, IGlobalValueResponse<T, TEx> where TEx : Exception
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalValueResponse{T, TEx}"/> class.
        /// </summary>
        protected GlobalValueResponse() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalValueResponse{T, TEx}"/> class with the specified <paramref name="status"/>.
        /// </summary>
        protected GlobalValueResponse(ResponseStatus status) : base(status) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalValueResponse{T, TEx}"/> class with the specified <paramref name="exception"/> 
        /// and the status set to <see cref="ResponseStatus.Failure"/>.
        /// </summary>
        /// <param name="exception">The exception associated with the response.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="exception"/> is null.</exception>
        public GlobalValueResponse(TEx exception) : base(exception) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalValueResponse{T, TEx}"/> class with the specified <paramref name="status"/> 
        /// and <paramref name="value"/>.
        /// If the <paramref name="status"/> is <see cref="ResponseStatus.Success"/>, 
        /// then the <paramref name="value"/> must be specified.
        /// If the <paramref name="status"/> is <see cref="ResponseStatus.Success"/> 
        /// and the <paramref name="value"/> was not specified, an <see cref="ArgumentNullException"/> will be thrown.
        /// If the <paramref name="status"/> is <see cref="ResponseStatus.Failure"/>, 
        /// then the <paramref name="value"/> must be null.
        /// If the <paramref name="status"/> is <see cref="ResponseStatus.Failure"/> and the <paramref name="value"/> was specified, 
        /// an <see cref="ArgumentException"/> will be thrown.
        /// </summary>
        /// <param name="status">The status of the response.</param>
        /// <param name="value">The value associated with the response.</param>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="status"/> is <see cref="ResponseStatus.Success"/> 
        /// and the <paramref name="value"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the <paramref name="status"/> is <see cref="ResponseStatus.Failure"/> 
        /// and the <paramref name="value"/> was specified.</exception>
        public GlobalValueResponse(ResponseStatus status, T? value) : this(status)
        {
            if (status == ResponseStatus.Success)
                AssertHelper.AssertNotNullOrThrow(value, nameof(value));
            else
            if (status == ResponseStatus.Failure && !IsDefaultValue(value))
                throw new ArgumentException(GlobalResponseConstants.ResponseStatusFailureAndValueSpecified(nameof(status), nameof(value)));

            Value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalValueResponse{T, TEx}"/> class from the data in <paramref name="globalResponse"/>. 
        /// The <paramref name="globalResponse"/> specified must contain the Status set to <see cref="ResponseStatus.Failure"/>. 
        /// The new instance contains the same status and messages of the <paramref name="globalResponse"/>, 
        /// but the message keys may have been modified with suffix. 
        /// If the Status in <paramref name="globalResponse"/> is not <see cref="ResponseStatus.Failure"/>, 
        /// an <see cref="ArgumentException"/> will be thrown.
        /// </summary>
        /// <param name="globalResponse">The response from where the instance will be generated.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="globalResponse"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the Status in <paramref name="globalResponse"/> is not <see cref="ResponseStatus.Failure"/>.</exception>
        protected internal GlobalValueResponse(IGlobalResponse globalResponse) : this(ResponseStatus.Failure, default)
        {
            AssertResponseHelper.AssertFailureNotNullOrThrow(globalResponse, nameof(globalResponse));

            AddMessagesFrom(globalResponse, KeyExistAction.Rename);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalValueResponse{T, TEx}"/> class from the data in <paramref name="globalResponse"/>. 
        /// The <paramref name="globalResponse"/> specified must contain the Status set to <see cref="ResponseStatus.Failure"/>. 
        /// The new instance contains the same status, exception and messages of the <paramref name="globalResponse"/>, 
        /// but the message keys may have been modified with suffix. 
        /// If the Status in <paramref name="globalResponse"/> is not <see cref="ResponseStatus.Failure"/>, 
        /// an <see cref="ArgumentException"/> will be thrown.
        /// </summary>
        /// <param name="globalResponse">The response from where the new instance will be generated.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="globalResponse"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the Status in <paramref name="globalResponse"/> is not <see cref="ResponseStatus.Failure"/>.</exception>
        protected internal GlobalValueResponse(IGlobalResponse<TEx> globalResponse) : this(ResponseStatus.Failure, default)
        {
            AssertResponseHelper.AssertFailureNotNullOrThrow(globalResponse, nameof(globalResponse));

            Exception = globalResponse.Exception;

            AddMessagesFrom(globalResponse, KeyExistAction.Rename);
        }

        #endregion

        #region Public properties

        /// <inheritdoc/>
        public bool HasValue => Value != null;

        /// <inheritdoc/>
        public virtual T? Value { get; protected set; }

        #endregion

        #region IVerboseResponse implementation

        /// <inheritdoc/>
        public override Dictionary<string, string> BuildVerbose()
        {
            var verboseResponse = base.BuildVerbose();
            verboseResponse.AddOrRenameKey(nameof(HasValue), HasValue.ToString());
            if (HasValue)
            {
                verboseResponse.AddOrRenameKey($"{nameof(Value)}{GlobalResponseConstants.DefaultTypeMessageKeySuffix}", 
                    Value!.GetType().FullName ?? GlobalResponseConstants.DefaultTypeFullNameUndefinedMessage);
                verboseResponse.AddOrRenameKey($"{nameof(Value)}", Value.ToString() ?? GlobalResponseConstants.DefaultValueToStringUndefinedMessage);
            }

            return verboseResponse;
        }

        #endregion

        #region Protected methods

        /// <summary>
        /// Determines whether the specified value is the default value for its type.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns><c>true</c> if the value is the default value for its type; otherwise, <c>false</c>.</returns>
        protected bool IsDefaultValue(T? value)
        {
            if (value == null)
                return true;

            return Equals(value, default(T));
        }

        #endregion
    }
}
