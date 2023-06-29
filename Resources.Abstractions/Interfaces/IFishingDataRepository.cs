using System.Collections.Generic;

namespace Resources.Abstractions
{
    public interface IFishingDataRepository
    {
        IEnumerable<FishingData> GetFishingData();
        void SaveFishingData(FishingData data);
    }
}

