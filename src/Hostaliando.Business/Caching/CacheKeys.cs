//-----------------------------------------------------------------------
// <copyright file="CacheKeys.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Business.Caching
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Cache keys
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1310:FieldNamesMustNotContainUnderscore", Justification = "Reviewed.")]
    public static class CacheKeys
    {
        /// <summary>
        /// The all locations
        /// </summary>
        public const string LOCATIONS_ALL = "cache.locations.all";
    }
}