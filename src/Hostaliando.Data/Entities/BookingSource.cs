//-----------------------------------------------------------------------
// <copyright file="BookingSource.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Data
{
    /// <summary>
    /// Booking Source entity
    /// </summary>
    public partial class BookingSource
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookingSource"/> class.
        /// </summary>
        public BookingSource()
        {
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }
    }
}