//-----------------------------------------------------------------------
// <copyright file="HostaliandoContext.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Data
{
    using Beto.Core.Data;
    using Hostaliando.Data.Entities.Mapping;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Context of <c>Hostaliando</c>
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.DbContext" />
    public partial class HostaliandoContext : DbContext, IDbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HostaliandoContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public HostaliandoContext(DbContextOptions<HostaliandoContext> options) : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the booking sources.
        /// </summary>
        /// <value>
        /// The booking sources.
        /// </value>
        public virtual DbSet<BookingSource> BookingSources { get; set; }

        /// <summary>
        /// Gets or sets the bookings.
        /// </summary>
        /// <value>
        /// The bookings.
        /// </value>
        public virtual DbSet<Booking> Bookings { get; set; }

        /// <summary>
        /// Gets or sets the currencies.
        /// </summary>
        /// <value>
        /// The currencies.
        /// </value>
        public virtual DbSet<Currency> Currencies { get; set; }

        /// <summary>
        /// Gets or sets the email notifications.
        /// </summary>
        /// <value>
        /// The email notifications.
        /// </value>
        public virtual DbSet<EmailNotification> EmailNotifications { get; set; }

        /// <summary>
        /// Gets or sets the hostel booking sources.
        /// </summary>
        /// <value>
        /// The hostel booking sources.
        /// </value>
        public virtual DbSet<HostelBookingSource> HostelBookingSources { get; set; }

        /// <summary>
        /// Gets or sets the hostels.
        /// </summary>
        /// <value>
        /// The hostels.
        /// </value>
        public virtual DbSet<Hostel> Hostels { get; set; }

        /// <summary>
        /// Gets or sets the locations.
        /// </summary>
        /// <value>
        /// The locations.
        /// </value>
        public virtual DbSet<Location> Locations { get; set; }

        /// <summary>
        /// Gets or sets the logs.
        /// </summary>
        /// <value>
        /// The logs.
        /// </value>
        public virtual DbSet<Log> Logs { get; set; }

        /// <summary>
        /// Gets or sets the notifications.
        /// </summary>
        /// <value>
        /// The notifications.
        /// </value>
        public virtual DbSet<Notification> Notifications { get; set; }

        /// <summary>
        /// Gets or sets the rooms.
        /// </summary>
        /// <value>
        /// The rooms.
        /// </value>
        public virtual DbSet<Room> Rooms { get; set; }

        /// <summary>
        /// Gets or sets the system notifications.
        /// </summary>
        /// <value>
        /// The system notifications.
        /// </value>
        public virtual DbSet<SystemNotification> SystemNotifications { get; set; }

        /// <summary>
        /// Gets or sets the system settings.
        /// </summary>
        /// <value>
        /// The system settings.
        /// </value>
        public virtual DbSet<SystemSetting> SystemSettings { get; set; }

        /// <summary>
        /// Gets or sets the text resources.
        /// </summary>
        /// <value>
        /// The text resources.
        /// </value>
        public virtual DbSet<TextResource> TextResources { get; set; }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        public virtual DbSet<User> Users { get; set; }

        /// <summary>
        /// Override this method to further configure the model that was discovered by convention from the entity types
        /// exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on your derived context. The resulting model may be cached
        /// and re-used for subsequent instances of your derived context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context. Databases (and other extensions) typically
        /// define extension methods on this object that allow you to configure aspects of the model that are specific
        /// to a given database.</param>
        /// <remarks>
        /// If a model is explicitly set on the options for this context (via <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />)
        /// then this method will not be run.
        /// </remarks>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookingSource>().Map();

            modelBuilder.Entity<Booking>().Map();

            modelBuilder.Entity<Currency>().Map();

            modelBuilder.Entity<EmailNotification>().Map();

            modelBuilder.Entity<HostelBookingSource>().Map();

            modelBuilder.Entity<Hostel>().Map();

            modelBuilder.Entity<Location>().Map();

            modelBuilder.Entity<Log>().Map();

            modelBuilder.Entity<Notification>().Map();

            modelBuilder.Entity<Room>().Map();

            modelBuilder.Entity<SystemNotification>().Map();

            modelBuilder.Entity<SystemSetting>().Map();

            modelBuilder.Entity<TextResource>().Map();

            modelBuilder.Entity<User>().Map();
        }
    }
}