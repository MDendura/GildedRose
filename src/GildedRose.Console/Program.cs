namespace GildedRose.Console
{
    using System;
    using System.Collections.Generic;
    using GildedRose.Console.Models;

    /// <summary>
    /// Application to update the inventory of the Gilded Rose.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Collection of items stocked by the Gilded Rose.
        /// </summary>
        private static IList<Item> Items = new List<Item>
        {
            new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 },
            new Item { Name = "Aged Brie", SellIn = 2, Quality = 0 },
            new Item { Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7 },
            new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 },
            new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15, Quality = 20 },
            new Item { Name = "Conjured Mana Cake", SellIn = 3, Quality = 6 }
        };

        /// <summary>
        /// Application entry point.
        /// </summary>
        public static void Main()
        {
            Console.WriteLine("OMGHAI!");

            foreach (var item in Items)
            {
                UpdateItem(item);
            }

            Console.ReadKey();
        }

        /// <summary>
        /// Updates the <see cref="Item.Quality"/> and <see cref="Item.SellIn"/> values of the given item.
        /// </summary>
        /// <param name="item">Item to be updated.</param>
        public static void UpdateItem(Item item)
        {
            switch (item.GetItemType())
            {
                case ItemType.Legendary:
                    // Legendary items should not be modified
                    return;

                case ItemType.Standard:
                    UpdateStandardItem(item);
                    break;

                case ItemType.Maturing:
                    UpdateMaturingItem(item);
                    break;

                case ItemType.Timed:
                    UpdateTimedItem(item);
                    break;

                case ItemType.Conjured:
                    UpdateConjuredItem(item);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(item), "Item type could not be determined or was invalid.");
            }
        }

        /// <summary>
        /// Updates the <see cref="Item.Quality"/> and <see cref="Item.SellIn"/> values of the given standard item.
        /// </summary>
        /// <param name="item">Standard item to be updated.</param>
        public static void UpdateStandardItem(Item item)
        {
            if (item.Quality > 0)
            {
                item.Quality = item.Quality - 1;
                item.SellIn = item.SellIn - 1;
            }

            if (item.SellIn < 0 && item.Quality > 0)
            {
                item.Quality = item.Quality - 1;
            }
        }

        /// <summary>
        /// Updates the <see cref="Item.Quality"/> and <see cref="Item.SellIn"/> values of the given maturing item.
        /// </summary>
        /// <param name="item">Maturing item to be updated.</param>
        public static void UpdateMaturingItem(Item item)
        {
            if (item.Quality < 50)
            {
                item.Quality = item.Quality + 1;
                item.SellIn = item.SellIn - 1;
            }

            if (item.SellIn < 0 && item.Quality < 50)
            {
                item.Quality = item.Quality + 1;
            }
        }

        /// <summary>
        /// Updates the <see cref="Item.Quality"/> and <see cref="Item.SellIn"/> values of the given timed item.
        /// </summary>
        /// <param name="item">Timed item to be updated.</param>
        public static void UpdateTimedItem(Item item)
        {
            if (item.Quality < 50)
            {
                item.Quality = item.Quality + 1;

                if (item.SellIn < 11)
                {
                    if (item.Quality < 50)
                    {
                        item.Quality = item.Quality + 1;
                    }
                }

                if (item.SellIn < 6)
                {
                    if (item.Quality < 50)
                    {
                        item.Quality = item.Quality + 1;
                    }
                }
            }

            item.SellIn = item.SellIn - 1;

            if (item.SellIn < 0)
            {
                item.Quality = item.Quality - item.Quality;
            }
        }

        /// <summary>
        /// Updates the <see cref="Item.Quality"/> and <see cref="Item.SellIn"/> values of the given conjured item.
        /// </summary>
        /// <param name="item">Conjured item to be updated.</param>
        public static void UpdateConjuredItem(Item item)
        {
            if (item.Quality > 0)
            {
                item.Quality = item.Quality - 2;
                item.SellIn = item.SellIn - 1;
            }

            if (item.SellIn < 0 && item.Quality > 0)
            {
                item.Quality = item.Quality - 1;
            }
        }
    }
}
