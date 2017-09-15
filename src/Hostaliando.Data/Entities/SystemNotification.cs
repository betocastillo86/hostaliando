//-----------------------------------------------------------------------
// <copyright file="SystemNotification.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Data
{
    using System;
    using Beto.Core.Data;
    using Beto.Core.Data.Notifications;

    /// <summary>
    /// System notification entity
    /// </summary>
    public partial class SystemNotification : IEntity, ISystemNotificationEntity
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        /// <value>
        /// The creation date.
        /// </value>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="SystemNotification"/> is seen.
        /// </summary>
        /// <value>
        ///   <c>true</c> if seen; otherwise, <c>false</c>.
        /// </value>
        public bool Seen { get; set; }

        /// <summary>
        /// Gets or sets the target URL.
        /// </summary>
        /// <value>
        /// The target URL.
        /// </value>
        public string TargetUrl { get; set; }

        /// <summary>
        /// Gets or sets the trigger user identifier.
        /// </summary>
        /// <value>
        /// The trigger user identifier.
        /// </value>
        public int? TriggerUserId { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the trigger user.
        /// </summary>
        /// <value>
        /// The trigger user.
        /// </value>
        public virtual User TriggerUser { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public virtual User User { get; set; }
    }
}