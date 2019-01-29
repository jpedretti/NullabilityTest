using System;

#nullable enable

namespace NullabilityTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var nullabilityTestClass = new NullabilityTestClass(-1);

            var a = nullabilityTestClass.ToStringNumber()
                 ?.Apply(i => Console.WriteLine($"the string is: {i}; Using Apply"))
                 .IfNull(() => Console.WriteLine("string was null using apply"));

            a.IfNull(() => Console.WriteLine("Executed IfNull only when using a variable"));

            nullabilityTestClass.ToStringNumber()
                ?.Run(i => Console.WriteLine($"the string is: {i}; Using Run"));

            nullabilityTestClass.ToDouble()
                ?.Apply(d => Console.WriteLine($"the double is: {d}; Using Apply"))
                .IfNull(() => Console.WriteLine("double was null using Apply"));

            nullabilityTestClass.ToDouble()
                ?.Run(d => Console.WriteLine($"the double is: {d}; Using Run"));
        }
    }

    public static class ClassExtensions
    {
        public static T Apply<T>(this T instance, Action<T> action) where T : class
        {
            action.Invoke(instance);
            return instance;
        }

        public static void Run<T>(this T instance, Action<T> action) where T : class => action.Invoke(instance);
    }

    public static class ObjectExtensions
    {
        public static T IfNull<T>(this T instance, Action action)
        {
            if (instance == null)
            {
                action.Invoke();
            }

            return instance;
        }
    }

    public static class StructExtensions
    {
        public static T Apply<T>(this T instance, Action<T> action) where T : struct
        {
            action.Invoke(instance);
            return instance;
        }

        public static void Run<T>(this T instance, Action<T> action) where T : struct => action.Invoke(instance);
    }

    public class NullabilityTestClass
    {
        public int Number { get; private set; }

        public NullabilityTestClass(int number)
        {
            Number = number;
        }

        public string? ToStringNumber() => Number > 0 ? Number.ToString() : null;

        public double? ToDouble()
        {
            double? result = null;
            if (Number > 0)
            {
                result = Convert.ToDouble(Number);
            }
            return result;
        }
    }
}
