//-----------------------------------------------------------------------
// <copyright file="LogModelValidator.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Models
{
    using FluentValidation;

    /// <summary>
    /// Log Model Validator
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{Hostaliando.Web.Models.LogModel}" />
    public class LogModelValidator : AbstractValidator<LogModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogModelValidator"/> class.
        /// </summary>
        public LogModelValidator()
        {
            this.RuleFor(c => c.ShortMessage)
                .NotNull()
                .NotEmpty();
        }
    }
}