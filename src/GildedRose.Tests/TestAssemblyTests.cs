namespace GildedRose.Tests
{
    using GildedRose.Console;
    using Xunit;

    public class TestAssemblyTests
    {
        /// <summary>
        /// Tests that <see cref="Program.UpdateQuality(Item)"/> reduces an item's quality by
        /// 1 if the sell by date has not yet passed
        /// </summary>
        /// <param name="sellIn">Test data for <see cref="Item.SellIn"/></param>
        /// <param name="quality">Test data for <see cref="Item.Quality"/></param>
        [Theory]
        [InlineData(1, 2)]
        [InlineData(10, 10)]
        [InlineData(int.MaxValue, 5)]
        public void UpdateQuality_BeforeSellByDate_DecrementsQualityBy1(int sellIn, int quality)
        {
            // Arrange
            var item = new Item
            {
                Name = "Bag of holding",
                Quality = quality,
                SellIn = sellIn
            };

            // Act
            Program.UpdateQuality(item);

            // Assert
            Assert.Equal(quality - 1, item.Quality);
        }

        /// <summary>
        /// Tests that <see cref="Program.UpdateQuality(Item)"/> reduces an item's quality by
        /// 2 if the sell by date has passed
        /// </summary>
        /// <param name="sellIn">Test data for <see cref="Item.SellIn"/></param>
        /// <param name="quality">Test data for <see cref="Item.Quality"/></param>
        [Theory]
        [InlineData(-1, 2)]
        [InlineData(-10, 10)]
        [InlineData(-50, 5)]
        public void UpdateQuality_AfterSellByDate_DecrementsQualityBy2(int sellIn, int quality)
        {
            // Arrange
            var item = new Item
            {
                Name = "Past its best",
                Quality = quality,
                SellIn = sellIn
            };

            // Act
            Program.UpdateQuality(item);

            // Assert
            Assert.Equal(quality - 2, item.Quality);
        }

        /// <summary>
        /// Tests that <see cref="Program.UpdateQuality(Item)"/> does not set the quality to a negative value.
        /// </summary>
        /// <param name="sellIn">Test data for <see cref="Item.SellIn"/></param>
        [Theory]
        [InlineData(2)]
        [InlineData(-2)]
        public void UpdateQuality_Quality_NotChangedToNegative(int sellIn)
        {
            // Arrange
            var item = new Item
            {
                Name = "Poor quality item",
                Quality = 0,
                SellIn = sellIn
            };

            // Act
            Program.UpdateQuality(item);

            // Assert
            Assert.True(item.Quality >= 0);
        }

        /// <summary>
        /// Tests that <see cref="Program.UpdateQuality(Item)"/> increases the quality for Aged Brie as
        /// the age increases.
        /// </summary>
        /// <param name="sellIn">Test data for <see cref="Item.SellIn"/></param>
        /// <param name="quality">Test data for <see cref="Item.Quality"/></param>
        [Theory]
        [InlineData(2, 5)]
        [InlineData(0, 10)]
        [InlineData(-1, 2)]
        [InlineData(-10, 10)]
        [InlineData(-50, 5)]
        public void UpdateQuality_AgedBrie_QualityIncreaseWithAge(int sellIn, int quality)
        {
            // Arrange
            var item = new Item
            {
                Name = "Aged Brie",
                SellIn = sellIn,
                Quality = quality
            };

            // Act
            Program.UpdateQuality(item);

            // Assert
            Assert.True(item.SellIn < sellIn);
            Assert.True(item.Quality > quality);
        }

        /// <summary>
        /// Tests that <see cref="Program.UpdateQuality(Item)"/> does not increase standard item quality
        /// above 50.
        /// </summary>
        /// <param name="sellIn">Test data for <see cref="Item.SellIn"/></param>
        [Theory]
        [InlineData(0)]
        [InlineData(10)]
        [InlineData(-10)]
        public void UpdateQuality_StandardItem_QualityNotChangedAbove50(int sellIn)
        {
            // Arrange
            var item = new Item
            {
                Name = "Standard Item",
                Quality = 50,
                SellIn = sellIn
            };

            // Act
            Program.UpdateQuality(item);

            // Assert
            Assert.True(item.Quality <= 50);
        }
    }
}