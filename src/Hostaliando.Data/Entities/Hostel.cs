//-----------------------------------------------------------------------
// <copyright file="Hostel.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Data
{
    using System;
    using System.Collections.Generic;
    using Beto.Core.Data;

    /// <summary>
    /// Hostel entity
    /// </summary>
    public partial class Hostel : IEntity
    {
        /// <summary>
        /// The hostel booking sources
        /// </summary>
        private ICollection<HostelBookingSource> hostelBookingSources;

        /// <summary>
        /// The rooms
        /// </summary>
        private ICollection<Room> rooms;

        /// <summary>
        /// Initializes a new instance of the <see cref="Hostel"/> class.
        /// </summary>
        public Hostel()
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
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the location identifier.
        /// </summary>
        /// <value>
        /// The location identifier.
        /// </value>
        public int LocationId { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>
        /// The phone number.
        /// </value>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the currency identifier.
        /// </summary>
        /// <value>
        /// The currency identifier.
        /// </value>
        public int CurrencyId { get; set; }

        /// <summary>
        /// Gets or sets the creation date UTC.
        /// </summary>
        /// <value>
        /// The creation date UTC.
        /// </value>
        public DateTime CreationDateUtc { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Hostel"/> is deleted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if deleted; otherwise, <c>false</c>.
        /// </value>
        public bool Deleted { get; set; }

        /// <summary>
        /// Gets or sets the hostel booking sources.
        /// </summary>
        /// <value>
        /// The hostel booking sources.
        /// </value>
        public virtual ICollection<HostelBookingSource> HostelBookingSources
        {
            get
            {
                return this.hostelBookingSources ?? new HashSet<HostelBookingSource>();
            }

            set
            {
                this.hostelBookingSources = value;
            }
        }

        /// <summary>
        /// Gets or sets the rooms.
        /// </summary>
        /// <value>
        /// The rooms.
        /// </value>
        public virtual ICollection<Room> Rooms
        {
            get
            {
                return this.rooms ?? new HashSet<Room>();
            }

            set
            {
                this.rooms = value;
            }
        }

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        /// <value>
        /// The currency.
        /// </value>
        public virtual Currency Currency { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        public virtual Location Location { get; set; }
    }
}