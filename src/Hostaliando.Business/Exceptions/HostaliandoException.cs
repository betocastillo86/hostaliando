//-----------------------------------------------------------------------
// <copyright file="HostaliandoException.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Business.Exceptions
{
    using Beto.Core.Exceptions;

    /// <summary>
    /// The business exception type
    /// </summary>
    /// <seealso cref="Beto.Core.Exceptions.CoreException{Hostaliando.Business.Exceptions.HostaliandoExceptionCode}" />
    public class HostaliandoException : CoreException<HostaliandoExceptionCode>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HostaliandoException"/> class.
        /// </summary>
        /// <param name="error">The error.</param>
        public HostaliandoException(string error) : base(error)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HostaliandoException"/> class.
        /// </summary>
        /// <param name="code">The code.</param>
        public HostaliandoException(HostaliandoExceptionCode code) : base(MessageExceptionFinder.GetErrorMessage(code))
        {
            this.Code = code;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HostaliandoException"/> class.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="error">The error.</param>
        public HostaliandoException(HostaliandoExceptionCode code, string error) : base(error)
        {
            this.Code = code;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HostaliandoException"/> class.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="code">The code.</param>
        public HostaliandoException(string target, HostaliandoExceptionCode code) : base(MessageExceptionFinder.GetErrorMessage(code))
        {
            this.Target = target;
            this.Code = code;
        }
    }
}