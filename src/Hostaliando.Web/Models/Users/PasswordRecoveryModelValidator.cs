//-----------------------------------------------------------------------
// <copyright file="PasswordRecoveryModelValidator.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Models
{
    using FluentValidation;

    /// <summary>
    /// Password Recovery Model Validator
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{Hostaliando.Web.Models.PasswordRecoveryModel}" />
    public class PasswordRecoveryModelValidator : AbstractValidator<PasswordRecoveryModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PasswordRecoveryModelValidator"/> class.
        /// </summary>
        public PasswordRecoveryModelValidator()
        {
            this.RuleFor(c => c.Email)
                .NotNull()
                .EmailAddress();
        }
    }
}