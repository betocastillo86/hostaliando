//-----------------------------------------------------------------------
// <copyright file="EmailNotification.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Data
{
    using System;

    /// <summary>
    /// Email Notification Entity
    /// </summary>
    public partial class EmailNotification
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets to.
        /// </summary>
        /// <value>
        /// To email.
        /// </value>
        public string To { get; set; }

        /// <summary>
        /// Gets or sets to name.
        /// </summary>
        /// <value>
        /// To name.
        /// </value>
        public string ToName { get; set; }

        /// <summary>
        /// Gets or sets the cc.
        /// </summary>
        /// <value>
        /// The cc.
        /// </value>
        public string Cc { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        /// <value>
        /// The subject.
        /// </value>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the body.
        /// </summary>
        /// <value>
        /// The body.
        /// </value>
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>
        /// The created date.
        /// </value>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the scheduled date.
        /// </summary>
        /// <value>
        /// The scheduled date.
        /// </value>
        public DateTime? ScheduledDate { get; set; }

        /// <summary>
        /// Gets or sets the sent date.
        /// </summary>
        /// <value>
        /// The sent date.
        /// </value>
        public DateTime? SentDate { get; set; }

        /// <summary>
        /// Gets or sets the sent tries.
        /// </summary>
        /// <value>
        /// The sent tries.
        /// </value>
        public short SentTries { get; set; }
    }
}