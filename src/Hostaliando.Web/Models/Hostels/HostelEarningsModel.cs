//-----------------------------------------------------------------------
// <copyright file="HostelEarningsModel.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Models
{
    /// <summary>
    /// Hostel Earnings Model
    /// </summary>
    public class HostelEarningsModel
    {
        /// <summary>
        /// Gets or sets the month earnings.
        /// </summary>
        /// <value>
        /// The month.
        /// </value>
        public decimal Month { get; set; }

        /// <summary>
        /// Gets or sets the two months earnings.
        /// </summary>
        /// <value>
        /// The two months.
        /// </value>
        public decimal TwoMonths { get; set; }

        /// <summary>
        /// Gets or sets the week earnings.
        /// </summary>
        /// <value>
        /// The week.
        /// </value>
        public decimal Week { get; set; }

        /// <summary>
        /// Gets or sets the two weeks earnings.
        /// </summary>
        /// <value>
        /// The two weeks.
        /// </value>
        public decimal TwoWeeks { get; set; }

        /// <summary>
        /// Gets or sets the today earnings.
        /// </summary>
        /// <value>
        /// The today.
        /// </value>
        public decimal Today { get; set; }

        /// <summary>
        /// Gets or sets the two days earnings.
        /// </summary>
        /// <value>
        /// The two days.
        /// </value>
        public decimal TwoDays { get; set; }
    }
}