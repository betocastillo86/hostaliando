//-----------------------------------------------------------------------
// <copyright file="BookingFilterModel.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Models
{
    using System;
    using Beto.Core.Web.Api;
    using Hostaliando.Data;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Booking Filter Model
    /// </summary>
    /// <seealso cref="Beto.Core.Web.Api.BaseFilterModel" />
    public class BookingFilterModel : BaseFilterModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookingFilterModel"/> class.
        /// </summary>
        public BookingFilterModel()
        {
            this.MaxPageSize = 200;
            this.ValidOrdersBy = Enum.GetNames(typeof(SortBookingBy));
        }

        /// <summary>
        /// Gets or sets the keyword.
        /// </summary>
        /// <value>
        /// The keyword.
        /// </value>
        public string Keyword { get; set; }

        /// <summary>
        /// Gets or sets the hostel identifier.
        /// </summary>
        /// <value>
        /// The hostel identifier.
        /// </value>
        public int? HostelId { get; set; }

        /// <summary>
        /// Gets or sets the room identifier.
        /// </summary>
        /// <value>
        /// The room identifier.
        /// </value>
        public int? RoomId { get; set; }

        /// <summary>
        /// Gets or sets from date.
        /// </summary>
        /// <value>
        /// From date.
        /// </value>
        public DateTime? FromDate { get; set; }

        /// <summary>
        /// Gets or sets to date.
        /// </summary>
        /// <value>
        /// To date.
        /// </value>
        public DateTime? ToDate { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        [JsonConverter(typeof(StringEnumConverter))]
        public BookingStatus? Status { get; set; }

        /// <summary>
        /// Gets or sets the not status.
        /// </summary>
        /// <value>
        /// The not status.
        /// </value>
        [JsonConverter(typeof(StringEnumConverter))]
        public BookingStatus? NotStatus { get; set; }

        /// <summary>
        /// Gets the order by enum.
        /// </summary>
        /// <value>
        /// The order by enum.
        /// </value>
        public SortBookingBy OrderByEnum
        {
            get
            {
                return !string.IsNullOrEmpty(this.OrderBy) ? (SortBookingBy)Enum.Parse(typeof(SortBookingBy), this.OrderBy) : SortBookingBy.Recent;
            }
        }
    }
}