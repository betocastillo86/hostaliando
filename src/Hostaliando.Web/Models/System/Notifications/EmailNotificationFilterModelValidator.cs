//-----------------------------------------------------------------------
// <copyright file="EmailNotificationFilterModelValidator.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Models
{
    using Beto.Core.Web.Api.Models;
    using FluentValidation;

    /// <summary>
    /// Email Notification Filter Model Validator
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{Hostaliando.Web.Models.EmailNotificationFilterModel}" />
    public class EmailNotificationFilterModelValidator : AbstractValidator<EmailNotificationFilterModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailNotificationFilterModelValidator"/> class.
        /// </summary>
        public EmailNotificationFilterModelValidator()
        {
            this.AddBaseFilterValidations();
        }
    }
}