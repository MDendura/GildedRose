namespace GildedRose.Tests
{
    using GildedRose.Console;
    using GildedRose.Models;
    using Xunit;

    /// <summary>
    /// Test fixture for the <see cref="ItemExtensions"/> class.
    /// </summary>
    public class ItemExtensionsTests
    {
        /// <summary>
        /// Tests that <see cref="ItemExtensions.GetItemType(Item)"/> assigns high quality items a type of <see cref="ItemType.Legendary"/>.
        /// </summary>
        [Fact]
        public void GetItemType_ItemWithHighQuality_ReturnsLegendary()
        {
            // Arrange
            var item = new Item
            {
                Name = "Excalibur",
                Quality = 90,
                SellIn = 10
            };

            // Act
            var result = item.GetItemType();

            // Assert
            Assert.Equal(ItemType.Legendary, result);
        }

        /// <summary>
        /// Tests that <see cref="ItemExtensions.GetItemType(Item)"/> assigns items with "conjured" in the name
        /// a type of <see cref="ItemType.Legendary"/>.
        /// </summary>
        [Fact]
        public void GetItemType_ItemNameContainsConjured_ReturnsConjured()
        {
            // Arrange
            var item = new Item
            {
                Name = "Conjured Armour of Defense +1",
                SellIn = 5,
                Quality = 10
            };

            // Act
            var result = item.GetItemType();

            // Assert
            Assert.Equal(ItemType.Conjured, result);
        }
    
        /// <summary>
        /// Tests that <see cref="ItemExtensions.GetItemType(Item)"/> assigns "Aged Brie" a type of <see cref="ItemType.Maturing"/>.
        /// </summary>
        [Fact]
        public void GetItemType_AgedBrie_ReturnsMaturing()
        {
            // Arrange
            var item = new Item
            {
                Name = "Aged Brie",
                Quality = 11,
                SellIn = 10
            };

            // Act
            var result = item.GetItemType();

            // Assert
            Assert.Equal(ItemType.Maturing, result);
        }

        /// <summary>
        /// Tests that <see cref="ItemExtensions.GetItemType(Item)"/> assigns "Backstage passes..." a type of <see cref="ItemType.Timed"/>.
        /// </summary>
        [Fact]
        public void GetItemType_BackstagePass_ReturnsTimed()
        {
            // Arrange
            var item = new Item
            {
                Name = "Backstage passes to a TAFKAL80ETC concert",
                Quality = 11,
                SellIn = 10
            };

            // Act
            var result = item.GetItemType();

            // Assert
            Assert.Equal(ItemType.Timed, result);
        }

        /// <summary>
        /// Tests that <see cref="ItemExtensions.GetItemType(Item)"/> assigns standard items a type of <see cref="ItemType.Standard"/>.
        /// </summary>
        [Fact]
        public void GetItemType_DefaultItemType_ReturnsStandard()
        {
            // Arrange
            var item = new Item
            {
                Name = "Long sword",
                Quality = 3,
                SellIn = 5
            };

            // Act
            var result = item.GetItemType();

            // Assert
            Assert.Equal(ItemType.Standard, result);
        }
    }
}