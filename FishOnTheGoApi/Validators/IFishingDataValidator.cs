using Resources.Abstractions;

namespace FishOnTheGoApi.Validators;

public interface IFishingDataValidator
{
    bool IsValid(FishingData fishingData);
}
