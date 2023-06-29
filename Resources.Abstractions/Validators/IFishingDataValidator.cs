using System;
using Resources.Abstractions.Models;

namespace FishOnTheGoApi.Services;

public interface IFishingDataValidator
{
    bool IsValid(FishingData fishingData);
}
