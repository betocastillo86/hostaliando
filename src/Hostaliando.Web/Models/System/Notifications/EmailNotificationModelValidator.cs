//-----------------------------------------------------------------------
// <copyright file="EmailNotificationModelValidator.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Models
{
    using FluentValidation;

    /// <summary>
    /// Email Notification Model Validator
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{Hostaliando.Web.Models.EmailNotificationModel}" />
    public class EmailNotificationModelValidator : AbstractValidator<EmailNotificationModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailNotificationModelValidator"/> class.
        /// </summary>
        public EmailNotificationModelValidator()
        {
            this.RuleFor(c => c.To)
                .NotNull()
                .EmailAddress();

            this.RuleFor(c => c.Subject)
                .NotNull()
                .MinimumLength(10)
                .MaximumLength(150);

            this.RuleFor(c => c.Body)
                .NotNull()
                .NotEmpty();
        }
    }
}