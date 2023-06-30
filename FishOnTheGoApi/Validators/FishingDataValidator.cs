using System;
using Resources.Abstractions;

namespace FishOnTheGoApi.Validators;

public class FishingDataValidator : IFishingDataValidator
{
    public bool IsValid(FishingData fishingData)
    {
        if (fishingData == null)
        {
            throw new ArgumentNullException(nameof(fishingData));
        }

        if (fishingData.ImageUrl == null)
        {
            throw new ArgumentNullException(nameof(fishingData.ImageUrl));
        }

        if (fishingData.Method == null)
        {
            throw new ArgumentNullException(nameof(fishingData.Method));
        }

        if (fishingData.Bait == null)
        {
            throw new ArgumentNullException(nameof(fishingData.Bait));
        }

        if (fishingData.FishSpecies == null)
        {
            throw new ArgumentNullException(nameof(fishingData.FishSpecies));
        }

        return true;
    }
}
