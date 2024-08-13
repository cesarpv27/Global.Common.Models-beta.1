
namespace Global.Common.Models
{
    /// <summary>
    /// Factory for creating instances of <see cref="GlobalResponse"/> and <see cref="GlobalResponse{TEx}"/>.
    /// </summary>
    public static class GlobalResponseFactory
    {
        #region Methods for GlobalResponse

        /// <summary>
        /// Creates a new instance of <see cref="GlobalResponse"/> with the status set to <see cref="ResponseStatus.Success"/>.
        /// Creates a successful response.
        /// </summary>
        /// <returns>An instance of <see cref="GlobalResponse"/>.</returns>
        public static GlobalResponse CreateSuccessful()
        {
            return new GlobalResponse(ResponseStatus.Success);
        }

        /// <summary>
        /// Creates a new instance of <see cref="GlobalResponse"/> with the status set to <see cref="ResponseStatus.Warning"/>.
        /// </summary>
        /// <returns>An instance of <see cref="GlobalResponse"/></returns>
        public static GlobalResponse CreateWarning()
        {
            return new GlobalResponse(ResponseStatus.Warning);
        }

        /// <summary>
        /// Creates a new instance of <see cref="GlobalResponse"/> with the status set to <see cref="ResponseStatus.Failure"/>.
        /// </summary>
        /// <returns>An instance of <see cref="GlobalResponse"/>.</returns>
        public static GlobalResponse CreateFailure()
        {
            return new GlobalResponse(ResponseStatus.Failure);
        }

        #endregion

        #region Methods for GlobalResponse{TEx}

        /// <summary>
        /// Creates an instance of <see cref="GlobalResponse{TEx}"/> with the status of <see cref="ResponseStatus.Success"/>.
        /// </summary>
        /// <typeparam name="TEx">The type of exception.</typeparam>
        /// <returns>An instance of <see cref="GlobalResponse{TEx}"/>.</returns>
        public static GlobalResponse<TEx> CreateSuccessful<TEx>() where TEx : Exception
        {
            return new GlobalResponse<TEx>(ResponseStatus.Success);
        }

        /// <summary>
        /// Creates an instance of <see cref="GlobalResponse{TEx}"/> with the status set to <see cref="ResponseStatus.Warning"/>.
        /// </summary>
        /// <typeparam name="TEx">The type of exception.</typeparam>
        /// <returns>An instance of <see cref="GlobalResponse{TEx}"/>.</returns>
        public static GlobalResponse<TEx> CreateWarning<TEx>() where TEx : Exception
        {
            return new GlobalResponse<TEx>(ResponseStatus.Warning);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="GlobalResponse{TEx}"/> class with the status set to <see cref="ResponseStatus.Failure"/>.
        /// </summary>
        /// <typeparam name="TEx">The type of exception.</typeparam>
        /// <returns>An instance of <see cref="GlobalResponse{TEx}"/>.</returns>
        public static GlobalResponse<TEx> CreateFailure<TEx>() where TEx : Exception
        {
            return new GlobalResponse<TEx>(ResponseStatus.Failure);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="GlobalResponse{TEx}"/> class with the specified <paramref name="exception"/> 
        /// and the status set to <see cref="ResponseStatus.Failure"/>.
        /// </summary>
        /// <typeparam name="TEx">The type of exception.</typeparam>
        /// <param name="exception">The exception associated with the response.</param>
        /// <returns>An instance of <see cref="GlobalResponse{TEx}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="exception"/> is null.</exception>
        public static GlobalResponse<TEx> CreateFailure<TEx>(TEx exception) where TEx : Exception
        {
            return new GlobalResponse<TEx>(exception);
        }

        #region CreateFrom methods

        /// <summary>
        /// Creates a new instance of the <see cref="GlobalResponse{TEx}"/> class using the status and messages from the <paramref name="globalResponse"/> specified.
        /// The new instance contains the same status and messages of the <paramref name="globalResponse"/> specified, but the message keys may have been modified with suffix.
        /// </summary>
        /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
        /// <param name="globalResponse">The response from where the outgoing response will be generated.</param>
        /// <returns>An instance of <see cref="GlobalResponse{TEx}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="globalResponse"/> is null.</exception>
        public static GlobalResponse<TEx> CreateFrom<TEx>(IGlobalResponse globalResponse) where TEx : Exception
        {
            return new GlobalResponse<TEx>(globalResponse);
        }

        #endregion

        #region MapFromFailure methods

        /// <summary>
        /// Creates a new instance of the <see cref="GlobalResponse{TEx}"/> class from the data in <paramref name="globalValueResponse"/>. 
        /// The <paramref name="globalValueResponse"/> specified must contain the Status set to <see cref="ResponseStatus.Failure"/>. 
        /// The new instance contains the same status, exception and messages of the <paramref name="globalValueResponse"/>, 
        /// but the message keys may have been modified with suffix. 
        /// If the Status in <paramref name="globalValueResponse"/> is not <see cref="ResponseStatus.Failure"/>, 
        /// an <see cref="ArgumentException"/> will be thrown.
        /// </summary>
        /// <typeparam name="T">The type of value associated with the response.</typeparam>
        /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
        /// <param name="globalValueResponse">The response from where the outgoing response will be generated.</param>
        /// <returns>An instance of <see cref="GlobalResponse{TEx}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="globalValueResponse"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the Status in <paramref name="globalValueResponse"/> is not <see cref="ResponseStatus.Failure"/>.</exception>
        public static GlobalResponse<TEx> MapFromFailure<T, TEx>(IGlobalValueResponse<T, TEx> globalValueResponse) where TEx : Exception
        {
            AssertResponseHelper.AssertFailureNotNullOrThrow(globalValueResponse, nameof(globalValueResponse));

            GlobalResponse<TEx> response;
            if (globalValueResponse.HasException)
                response = CreateFailure(globalValueResponse.Exception!);
            else
                response = CreateFailure<TEx>();

            response.AddMessagesFrom(globalValueResponse, KeyExistAction.Rename);

            return response;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="GlobalResponse{TEx}"/> class from the data in <paramref name="exGlobalValueResponse"/>. 
        /// The <paramref name="exGlobalValueResponse"/> specified must contain the Status set to <see cref="ResponseStatus.Failure"/>. 
        /// The new instance contains the same status, exception and messages of the <paramref name="exGlobalValueResponse"/>, 
        /// but the message keys may have been modified with suffix. 
        /// If the Status in <paramref name="exGlobalValueResponse"/> is not <see cref="ResponseStatus.Failure"/>, 
        /// an <see cref="ArgumentException"/> will be thrown.
        /// </summary>
        /// <typeparam name="T">The type of value associated with the response.</typeparam>
        /// <param name="exGlobalValueResponse">The response from where the outgoing response will be generated.</param>
        /// <returns>An instance of <see cref="GlobalResponse{TEx}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="exGlobalValueResponse"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the Status in <paramref name="exGlobalValueResponse"/> is not <see cref="ResponseStatus.Failure"/>.</exception>
        public static GlobalResponse<Exception> MapFromFailure<T>(IExGlobalValueResponse<T> exGlobalValueResponse)
        {
            return MapFromFailure<T, Exception>(exGlobalValueResponse);
        }

        #endregion

        #endregion
    }
}
