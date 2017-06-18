using System;

namespace DetectPublicApiChanges.Extensions
{
    /// <summary>
    /// The class ObjectExtensions contains extension methods for the type object
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Determines whether the object is not null, then executes the function and returns the result
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="whenNotNull">The when not null.</param>
        /// <returns></returns>
        public static TResult IsNotNull<TResult, TType>(this TType obj, Func<TType, TResult> whenNotNull)
        {
            return obj == null ? default(TResult) : whenNotNull(obj);
        }

        /// <summary>
        /// Determines whether the object is not null, then executes the function and sets <paramref name="result"/>
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="whenNotNull">The when not null.</param>
        /// <param name="result">The result.</param>
        public static void IsNotNull<TResult, TType>(this TType obj, Func<TType, TResult> whenNotNull, ref TResult result)
        {
            if (obj == null)
                return;

            result = whenNotNull(obj);
        }

        /// <summary>
        /// Casts the specified object.
        /// </summary>
        /// <typeparam name="TType">The type of the cast.</typeparam>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static TType As<TType>(this object obj) where TType : class
        {
            return obj as TType;
        }
    }
}