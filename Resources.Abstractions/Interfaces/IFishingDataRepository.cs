using System;
using System.Collections.Generic;
using Resources.Abstractions.Models;

namespace Resources.Abstractions
{
    public interface IFishingDataRepository
    {
        IEnumerable<FishingData> GetFishingData();
        void SaveFishingData(FishingData data);
    }
}

