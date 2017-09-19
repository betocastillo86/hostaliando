//-----------------------------------------------------------------------
// <copyright file="TaskSettings.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Business.Configuration
{
    using Beto.Core.Data.Configuration;
    using Hostaliando.Business.Services.Extensions;

    /// <summary>
    /// Task Settings
    /// </summary>
    /// <seealso cref="Hostaliando.Business.Configuration.ITaskSettings" />
    public class TaskSettings : ITaskSettings
    {
        /// <summary>
        /// The core setting service
        /// </summary>
        private readonly ICoreSettingService coreSettingService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskSettings"/> class.
        /// </summary>
        /// <param name="coreSettingService">The core setting service.</param>
        public TaskSettings(ICoreSettingService coreSettingService)
        {
            this.coreSettingService = coreSettingService;
        }

        /// <summary>
        /// Gets the send emails interval.
        /// </summary>
        /// <value>
        /// The send emails interval.
        /// </value>
        public int SendEmailsInterval => this.coreSettingService.Get<int>("TaskSettings.SendEmailsInterval");
    }
}