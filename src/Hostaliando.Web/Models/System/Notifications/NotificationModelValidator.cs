//-----------------------------------------------------------------------
// <copyright file="NotificationModelValidator.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Models
{
    using FluentValidation;

    /// <summary>
    /// Notification Filter Model Validator
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{Hostaliando.Web.Models.NotificationModel}" />
    public class NotificationModelValidator : AbstractValidator<NotificationModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationModelValidator"/> class.
        /// </summary>
        public NotificationModelValidator()
        {
            this.RuleFor(c => c.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(500);

            this.RuleFor(c => c.SystemText)
                .MaximumLength(4000);

            this.RuleFor(c => c.EmailSubject)
                .MaximumLength(1000);
        }
    }
}