//-----------------------------------------------------------------------
// <copyright file="BookingFilterModelValidator.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Models
{
    using Beto.Core.Web.Api.Models;
    using FluentValidation;

    /// <summary>
    /// Booking Filter Model Validator
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{Hostaliando.Web.Models.BookingFilterModel}" />
    public class BookingFilterModelValidator : AbstractValidator<BookingFilterModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookingFilterModelValidator"/> class.
        /// </summary>
        public BookingFilterModelValidator()
        {
            this.AddBaseFilterValidations();
        }
    }
}