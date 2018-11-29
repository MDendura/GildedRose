namespace GildedRose.Console
{
    using System;
    using GildedRose.Models;

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
            switch (item)
            {
                case Item i when i.Quality > 50:
                    return ItemType.Legendary;

                case Item i when i.Name.ToLowerInvariant().Contains("conjured"):
                    return ItemType.Conjured;

                case Item i when i.Name.Equals("Aged Brie", StringComparison.InvariantCulture):
                    return ItemType.Maturing;

                case Item i when i.Name.Equals("Backstage passes to a TAFKAL80ETC concert", StringComparison.InvariantCulture):
                    return ItemType.Timed;

                default:
                    return ItemType.Standard;
            }
        }
    }
}