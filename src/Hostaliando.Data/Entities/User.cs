//-----------------------------------------------------------------------
// <copyright file="User.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Data
{
    using System;
    using Beto.Core.Data;

    /// <summary>
    /// User entity
    /// </summary>
    public partial class User : IEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        public User()
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
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the salt.
        /// </summary>
        /// <value>
        /// The salt.
        /// </value>
        public string Salt { get; set; }

        /// <summary>
        /// Gets or sets the role identifier.
        /// </summary>
        /// <value>
        /// The role identifier.
        /// </value>
        public short RoleId { get; set; }

        /// <summary>
        /// Gets or sets the hostel identifier.
        /// </summary>
        /// <value>
        /// The hostel identifier.
        /// </value>
        public int? HostelId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="User"/> is deleted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if deleted; otherwise, <c>false</c>.
        /// </value>
        public bool Deleted { get; set; }

        /// <summary>
        /// Gets or sets the time zone.
        /// </summary>
        /// <value>
        /// The time zone.
        /// </value>
        public short TimeZone { get; set; }

        /// <summary>
        /// Gets or sets the hostel.
        /// </summary>
        /// <value>
        /// The hostel.
        /// </value>
        public virtual Hostel Hostel { get; set; }

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>
        /// The role.
        /// </value>
        public Role Role
        {
            get
            {
                return (Role)this.RoleId;
            }

            set
            {
                this.RoleId = Convert.ToInt16(value);
            }
        }
    }
}