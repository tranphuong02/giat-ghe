namespace CL.Transverse.Enums
{
    public enum ResponseCode
    {
        /// <summary>
        ///  This code indicates that the server has received and is processing the request, but no response is available yet
        /// </summary>
        Processing = 102,

        /// <summary>
        /// Standard response for successful HTTP requests. The actual response will depend on the request method used.
        /// </summary>
        Success = 200,

        /// <summary>
        /// The server successfully processed the request, but is not returning any content.
        /// </summary>
        NoContent = 204,

        /// <summary>
        /// The server cannot or will not process the request due to something that is perceived to be a client error 
        /// (e.g., malformed request syntax, invalid request message framing, or deceptive request routing).
        /// </summary>
        BadRequest = 400,

        /// <summary>
        /// Similar to 403 Forbidden, but specifically for use when authentication is required and has failed or has not yet been provided. 
        /// The response must include a WWW-Authenticate header field containing a challenge applicable to the requested resource
        /// </summary>
        Unauthorized = 401,

        /// <summary>
        /// The request was a valid request, but the server is refusing to respond to it.
        /// Unlike a 401 Unauthorized response, authenticating will make no difference
        /// </summary>
        Forbidden = 403,

        /// <summary>
        /// The requested resource could not be found but may be available again in the future. Subsequent requests by the client are permissible
        /// </summary>
        NotFound = 404,

        /// <summary>
        /// The server timed out waiting for the request.
        /// According to HTTP specifications: The client did not produce a request within the time that the server was prepared to wait. 
        /// The client MAY repeat the request without modifications at any later time
        /// </summary>
        TimeOut = 408,

        /// <summary>
        /// Indicates that the request could not be processed because of conflict in the request, such as an edit conflict in the case of multiple updates.
        /// </summary>
        Conflict = 409,

        /// <summary>
        /// A generic error message, given when an unexpected condition was encountered and no more specific message is suitable.
        /// </summary>
        InternalError = 500,

        /// <summary>
        /// The server is currently unavailable (because it is overloaded or down for maintenance). Generally, this is a temporary state.
        /// </summary>
        ServiceUnavailable = 503,

        /// <summary>
        /// This status code is not specified in any RFC and is returned by certain services
        /// </summary>
        UnknownError = 520,

        /// <summary>
        /// This status code is fail to validate an object
        /// </summary>
        ValidateFail = 999
    }
}
