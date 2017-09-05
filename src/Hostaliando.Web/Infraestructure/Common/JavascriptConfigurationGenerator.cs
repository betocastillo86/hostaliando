//-----------------------------------------------------------------------
// <copyright file="JavascriptConfigurationGenerator.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Infraestructure.Common
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Beto.Core.Caching;
    using Beto.Core.Data;
    using Hostaliando.Business.Configuration;
    using Hostaliando.Business.Services;
    using Hostaliando.Data;
    using Microsoft.AspNetCore.Hosting;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// JAVASCRIPT Configuration Generator
    /// </summary>
    /// <seealso cref="Hostaliando.Web.Infraestructure.Common.IJavascriptConfigurationGenerator" />
    public class JavascriptConfigurationGenerator : IJavascriptConfigurationGenerator
    {
        /// <summary>
        /// The cache manager
        /// </summary>
        private readonly ICacheManager cacheManager;

        /// <summary>
        /// The currency service
        /// </summary>
        private readonly ICurrencyService currencyService;

        /// <summary>
        /// The environment
        /// </summary>
        private readonly IHostingEnvironment env;

        /// <summary>
        /// The general settings
        /// </summary>
        private readonly IGeneralSettings generalSettings;

        /// <summary>
        /// The system setting repository
        /// </summary>
        private readonly IRepository<SystemSetting> systemSettingRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="JavascriptConfigurationGenerator"/> class.
        /// </summary>
        /// <param name="generalSettings">The general settings.</param>
        /// <param name="cacheManager">The cache manager.</param>
        /// <param name="env">The env.</param>
        /// <param name="systemSettingRepository">The system setting repository.</param>
        /// <param name="currencyService">The currency service.</param>
        public JavascriptConfigurationGenerator(
            IGeneralSettings generalSettings,
            ICacheManager cacheManager,
            IHostingEnvironment env,
            IRepository<SystemSetting> systemSettingRepository,
            ICurrencyService currencyService)
        {
            this.generalSettings = generalSettings;
            this.cacheManager = cacheManager;
            this.env = env;
            this.systemSettingRepository = systemSettingRepository;
            this.currencyService = currencyService;
        }

        /// <summary>
        /// Creates the <c>javascript</c> configuration file.
        /// </summary>
        public void CreateJavascriptConfigurationFile()
        {
            this.cacheManager.Clear();

            var isDebug = false;

#if DEBUG
            isDebug = true;
#endif

            ////Actualiza la llave de cache del javascript
            var key = "GeneralSettings.ConfigJavascriptCacheKey";
            var cacheKey = this.systemSettingRepository.Table.FirstOrDefault(c => c.Name.Equals(key));

            this.SaveFile(this.GetJson(isDebug, cacheKey?.Value));

            if (cacheKey == null)
            {
                this.systemSettingRepository.Insert(new SystemSetting() { Name = key, Value = Guid.NewGuid().ToString() });
            }
            else
            {
                cacheKey.Value = Guid.NewGuid().ToString();
                this.systemSettingRepository.Update(cacheKey);
            }

            ////Clears cache after creating file because of the javascript cache key
            this.cacheManager.Clear();
        }

        /// <summary>
        /// Adds the resources.
        /// </summary>
        /// <param name="resources">The resources.</param>
        /// <param name="key">The key.</param>
        private void AddResources(IDictionary<string, string> resources, string key)
        {
            ////resources.Add(key, this.textResourceService.GetCachedResource(this.cacheManager, key));
        }

        /// <summary>
        /// Gets the front resources.
        /// </summary>
        /// <returns>the resources</returns>
        private IDictionary<string, string> GetFrontResources()
        {
            var resources = new Dictionary<string, string>();
            this.AddResources(resources, "Home.HowTo.Help.Title");
            return resources;
        }

        /// <summary>
        /// Gets the JSON.
        /// </summary>
        /// <param name="isDebug">if set to <c>true</c> [is debug].</param>
        /// <param name="cacheKey">The cache key.</param>
        /// <returns>the JSON</returns>
        private string GetJson(bool isDebug, string cacheKey)
        {
            var config = new
            {
                general = new
                {
                    siteUrl = this.generalSettings.SiteUrl
                },
                resources = this.GetFrontResources(),
                isDebug = isDebug,
                currencies = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(this.currencyService.GetAll().Result, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }))
            };

            return JsonConvert.SerializeObject(config);
        }

        /// <summary>
        /// Saves the file.
        /// </summary>
        /// <param name="jsonString">The JSON string.</param>
        private void SaveFile(string jsonString)
        {
            ////If does not exist the directory. It creates it.
            var filename = $"{env.ContentRootPath}/wwwroot/js/hostaliando.configuration.js";
            var directory = System.IO.Path.GetDirectoryName(filename);

            if (!System.IO.Directory.Exists(directory))
            {
                System.IO.Directory.CreateDirectory(directory);
            }

            using (var stream = new FileStream(filename, FileMode.Create))
            {
                using (var sw = new System.IO.StreamWriter(stream))
                {
                    sw.Write($"var app = app || {{}}; app.Settings = {jsonString.ToString()}");
                }
            }
        }
    }
}