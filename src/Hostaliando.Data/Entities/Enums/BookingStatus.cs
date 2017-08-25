//-----------------------------------------------------------------------
// <copyright file="BookingStatus.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Data
{
    /// <summary>
    /// Booking Status
    /// </summary>
    public enum BookingStatus : short
    {
        /// <summary>
        /// The booked status
        /// </summary>
        Booked = 1,

        /// <summary>
        /// When the booking is canceled
        /// </summary>
        Canceled = 2
    }
}