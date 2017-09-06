//-----------------------------------------------------------------------
// <copyright file="BookingModel.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Models
{
    using System;
    using Hostaliando.Data;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    /// <summary>
    /// Booking Model
    /// </summary>
    /// <seealso cref="Hostaliando.Web.Models.BaseModel" />
    public class BookingModel : BaseModel
    {
        /// <summary>
        /// Gets or sets the room.
        /// </summary>
        /// <value>
        /// The room.
        /// </value>
        public RoomModel Room { get; set; }

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
        /// Gets or sets the name of the guest.
        /// </summary>
        /// <value>
        /// The name of the guest.
        /// </value>
        public string GuestName { get; set; }

        /// <summary>
        /// Gets or sets the guest email.
        /// </summary>
        /// <value>
        /// The guest email.
        /// </value>
        public string GuestEmail { get; set; }

        /// <summary>
        /// Gets or sets the guest location identifier.
        /// </summary>
        /// <value>
        /// The guest location identifier.
        /// </value>
        public LocationModel GuestLocation { get; set; }

        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        /// <value>
        /// The comments.
        /// </value>
        public string Comments { get; set; }

        /// <summary>
        /// Gets or sets the total price.
        /// </summary>
        /// <value>
        /// The total price.
        /// </value>
        public decimal? TotalPrice { get; set; }

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>
        /// The source.
        /// </value>
        public BaseNamedModel Source { get; set; }

        /// <summary>
        /// Gets or sets the creation date UTC.
        /// </summary>
        /// <value>
        /// The creation date UTC.
        /// </value>
        public DateTime CreationDateUtc { get; set; }

        /// <summary>
        /// Gets or sets the nigths.
        /// </summary>
        /// <value>
        /// The nigths.
        /// </value>
        public double Nigths => (this.ToDate.Value - this.FromDate.Value).TotalDays;

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        [JsonConverter(typeof(StringEnumConverter))]
        public BookingStatus Status { get; set; }
    }
}