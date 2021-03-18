using System;

namespace LiteNetLib
{
    public static class TimeSpanHelper
    {
        // This class is here as TimeSpan does not support the operator * until .NET Standard 2.1
        public static TimeSpan Multiply(this TimeSpan time, double factor)
        {
            return new TimeSpan((long)(time.Ticks * factor));
        }

        // This class is here as TimeSpan does not support the operator / until .NET Standard 2.1
        public static TimeSpan Divide(this TimeSpan time, double divisor)
        {
            return new TimeSpan((long)(time.Ticks / divisor));
        }
    }
}
