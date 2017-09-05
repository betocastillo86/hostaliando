//-----------------------------------------------------------------------
// <copyright file="BookingSourceFilterModelValidator.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Models
{
    using Beto.Core.Web.Api.Models;
    using FluentValidation;

    /// <summary>
    /// Booking Source Filter Validator
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{Hostaliando.Web.Models.BookingSourceFilterModel}" />
    public class BookingSourceFilterModelValidator : AbstractValidator<BookingSourceFilterModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookingSourceFilterModelValidator"/> class.
        /// </summary>
        public BookingSourceFilterModelValidator()
        {
            this.AddBaseFilterValidations();
        }
    }
}