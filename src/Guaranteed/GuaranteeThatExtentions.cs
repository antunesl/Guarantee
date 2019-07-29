using System;
using System.Collections.Generic;
using System.Linq;

namespace Guaranteed
{
    public static class GuaranteeThatExtentions
    {

        public static void IsNotNull(this IGuaranteeThat guarantee, object input, string parameterName = null)
        {
            if (null == input)
            {
                throw Guarantee.ExceptionFactory.ArgumentNullException("", parameterName);
            }
        }

        public static void IsNotNullOrEmpty(this IGuaranteeThat guarantee, string input, string parameterName = null)
        {
            guarantee.IsNotNull(input);

            if (string.IsNullOrEmpty(input))
            {
                throw Guarantee.ExceptionFactory.ArgumentNullException("", parameterName);
            }

        }
    }


    public static class GuaranteeThatBoolExtensions
    {
        public static void IsTrue(this IGuaranteeThat guarantee, bool input, string parameterName = null)
        {
            if (!input)
                throw Guarantee.ExceptionFactory.ArgumentException("", parameterName);
        }

        public static void IsFalse(this IGuaranteeThat guarantee, bool input, string parameterName = null)
        {
            if (input)
                throw Guarantee.ExceptionFactory.ArgumentException("", parameterName);
        }
    }



    public static class GuaranteeThatTypeExtensions
    {
        public static void IsOfType(this IGuaranteeThat guarantee, object input, Type expectedType, string parameterName = null)
        {
            Guarantee.That.IsNotNull(input, parameterName);
            Guarantee.That.IsNotNull(expectedType, nameof(expectedType));

            if (input.GetType() != expectedType)
                throw Guarantee.ExceptionFactory.ArgumentException(
                    "",
                    parameterName);
        }

        public static void IsOfType<TExpected>(this IGuaranteeThat guarantee, object input, string parameterName = null)
        {
            Guarantee.That.IsNotNull(input, parameterName);

            if (input.GetType() != typeof(TExpected))
                throw Guarantee.ExceptionFactory.ArgumentException(
                    "",
                    parameterName);
        }
    }


    public static class GuaranteeThatEnumerableExtensions
    {
        public static IGuaranteeThat GuaranteeThat<T>(this IEnumerable<T> collection)
        {
            return Guarantee.That;
        }

        public static IEnumerable<T> HasItems<T>(this IGuaranteeThat guarantee, IEnumerable<T> value, string parameterName = null)
        {
            Guarantee.That.IsNotNull(value, parameterName);

            if (value.Count() == 0)
                throw Guarantee.ExceptionFactory.ArgumentException(
                    "",
                    parameterName);

            return value;
        }


        public static IEnumerable<T> SizeIs<T>(this IGuaranteeThat guarantee, IEnumerable<T> value, int expected, string parameterName = null)
        {
            Guarantee.That.IsNotNull(value, parameterName);

            var count = value.Count();

            if (count != expected)
                throw Guarantee.ExceptionFactory.ArgumentException(
                    "",
                    parameterName);

            return value;
        }

        public static IEnumerable<T> SizeIsLt<T>(this IGuaranteeThat guarantee, IEnumerable<T> value, int expected, string parameterName = null)
        {
            Guarantee.That.IsNotNull(value, parameterName);

            var count = value.Count();

            if (count >= expected)
                throw Guarantee.ExceptionFactory.ArgumentException(
                    "",
                    parameterName);

            return value;
        }

        public static IEnumerable<T> SizeIsLte<T>(this IGuaranteeThat guarantee, IEnumerable<T> value, int expected, string parameterName = null)
        {
            Guarantee.That.IsNotNull(value, parameterName);

            var count = value.Count();

            if (count > expected)
                throw Guarantee.ExceptionFactory.ArgumentException(
                    "",
                    parameterName);

            return value;
        }

        public static void SizeIsGt<T>(this IGuaranteeThat guarantee, IEnumerable<T> value, int expected, string parameterName = null)
        {
            Guarantee.That.IsNotNull(value, parameterName);

            var count = value.Count();

            if (count <= expected)
                throw Guarantee.ExceptionFactory.ArgumentException(
                    "",
                    parameterName);
        }

        public static void SizeIsGte<T>(this IGuaranteeThat guarantee, IEnumerable<T> value, int expected, string parameterName = null)
        {
            Guarantee.That.IsNotNull(value, parameterName);

            var count = value.Count();

            if (count < expected)
                throw Guarantee.ExceptionFactory.ArgumentException(
                    "",
                    parameterName);
        }
    }
}
