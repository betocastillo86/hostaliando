﻿//-----------------------------------------------------------------------
// <copyright file="MessageExceptionFinder.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Business.Exceptions
{
    using System;
    using Beto.Core.Exceptions;

    /// <summary>
    /// Message Exception Finder
    /// </summary>
    /// <seealso cref="Beto.Core.Exceptions.IMessageExceptionFinder" />
    public class MessageExceptionFinder : IMessageExceptionFinder
    {
        /// <summary>
        /// Gets the error message.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns>the error message</returns>
        public static string GetErrorMessage(HostaliandoExceptionCode code)
        {
            switch (code)
            {
                case HostaliandoExceptionCode.InvalidForeignKey:
                    return "Llave foranea no existe";
                default:
                case HostaliandoExceptionCode.BadArgument:
                    return "Argumento invalido";
            }
        }

        /// <summary>
        /// Gets the error message depending of the exception code
        /// </summary>
        /// <typeparam name="T">the type of errors</typeparam>
        /// <param name="exceptionCode">The exception code.</param>
        /// <returns>
        /// The text of exception
        /// </returns>
        public string GetErrorMessage<T>(T exceptionCode)
        {
            if (exceptionCode is HostaliandoExceptionCode)
            {
                return MessageExceptionFinder.GetErrorMessage((HostaliandoExceptionCode)Enum.Parse(typeof(HostaliandoExceptionCode), exceptionCode.ToString()));
            }
            else
            {
                return string.Empty;
            }
        }
    }
}