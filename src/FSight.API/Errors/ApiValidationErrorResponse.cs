using System.Collections;
using System.Collections.Generic;

namespace FSight.API.Errors
{
    public class ApiValidationErrorResponse : ApiResponse
    {
        public ApiValidationErrorResponse() : base(422)
        {
        }

        public IEnumerable<string> Errors { get; set; }
    }
}