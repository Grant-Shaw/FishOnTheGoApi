using System;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using Resources.Abstractions;
using Resources.Database;
using Resources.GoogleCloud;


namespace FishOnTheGoApi.Services.Tests
{
    [TestFixture]
    public class FishingDataServiceTests
    {
        private IDBService _dbService;
        private IGoogleCloudStorage _googleCloudStorage;
        private IFishingDataValidator _fishingDataValidator;
        private FishingDataService _fishingDataService;

        [SetUp]
        public void Setup()
        {
            _dbService = Substitute.For<IDBService>();
            _googleCloudStorage = Substitute.For<IGoogleCloudStorage>();
            _fishingDataValidator = Substitute.For<IFishingDataValidator>();
            _fishingDataService = new FishingDataService(_dbService, _googleCloudStorage, _fishingDataValidator);
        }

        [Test]
        public async Task SaveFishingDataAsync_ValidData_SuccessfullySaved()
        {
            // Arrange
            var fishingData = new FishingData
            {
                // Set valid fishing data properties
                Method = "Casting",
                Bait = "Worm",
                FishSpecies = "Trout"
            };
            var imageBytes = new byte[] { 1, 2, 3 }; // Mock image byte data
            var imageUrl = "https://example.com/image.jpg";

            _fishingDataValidator.IsValid(fishingData).Returns(true);
            _googleCloudStorage.UploadImageAsync(imageBytes).Returns(imageUrl);
            _dbService.SaveFishingDataAsync(fishingData).Returns(1);

            // Act
            var result = await _fishingDataService.SaveFishingDataAsync(fishingData, imageBytes);

            // Assert
            Assert.That(result, Is.EqualTo(fishingData));
            Assert.That(fishingData.ImageUrl, Is.EqualTo(imageUrl));
            await _dbService.Received(1).SaveFishingDataAsync(fishingData);
        }

        [Test]
        public void SaveFishingDataAsync_InvalidData_ThrowsArgumentException()
        {
            // Arrange
            var fishingData = new FishingData(); // Invalid fishing data with null properties
            var imageBytes = new byte[] { 1, 2, 3 }; // Mock image byte data

            _fishingDataValidator.IsValid(fishingData).Returns(false);

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(() => _fishingDataService.SaveFishingDataAsync(fishingData, imageBytes));
            _dbService.DidNotReceive().SaveFishingDataAsync(Arg.Any<FishingData>());
        }

        [Test]
        public void SaveFishingDataAsync_ImageUploadFailed_ThrowsException()
        {
            // Arrange
            var fishingData = new FishingData
            {
                // Set valid fishing data properties
                Method = "Casting",
                Bait = "Worm",
                FishSpecies = "Trout"
            };
            var imageBytes = new byte[] { 1, 2, 3 }; // Mock image byte data

            _fishingDataValidator.IsValid(fishingData).Returns(true);
            _googleCloudStorage.UploadImageAsync(imageBytes).Returns("");

            // Act & Assert
            Assert.ThrowsAsync<Exception>(() => _fishingDataService.SaveFishingDataAsync(fishingData, imageBytes));
            _dbService.DidNotReceive().SaveFishingDataAsync(Arg.Any<FishingData>());
        }

        [Test]
        public void SaveFishingDataAsync_DatabaseSaveFailed_ThrowsException()
        {
            // Arrange
            var fishingData = new FishingData
            {
                // Set valid fishing data properties
                Method = "Casting",
                Bait = "Worm",
                FishSpecies = "Trout"
            };
            var imageBytes = new byte[] { 1, 2, 3 }; // Mock image byte data
            var imageUrl = "https://example.com/image.jpg";

            _fishingDataValidator.IsValid(fishingData).Returns(true);
            _googleCloudStorage.UploadImageAsync(imageBytes).Returns(imageUrl);
            _dbService.SaveFishingDataAsync(fishingData).Returns(0); // Mock database save failure

            // Act & Assert
            Assert.ThrowsAsync<Exception>(() => _fishingDataService.SaveFishingDataAsync(fishingData, imageBytes));
            _dbService.Received(1).SaveFishingDataAsync(fishingData);
        }
    }
}
