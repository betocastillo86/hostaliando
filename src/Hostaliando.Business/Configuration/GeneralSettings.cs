﻿//-----------------------------------------------------------------------
// <copyright file="GeneralSettings.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Business.Configuration
{
    using Beto.Core.Data.Configuration;
    using Hostaliando.Business.Services.Extensions;

    /// <summary>
    /// General Settings
    /// </summary>
    /// <seealso cref="Hostaliando.Business.Configuration.IGeneralSettings" />
    public class GeneralSettings : IGeneralSettings
    {
        /// <summary>
        /// The core setting service
        /// </summary>
        private readonly ICoreSettingService coreSettingService;

        /// <summary>
        /// Initializes a new instance of the <see cref="GeneralSettings"/> class.
        /// </summary>
        /// <param name="coreSettingService">The core setting service.</param>
        public GeneralSettings(ICoreSettingService coreSettingService)
        {
            this.coreSettingService = coreSettingService;
        }

        /// <summary>
        /// Gets the site URL.
        /// </summary>
        /// <value>
        /// The site URL.
        /// </value>
        public string SiteUrl => this.coreSettingService.Get<string>("GeneralSettings.SiteUrl");

        /// <summary>
        /// Gets the body base HTML.
        /// </summary>
        /// <value>
        /// The body base HTML.
        /// </value>
        public string BodyBaseHtml => this.coreSettingService.Get<string>("GeneralSettings.BodyBaseHtml");

        /// <summary>
        /// Gets the date format.
        /// </summary>
        /// <value>
        /// The date format.
        /// </value>
        public string DateFormat => this.coreSettingService.Get<string>("GeneralSettings.DateFormat");
    }
}