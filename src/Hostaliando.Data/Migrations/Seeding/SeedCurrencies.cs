//-----------------------------------------------------------------------
// <copyright file="SeedCurrencies.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Data.Migrations
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Seeds currencies
    /// </summary>
    public static class SeedCurrencies
    {
        /// <summary>
        /// Seeds the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        public static void Seed(HostaliandoContext context)
        {
            var list = new List<Currency>();

            list.Add(new Currency { Name = "COP", Symbol = "$" });
            list.Add(new Currency { Name = "USD", Symbol = "$" });

            foreach (var item in list)
            {
                if (!context.Currencies.Any(c => c.Name.Equals(item.Name)))
                {
                    context.Currencies.Add(item);
                }
            }

            context.SaveChanges();
        }
    }
}