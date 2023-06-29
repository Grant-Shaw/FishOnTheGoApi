using System.Threading.Tasks;
using Resources.Abstractions.Models;
using Resources.Database;
using Resources.GoogleCloud;

namespace FishOnTheGoApi.Services
{
    public class FishingDataService : IFishingDataService
    {
        private readonly IDBService _dbService;
        private readonly IGoogleCloudStorage _googleCloudStorage;

        public FishingDataService(IDBService dbService, IGoogleCloudStorage googleCloudStorage)
        {
            _dbService = dbService;
            _googleCloudStorage = googleCloudStorage;
        }

        public async Task<FishingData> SaveFishingDataAsync(FishingData fishingData, byte[] imageBytes)
        {
            // Upload the image to Google Cloud Storage
            string imageUrl = await _googleCloudStorage.UploadImageAsync(imageBytes);

            // Set the image URL in the fishingData object
            fishingData.ImageUrl = imageUrl;

            // Save the fishingData to the database
            await _dbService.SaveFishingDataAsync(fishingData);

            return fishingData;
        }
    }
}

