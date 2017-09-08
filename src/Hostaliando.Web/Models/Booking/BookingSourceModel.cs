namespace Hostaliando.Web.Models
{
    /// <summary>
    /// Booking Source Model
    /// </summary>
    /// <seealso cref="Hostaliando.Web.Models.BaseNamedModel" />
    public class BookingSourceModel : BaseNamedModel
    {
        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>
        /// The color.
        /// </value>
        public string Color { get; set; }

        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        /// <value>
        /// The icon.
        /// </value>
        public string Icon { get; set; }
    }
}