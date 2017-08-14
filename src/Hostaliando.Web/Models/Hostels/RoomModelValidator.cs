//-----------------------------------------------------------------------
// <copyright file="RoomModelValidator.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Models
{
    using FluentValidation;

    /// <summary>
    /// Room Model Validator
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{Hostaliando.Web.Models.RoomModel}" />
    public class RoomModelValidator : AbstractValidator<RoomModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoomModelValidator"/> class.
        /// </summary>
        public RoomModelValidator()
        {
            this.RuleFor(c => c.Name)
                .NotNull()
                .NotEmpty();

            this.RuleFor(c => c.RoomType)
                .NotNull();

            this.RuleFor(c => c.Hostel)
                .NotNull();

            this.RuleFor(c => c.Beds)
                .GreaterThan((byte)0);
        }
    }
}