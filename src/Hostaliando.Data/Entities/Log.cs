//-----------------------------------------------------------------------
// <copyright file="Log.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Data
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;
    using Beto.Core.Data;

    /// <summary>
    /// Log entity
    /// </summary>
    public partial class Log : IEntity
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the log level identifier.
        /// </summary>
        /// <value>
        /// The log level identifier.
        /// </value>
        public short LogLevelId { get; set; }

        /// <summary>
        /// Gets or sets the short message.
        /// </summary>
        /// <value>
        /// The short message.
        /// </value>
        public string ShortMessage { get; set; }

        /// <summary>
        /// Gets or sets the full message.
        /// </summary>
        /// <value>
        /// The full message.
        /// </value>
        public string FullMessage { get; set; }

        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        /// <value>
        /// The creation date.
        /// </value>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Gets or sets the IP address.
        /// </summary>
        /// <value>
        /// The IP address.
        /// </value>
        public string IpAddress { get; set; }

        /// <summary>
        /// Gets or sets the page URL.
        /// </summary>
        /// <value>
        /// The page URL.
        /// </value>
        public string PageUrl { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int? UserId { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public virtual User User { get; set; }

        /// <summary>
        /// Gets or sets the log level.
        /// </summary>
        /// <value>
        /// The log level.
        /// </value>
        [NotMapped]
        public LogLevel LogLevel
        {
            get
            {
                return (LogLevel)this.LogLevelId;
            }

            set
            {
                this.LogLevelId = Convert.ToInt16(value);
            }
        }
    }
}