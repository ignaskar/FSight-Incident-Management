namespace FSight.API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
        
        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "Bad Request has been made",
                401 => "You are not authorized to view this resource",
                403 => "You do not have sufficient privileges to view this resource",
                404 => "Requested resource was not found",
                422 => "One or more validation errors occured",
                _ => null
            };
        }
    }
}