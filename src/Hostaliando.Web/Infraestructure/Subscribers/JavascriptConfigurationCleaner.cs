//-----------------------------------------------------------------------
// <copyright file="JavascriptConfigurationCleaner.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Infraestructure.Subscribers
{
    using System.Threading.Tasks;
    using Beto.Core.EventPublisher;
    using Hostaliando.Data;
    using Hostaliando.Web.Infraestructure.Common;

    /// <summary>
    /// JAVASCRIPT Configuration Cleaner
    /// </summary>
    /// <seealso cref="Beto.Core.EventPublisher.ISubscriber{Beto.Core.EventPublisher.EntityInsertedMessage{Hostaliando.Data.SystemSetting}}" />
    /// <seealso cref="Beto.Core.EventPublisher.ISubscriber{Beto.Core.EventPublisher.EntityUpdatedMessage{Hostaliando.Data.SystemSetting}}" />
    /// <seealso cref="Beto.Core.EventPublisher.ISubscriber{Beto.Core.EventPublisher.EntityDeletedMessage{Hostaliando.Data.SystemSetting}}" />
    /// <seealso cref="Beto.Core.EventPublisher.ISubscriber{Beto.Core.EventPublisher.EntityUpdatedMessage{Hostaliando.Data.TextResource}}" />
    public class JavascriptConfigurationCleaner :
        ISubscriber<EntityInsertedMessage<SystemSetting>>,
        ISubscriber<EntityUpdatedMessage<SystemSetting>>,
        ISubscriber<EntityDeletedMessage<SystemSetting>>,
        ISubscriber<EntityUpdatedMessage<TextResource>>
    {
        /// <summary>
        /// The <c>javascript</c> configuration generator
        /// </summary>
        private readonly IJavascriptConfigurationGenerator javascriptConfigurationGenerator;

        /// <summary>
        /// Initializes a new instance of the <see cref="JavascriptConfigurationCleaner"/> class.
        /// </summary>
        /// <param name="javascriptConfigurationGenerator">The <c>javascript</c> configuration generator.</param>
        public JavascriptConfigurationCleaner(IJavascriptConfigurationGenerator javascriptConfigurationGenerator)
        {
            this.javascriptConfigurationGenerator = javascriptConfigurationGenerator;
        }

        /// <summary>
        /// Handles the event.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>
        /// the task
        /// </returns>
        public async Task HandleEvent(EntityUpdatedMessage<TextResource> message)
        {
            await this.Clean();
        }

        /// <summary>
        /// Handles the event.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>the task</returns>
        public async Task HandleEvent(EntityInsertedMessage<SystemSetting> message)
        {
            await this.Clean();
        }

        /// <summary>
        /// Handles the event.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>the task</returns>
        public async Task HandleEvent(EntityDeletedMessage<SystemSetting> message)
        {
            await this.Clean();
        }

        /// <summary>
        /// Handles the event.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>the task</returns>
        public async Task HandleEvent(EntityUpdatedMessage<SystemSetting> message)
        {
            await this.Clean();
        }

        /// <summary>
        /// Cleans this instance.
        /// </summary>
        /// <returns>the task</returns>
        private async Task Clean()
        {
            this.javascriptConfigurationGenerator.CreateJavascriptConfigurationFile();
            await Task.FromResult(0);
        }
    }
}