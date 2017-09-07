//-----------------------------------------------------------------------
// <copyright file="SortBookingBy.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Data
{
    /// <summary>
    /// Sorting booking options
    /// </summary>
    public enum SortBookingBy
    {
        /// <summary>
        /// by most recent
        /// </summary>
        Recent,

        /// <summary>
        /// The oldest
        /// </summary>
        Oldest,

        /// <summary>
        /// by total price
        /// </summary>
        TotalPrice,

        /// <summary>
        /// by room
        /// </summary>
        Room,

        /// <summary>
        /// From date
        /// </summary>
        FromDate
    }
}