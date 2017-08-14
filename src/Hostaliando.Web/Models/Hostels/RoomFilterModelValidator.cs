//-----------------------------------------------------------------------
// <copyright file="RoomFilterModelValidator.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Models.Hostels
{
    using Beto.Core.Web.Api.Models;
    using FluentValidation;

    /// <summary>
    /// Room Filter Model Validator
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{Hostaliando.Web.Models.RoomFilterModel}" />
    public class RoomFilterModelValidator : AbstractValidator<RoomFilterModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoomFilterModelValidator"/> class.
        /// </summary>
        public RoomFilterModelValidator()
        {
            this.AddBaseFilterValidations();

            this.RuleFor(c => c.Keyword)
                .MinimumLength(2);
        }
    }
}