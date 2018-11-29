namespace GildedRose.Tests
{
    using GildedRose.Console;
    using GildedRose.Models;
    using Xunit;

    /// <summary>
    /// Test fixture for the <see cref="Program"/> class.
    /// </summary>
    public class ProgramTests
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
        /// the age increases
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
        /// above 50
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

        /// <summary>
        /// Tests that <see cref="Program.UpdateQuality(Item)"/> does not modify legendary items.
        /// </summary>
        [Fact]
        public void UpdateQuality_LegendaryItem_NotChanged()
        {
            // Arrange
            const string LegendaryItemName = "Sulfuras, Hand of Ragnaros";
            const int StartingQuality = 99;
            const int StartingSellIn = 20;

            var item = new Item
            {
                Name = "Sulfuras, Hand of Ragnaros",
                Quality = StartingQuality,
                SellIn = StartingSellIn
            };

            // Act
            Program.UpdateQuality(item);

            // Assert
            Assert.Equal(LegendaryItemName, item.Name);
            Assert.Equal(StartingQuality, item.Quality);
            Assert.Equal(StartingSellIn, item.SellIn);
        }

        /// <summary>
        /// Tests that <see cref="Program.UpdateQuality(Item)"/> sets <see cref="Item.Quality"/> to 0
        /// for backstage passes are the sell by date has expired
        /// </summary>
        /// <param name="sellIn">Test data for <see cref="Item.SellIn"/></param>
        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        [InlineData(-100)]
        public void UpdateQuality_BackstagePassNegativeSellIn_SetsQuality0(int sellIn)
        {
            // Arrange
            var item = new Item
            {
                Name = "Backstage passes to a TAFKAL80ETC concert",
                Quality = 30,
                SellIn = sellIn
            };

            // Act
            Program.UpdateQuality(item);

            // Assert
            Assert.Equal(0, item.Quality);
        }

        /// <summary>
        /// Tests that <see cref="Program.UpdateQuality(Item)"/> increases <see cref="Item.Quality"/> by the given amount when
        /// <see cref="Item.SellIn"/> is between 0 and 10
        /// <example>
        ///   <list type="bullet">
        ///     <value>0-5 - Increases by 3</value>
        ///     <value>5-10 - Increases by 2</value>
        ///   </list>
        /// </example>
        /// </summary>
        /// <param name="sellIn">Test data for <see cref="Item.SellIn"/></param>
        /// <param name="quality">Test data for <see cref="Item.Quality"/></param>
        /// <param name="expectedIncrease">The expected quality increase</param>
        [Theory]
        [InlineData(10, 10, 2)]
        [InlineData(8, 20, 2)]
        [InlineData(6, 0, 2)]
        [InlineData(5, 10, 3)]
        [InlineData(3, 20, 3)]
        [InlineData(1, 0, 3)]
        public void UpdateQuality_BackstagePassSellInLessThan10_QualityIncrease2(int sellIn, int quality, int expectedIncrease)
        {
            // Arrange
            var item = new Item
            {
                Name = "Backstage passes to a TAFKAL80ETC concert",
                Quality = quality,
                SellIn = sellIn
            };

            // Act
            Program.UpdateQuality(item);

            // Assert
            Assert.Equal(quality + expectedIncrease, item.Quality);
        }

        /// <summary>
        /// Tests that <see cref="Program.UpdateQuality(Item)"/> decreases <see cref="Item.Quality"/> by 2 for Conjured items
        /// </summary>
        /// <param name="quality">Test data for <see cref="Item.Quality"/></param>
        [Theory(Skip = "Conjured items are not yet implemented")]
        [InlineData(10)]
        [InlineData(5)]
        [InlineData(0)]
        public void UpdateQuality_ConjuredItem_QualityDecreasesBy2(int quality)
        {
            // Arrange
            var item = new Item
            {
                Name = "Conjured Armour of Perception +1",
                Quality = quality,
                SellIn = 10
            };

            // Act
            Program.UpdateQuality(item);

            // Assert
            Assert.Equal(quality - 2, item.Quality);
        }
    }
}