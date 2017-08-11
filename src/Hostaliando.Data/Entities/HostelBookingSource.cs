//-----------------------------------------------------------------------
// <copyright file="HostelBookingSource.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Data
{
    /// <summary>
    /// Hostel Booking Source entity
    /// </summary>
    public partial class HostelBookingSource
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the source identifier.
        /// </summary>
        /// <value>
        /// The source identifier.
        /// </value>
        public int SourceId { get; set; }

        /// <summary>
        /// Gets or sets the hostel identifier.
        /// </summary>
        /// <value>
        /// The hostel identifier.
        /// </value>
        public int HostelId { get; set; }

        /// <summary>
        /// Gets or sets the hostel.
        /// </summary>
        /// <value>
        /// The hostel.
        /// </value>
        public virtual Hostel Hostel { get; set; }

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>
        /// The source.
        /// </value>
        public virtual BookingSource Source { get; set; }
    }
}