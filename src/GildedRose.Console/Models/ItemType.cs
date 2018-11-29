namespace GildedRose.Console.Models
{
    /// <summary>
    /// Indicates the inventory update strategy for a given <see cref="Item"/>.
    /// </summary>
    public enum ItemType
    {
        /// <summary>
        /// Item follows standard age and quality rules.
        /// </summary>
        Standard,

        /// <summary>
        /// Item quality increases as age increases.
        /// </summary>
        Maturing,

        /// <summary>
        /// Item quality and sell in does not change over time.
        /// </summary>
        Legendary,

        /// <summary>
        /// Item quality change rate increases up as sell in value decreases.
        /// </summary>
        Timed,

        /// <summary>
        /// Item quality degrades twice as fast as <see cref="Standard"/> items.
        /// </summary>
        Conjured
    }
}