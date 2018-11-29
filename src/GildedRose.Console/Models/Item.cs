namespace GildedRose.Models
{
    /// <summary>
    /// Inventory item sold by the Gilded Rose.
    /// </summary>
    public class Item
    {
        /// <summary>
        /// Gets or sets the name of the item
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the quality level of the item
        /// </summary>
        public int Quality { get; set; }

        /// <summary>
        /// Gets or sets the number of days remaining to sell the item
        /// </summary>
        public int SellIn { get; set; }
    }
}