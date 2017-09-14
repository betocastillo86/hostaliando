//-----------------------------------------------------------------------
// <copyright file="NotificationFilterModelValidator.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Models
{
    using Beto.Core.Web.Api.Models;
    using FluentValidation;

    /// <summary>
    /// Notification Filter Model
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{Hostaliando.Web.Models.NotificationFilterModel}" />
    public class NotificationFilterModelValidator : AbstractValidator<NotificationFilterModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationFilterModelValidator"/> class.
        /// </summary>
        public NotificationFilterModelValidator()
        {
            this.AddBaseFilterValidations();
        }
    }
}