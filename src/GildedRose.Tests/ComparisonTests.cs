namespace GildedRose.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using GildedRose.Console;
    using GildedRose.Models;
    using Xunit;

    /// <summary>
    /// Test fixture to compare old and new implementations in <see cref="Program"/>.
    /// </summary>
    public class ComparisonTests
    {
        /// <summary>
        /// Test to ensure old and new implementations produce the same results.
        /// </summary>
        /// <remarks>
        /// They don't, of course, because the old implementation doesn't have the new logic for
        /// conjured items.
        /// </remarks>
        [Fact]
        public void CompareData()
        {
            // Arrange
            IEnumerable<Item> items1 = new List<Item>
            {
                new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 },
                new Item { Name = "Aged Brie", SellIn = 2, Quality = 0 },
                new Item { Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7 },
                new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 },
                new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15, Quality = 20 },
                new Item { Name = "Conjured Mana Cake", SellIn = 3, Quality = 6 }
            };

            IEnumerable<Item> items2 = new List<Item>
            {
                new Item { Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20 },
                new Item { Name = "Aged Brie", SellIn = 2, Quality = 0 },
                new Item { Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7 },
                new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 },
                new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15, Quality = 20 },
                new Item { Name = "Conjured Mana Cake", SellIn = 3, Quality = 6 }
            };

            // Act
            foreach (var item in items1)
            {
                Program.UpdateQuality(item);
            }

            foreach (var item in items2)
            {
                Program.UpdateItemQuality(item);
            }

            // Assert
            Assert.Equal(items1.Count(), items2.Count());

            for (var i = 0; i < items1.Count(); i++)
            {
                var item1 = items1.ElementAt(i);
                var item2 = items2.ElementAt(i);

                Assert.Equal(item1.Name, item2.Name);
                Assert.Equal(item1.Quality, item2.Quality);
                Assert.Equal(item1.SellIn, item2.SellIn);
            }
        }
    }
}