using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Sharenv.Application.Validation
{
    /// <summary>
    /// Provides common argument validation methods.
    /// </summary>
    public static class ArgumentValidation
    {
        /// <summary>
        /// Throws an <see cref="ArgumentNullException"/> if the argument is null.
        /// </summary>
        /// <typeparam name="T">The type of the argument.</typeparam>
        /// <param name="argument">The argument to validate.</param>
        /// <param name="paramName">The name of the parameter. This is automatically set by the compiler.</param>
        /// <returns>The original argument if it is not null.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="argument"/> is null.</exception>
        [return: NotNull]
        public static T ThrowIfNull<T>([NotNull] T argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null) where T : class
        {
            if (argument is null)
            {
                throw new ArgumentNullException(paramName);
            }
            return argument;
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the string argument is null or empty.
        /// </summary>
        /// <param name="argument">The string argument to validate.</param>
        /// <param name="paramName">The name of the parameter. This is automatically set by the compiler.</param>
        /// <returns>The original argument if it is not null or empty.</returns>
        /// <exception cref="ArgumentException">Thrown if <paramref name="argument"/> is null or an empty string.</exception>
        [return: NotNull]
        public static string ThrowIfNullOrEmpty([NotNull] string? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
        {
            if (string.IsNullOrEmpty(argument))
            {
                throw new ArgumentException("Value cannot be null or empty.", paramName);
            }
            return argument;
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the string argument is null, empty, or consists only of whitespace characters.
        /// </summary>
        /// <param name="argument">The string argument to validate.</param>
        /// <param name="paramName">The name of the parameter. Automatically set by the compiler.</param>
        /// <returns>The original argument if it is not null, empty, or whitespace.</returns>
        /// <exception cref="ArgumentException">Thrown if <paramref name="argument"/> is null, empty, or whitespace.</exception>
        [return: NotNull]
        public static string ThrowIfNullOrWhiteSpace([NotNull] string? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
        {
            if (string.IsNullOrWhiteSpace(argument))
            {
                throw new ArgumentException("Value cannot be null, empty, or consist only of whitespace.", paramName);
            }
            return argument;
        }

        /// <summary>
        /// Throws an <see cref="ArgumentException"/> if the collection argument is null or contains no elements.
        /// </summary>
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// <param name="argument">The collection argument to validate.</param>
        /// <param name="paramName">The name of the parameter. Automatically set by the compiler.</param>
        /// <returns>The original argument if it is not null and not empty.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="argument"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="argument"/> contains no elements.</exception>
        [return: NotNull]
        public static ICollection<T> ThrowIfNullOrEmpty<T>([NotNull] ICollection<T>? argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
        {
            ThrowIfNull(argument!, paramName);

            if (argument.Count == 0)
            {
                throw new ArgumentException("Collection cannot be empty.", paramName);
            }
            return argument;
        }

        /// <summary>
        /// Throws an <see cref="ArgumentOutOfRangeException"/> if the integer argument is less than a specified minimum value.
        /// </summary>
        /// <param name="argument">The integer argument to validate.</param>
        /// <param name="min">The minimum allowable value.</param>
        /// <param name="paramName">The name of the parameter. This is automatically set by the compiler.</param>
        /// <returns>The original argument if it meets the validation criteria.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="argument"/> is less than <paramref name="min"/>.</exception>
        public static int ThrowIfLessThan(int argument, int min, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
        {
            if (argument < min)
            {
                throw new ArgumentOutOfRangeException(paramName, argument, $"Value cannot be less than {min}.");
            }
            return argument;
        }

        /// <summary>
        ///  Throws an <see cref="ArgumentOutOfRangeException"/> if the integer argument is greater than zero.
        /// </summary>
        /// <param name="argument"></param>
        /// <param name="paramName"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="argument"/> is greater than zero.</exception>
        public static int ThrowIfPositive(int argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
        {
            if (argument > 0)
            {
                throw new ArgumentOutOfRangeException(paramName, argument, $"Value cannot be greater than 0.");
            }
            return argument;
        }

        /// <summary>
        /// Throws a generic <see cref="ArgumentException"/> if the provided condition is false.
        /// </summary>
        /// <param name="condition">The condition that must be true.</param>
        /// <param name="message">The exception message if the condition is false.</param>
        /// <param name="paramName">The name of the parameter causing the failure.</param>
        /// <exception cref="ArgumentException">Thrown if <paramref name="condition"/> is false.</exception>
        public static void ThrowIfConditionIsFalse(bool condition, string message, string? paramName = null)
        {
            if (!condition)
            {
                throw new ArgumentException(message, paramName);
            }
        }
    }
}
