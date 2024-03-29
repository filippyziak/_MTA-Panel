using System;
using MTA.Core.Common.Helpers;

namespace MTA.Core.Application.Exceptions
{
    public class DuplicateException : Exception
    {
        public string ErrorCode { get; }

        public DuplicateException(string message, string errorCode = ErrorCodes.DuplicateExists) : base(message)
            => (ErrorCode) = (errorCode);
    }
}