using System;

namespace Guaranteed
{
    public class Guarantee : IGuaranteeThat
    {
        internal static readonly ExceptionFactory ExceptionFactory = new ExceptionFactory();

        public static IGuaranteeThat That { get; } = new Guarantee();

        public static IGuaranteeThat This(Func<bool> predicate)
        {
            if (!predicate())
            {
                throw ExceptionFactory.GeneralException();
            }
            return new Guarantee();
        }

        public static IGuaranteeThat This<T>(Func<T, bool> predicate, T parameter)
        {
            if (!predicate(parameter))
            {
                throw ExceptionFactory.GeneralException();
            }
            return new Guarantee();
        }


        private Guarantee()
        {

        }
    }
}
