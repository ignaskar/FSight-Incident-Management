using System;

namespace FSight.API.Exceptions
{
    public class HttpAccessorNullUserException : Exception
    {
        public HttpAccessorNullUserException(string message) : base(message)
        {
            
        }
    }
}