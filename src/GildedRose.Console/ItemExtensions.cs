namespace GildedRose.Console
{
    using System;
    using GildedRose.Console.Models;

    /// <summary>
    /// Extension methods for the <see cref="Item"/> class.
    /// </summary>
    public static class ItemExtensions
    {
        /// <summary>
        /// Determines the inventory management strategy for the current <see cref="Item"/>.
        /// </summary>
        /// <param name="item">Current inventory item.</param>
        /// <returns>Matching strategy.</returns>
        public static ItemType GetItemType(this Item item)
        {
            if (item.Quality > 50)
            {
                return ItemType.Legendary;
            }

            if (item.Name.ToLowerInvariant().Contains("conjured"))
            {
                return ItemType.Conjured;
            }

            if (item.Name.Equals("Aged Brie", StringComparison.InvariantCulture))
            {
                return ItemType.Maturing;
            }

            if (item.Name.Equals("Backstage passes to a TAFKAL80ETC concert", StringComparison.InvariantCulture))
            {
                return ItemType.Timed;
            }

            return ItemType.Standard;
        }
    }
}