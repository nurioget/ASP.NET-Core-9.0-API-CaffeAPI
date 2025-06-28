using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaffeAPI.Aplication.Dtos.ResponseDtos
{
    public static class ErrorCodes
    {
        public const string notFound = "NOT_FOUND";
        public const string Unauthorized = "UNAUTHORIZED";
        public const string Exception = "EXCEPTION";
        public const string VlidationError = "VALIDATION_ERROR";
    }
}
