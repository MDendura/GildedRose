using Xunit;

namespace GildedRose.Tests
{
    using GildedRose.Console;

    public class TestAssemblyTests
    {
        /// <summary>
        /// Tests that <see cref="Program.UpdateQuality(Item)"/> reduces an item's quality by
        /// 1 if the sell by date has not yet passed
        /// </summary>
        /// <param name="sellIn">Test data for <see cref="Item.SellIn"/></param>
        /// <param name="startingQuality">Test data for <see cref="Item.Quality"/></param>
        [Theory]
        [InlineData(1, 2)]
        [InlineData(10, 10)]
        [InlineData(int.MaxValue, 5)]
        public void UpdateQuality_BeforeSellByDate_DecrementsQualityBy1(int sellIn, int startingQuality)
        {
            // Arrange
            var item = new Item
            {
                Name = "Bag of holding",
                Quality = startingQuality,
                SellIn = sellIn
            };

            // Act
            Program.UpdateQuality(item);

            // Assert
            Assert.Equal(startingQuality - 1, item.Quality);
        }

        /// <summary>
        /// Tests that <see cref="Program.UpdateQuality(Item)"/> reduces an item's quality by
        /// 2 if the sell by date has passed
        /// </summary>
        /// <param name="sellIn">Test data for <see cref="Item.SellIn"/></param>
        /// <param name="startingQuality">Test data for <see cref="Item.Quality"/></param>
        [Theory]
        [InlineData(-1, 2)]
        [InlineData(-10, 10)]
        [InlineData(-50, 5)]
        public void UpdateQuality_AfterSellByDate_DecrementsQualityBy2(int sellIn, int startingQuality)
        {
            // Arrange
            var item = new Item
            {
                Name = "Past its best",
                Quality = startingQuality,
                SellIn = sellIn
            };

            // Act
            Program.UpdateQuality(item);

            // Assert
            Assert.Equal(startingQuality - 2, item.Quality);
        }
    }
}