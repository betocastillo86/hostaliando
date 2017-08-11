//-----------------------------------------------------------------------
// <copyright file="Room.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Data
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Room entity
    /// </summary>
    public partial class Room
    {
        /// <summary>
        /// The bookings
        /// </summary>
        private ICollection<Booking> bookings;

        /// <summary>
        /// Initializes a new instance of the <see cref="Room"/> class.
        /// </summary>
        public Room()
        {
            this.Bookings = new HashSet<Booking>();
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the hostel identifier.
        /// </summary>
        /// <value>
        /// The hostel identifier.
        /// </value>
        public int HostelId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the beds.
        /// </summary>
        /// <value>
        /// The beds.
        /// </value>
        public byte Beds { get; set; }

        /// <summary>
        /// Gets or sets the room type identifier.
        /// </summary>
        /// <value>
        /// The room type identifier.
        /// </value>
        public short RoomTypeId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is privated.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is privated; otherwise, <c>false</c>.
        /// </value>
        public bool IsPrivated { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Room"/> is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Room"/> is deleted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if deleted; otherwise, <c>false</c>.
        /// </value>
        public bool Deleted { get; set; }

        /// <summary>
        /// Gets or sets the creation date UTC.
        /// </summary>
        /// <value>
        /// The creation date UTC.
        /// </value>
        public DateTime CreationDateUtc { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int? UserId { get; set; }

        /// <summary>
        /// Gets or sets the bookings.
        /// </summary>
        /// <value>
        /// The bookings.
        /// </value>
        public virtual ICollection<Booking> Bookings
        {
            get
            {
                return this.bookings ?? new HashSet<Booking>();
            }

            set
            {
                this.bookings = value;
            }
        }

        /// <summary>
        /// Gets or sets the hostel.
        /// </summary>
        /// <value>
        /// The hostel.
        /// </value>
        public virtual Hostel Hostel { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public virtual User User { get; set; }
    }
}