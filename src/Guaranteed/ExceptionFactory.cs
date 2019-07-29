using System;

namespace Guaranteed
{
    internal sealed class ExceptionFactory
    {
        internal Exception ArgumentException(string defaultMessage, string paramName)
        {
            return new ArgumentException(defaultMessage, paramName);
        }

        internal Exception ArgumentNullException(string defaultMessage, string paramName)
        {
            return new ArgumentNullException(paramName, defaultMessage);
        }

        internal Exception ArgumentOutOfRangeException<TValue>(string defaultMessage, string paramName, TValue value)
        {
            return new ArgumentOutOfRangeException(paramName, value, defaultMessage);
        }

        internal Exception GeneralException(string message = null)
        {
            return new Exception(message);
        }
    }
}
