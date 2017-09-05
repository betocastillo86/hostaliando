//-----------------------------------------------------------------------
// <copyright file="IJavascriptConfigurationGenerator.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Web.Infraestructure.Common
{
    /// <summary>
    /// Interface of <c>javascript</c> generator
    /// </summary>
    public interface IJavascriptConfigurationGenerator
    {
        /// <summary>
        /// Creates the <c>javascript</c> configuration file.
        /// </summary>
        void CreateJavascriptConfigurationFile();
    }
}