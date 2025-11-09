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

        /// <summary>
        /// Validates that a <see cref="Stream"/> is not null and can be read from.
        /// </summary>
        /// <param name="stream">The <see cref="Stream"/> object to validate.</param>
        /// <param name="paramName">The name of the parameter. This is automatically set by the compiler.</param>
        /// <returns>The original <see cref="Stream"/> if it passes validation.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="stream"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="stream"/> cannot be read.</exception>
        [return: NotNull]
        public static Stream ValidateStreamReadable([NotNull] Stream stream, [CallerArgumentExpression(nameof(stream))] string? paramName = null)
        {
            // Use your existing ThrowIfNull helper (assuming it's available)
            ThrowIfNull(stream, paramName);

            // Check for readability
            if (!stream.CanRead)
            {
                throw new ArgumentException($"The stream passed for '{paramName}' must be readable (CanRead must be true).", paramName);
            }

            return stream;
        }

        /// <summary>
        /// Validates that a <see cref="Stream"/> is not null and can be written to.
        /// </summary>
        /// <param name="stream">The <see cref="Stream"/> object to validate.</param>
        /// <param name="paramName">The name of the parameter. This is automatically set by the compiler.</param>
        /// <returns>The original <see cref="Stream"/> if it passes validation.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="stream"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="stream"/> cannot be written to.</exception>
        [return: NotNull]
        public static Stream ValidateStreamWritable([NotNull] Stream stream, [CallerArgumentExpression(nameof(stream))] string? paramName = null)
        {
            ThrowIfNull(stream, paramName);

            // Check for writability
            if (!stream.CanWrite)
            {
                throw new ArgumentException($"The stream passed for '{paramName}' must be writable (CanWrite must be true).", paramName);
            }

            return stream;
        }

        /// <summary>
        /// Validates that a seekable <see cref="Stream"/> is not null, is readable, and contains data.
        /// </summary>
        /// <param name="stream">The <see cref="Stream"/> object to validate.</param>
        /// <param name="paramName">The name of the parameter. This is automatically set by the compiler.</param>
        /// <returns>The original <see cref="Stream"/> if it passes validation.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="stream"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="stream"/> is unreadable, has zero length, or has already been fully consumed.</exception>
        [return: NotNull]
        public static Stream ValidateStreamHasContent([NotNull] Stream stream, [CallerArgumentExpression(nameof(stream))] string? paramName = null)
        {
            // 1. Check for null and readability first
            ValidateStreamReadable(stream, paramName);

            // 2. Check for content and position (only if seekable)
            if (stream.CanSeek)
            {
                if (stream.Length == 0)
                {
                    throw new ArgumentException($"The seekable stream passed for '{paramName}' has a length of zero.", paramName);
                }

                if (stream.Position >= stream.Length)
                {
                    throw new ArgumentException($"The position of the seekable stream passed for '{paramName}' is at or beyond its end (already fully consumed).", paramName);
                }
            }
            // If it's not seekable, we can't reliably check length/position, so we assume it's ready to read.

            return stream;
        }
    }
}
