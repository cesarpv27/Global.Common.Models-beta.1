
namespace Global.Common.Models.Helpers
{
    /// <summary>
    /// Helper class for asserting global responses.
    /// </summary>
    public static class AssertResponseHelper
    {
        /// <summary>
        /// Asserts that the specified <paramref name="response"/> is not null and contains the status set to <see cref="ResponseStatus.Failure"/>.
        /// </summary>
        /// <param name="response">The response instance to assert.</param>
        /// <param name="responseParamName">The name of the response parameter.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="response"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the Status in <paramref name="response"/> is not <see cref="ResponseStatus.Failure"/>.</exception>
        public static void AssertFailureNotNullOrThrow(IGlobalResponse response, string responseParamName)
        {
            AssertHelper.AssertNotNullOrThrow(response, responseParamName);

            if (response.Status != ResponseStatus.Failure)
                throw new ArgumentException(GlobalResponseConstants.ResponseStatusIsNotFailure(responseParamName));
        }

        /// <summary>
        /// Asserts that the specified <paramref name="response"/> is not null and contains the status set to <see cref="ResponseStatus.Failure"/>.
        /// </summary>
        /// <typeparam name="TEx">The type of exception associated with the response.</typeparam>
        /// <param name="response">The response instance to assert.</param>
        /// <param name="responseParamName">The name of the response parameter.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="response"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the Status in <paramref name="response"/> is not <see cref="ResponseStatus.Failure"/>.</exception>
        public static void AssertFailureNotNullOrThrow<TEx>(IGlobalResponse<TEx> response, string responseParamName)
            where TEx : Exception
        {
            AssertFailureNotNullOrThrow((IGlobalResponse)response, responseParamName);
        }
    }
}
