//-----------------------------------------------------------------------
// <copyright file="LogExtensions.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Business.Extensions
{
    using System;
    using System.Threading.Tasks;
    using Beto.Core.Exceptions;
    using Hostaliando.Data;

    /// <summary>
    /// Log extensions
    /// </summary>
    public static class LogExtensions
    {
        /// <summary>
        /// Errors the specified short message.
        /// </summary>
        /// <param name="log">The log.</param>
        /// <param name="shortMessage">The short message.</param>
        /// <param name="fullMessage">The full message.</param>
        /// <param name="user">The user.</param>
        /// <returns>the task</returns>
        public static async Task Error(this ILoggerService log, string shortMessage, string fullMessage = null, User user = null)
        {
            await log.InsertAsync(shortMessage, fullMessage);
        }

        /// <summary>
        /// Errors the specified exception.
        /// </summary>
        /// <param name="log">The log.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="user">The user.</param>
        /// <returns>the task</returns>
        public static async Task Error(this ILoggerService log, Exception exception, User user = null)
        {
            await log.InsertAsync(exception.Message, exception.ToString());
        }
    }
}