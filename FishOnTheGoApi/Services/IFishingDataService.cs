using System.Threading.Tasks;
using Resources.Abstractions.Models;

namespace FishOnTheGoApi.Services;

public interface IFishingDataService
{
    Task<FishingData> SaveFishingDataAsync(FishingData fishingData, byte[] imageBytes);
}
