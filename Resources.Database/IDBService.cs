using System;
using System.Threading.Tasks;
using Resources.Abstractions.Models;

namespace Resources.Database
{
    public interface IDBService
    {
        Task<int> SaveFishingDataAsync(FishingData fishData);
        Task<FishingData> GetFishDataAsync(int id);
        Task<bool> DeleteFishDataAsync(int id);
    }
}

