﻿//-----------------------------------------------------------------------
// <copyright file="Currency.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Data
{
    using Beto.Core.Data;

    /// <summary>
    /// Currency Entity
    /// </summary>
    public partial class Currency : IEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Currency"/> class.
        /// </summary>
        public Currency()
        {
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the symbol.
        /// </summary>
        /// <value>
        /// The symbol.
        /// </value>
        public string Symbol { get; set; }
    }
}