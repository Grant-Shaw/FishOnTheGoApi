using System;
using System.Threading.Tasks;
using Resources.Abstractions;
using Resources.Database;
using Resources.GoogleCloud;
using FishOnTheGoApi.Validators;

namespace FishOnTheGoApi.Services
{
    public class FishingDataService : IFishingDataService
    {
        private readonly IDBService _dbService;
        private readonly IGoogleCloudStorage _googleCloudStorage;
        private readonly IFishingDataValidator _fishingDataValidator;


        public FishingDataService(IDBService dbService, IGoogleCloudStorage googleCloudStorage, IFishingDataValidator fishingDataValidator)
        {
            _dbService = dbService;
            _googleCloudStorage = googleCloudStorage;
            _fishingDataValidator = fishingDataValidator;
        }

        public async Task<FishingData> SaveFishingDataAsync(FishingData fishingData, byte[] imageBytes)
        {
            try
            {
                //perhaps move this to the GraphQL controller
                if (!_fishingDataValidator.IsValid(fishingData))
                {
                    throw new ArgumentException("Invalid FishingData provided.");
                }

                // Upload the image to Google Cloud Storage
                var imageUrl = await _googleCloudStorage.UploadImageAsync(imageBytes);

                if (string.IsNullOrEmpty(imageUrl))
                {
                    throw new Exception("Failed to upload image to Google Cloud Storage.");
                }

                // Set the image URL in the fishingData object
                fishingData.ImageUrl = imageUrl;

                // Save the fishingData to the database
                int saveResult = await _dbService.SaveFishingDataAsync(fishingData);

                if (saveResult <= 0)
                {
                    throw new Exception("Failed to save FishingData to the database.");
                }

                return fishingData;
            }
            catch (Exception ex)
            {
                // Handle the exception here, log or perform any necessary actions
                throw;
            }
        }
    }
}

