using System.Threading.Tasks;

namespace Resources.GoogleCloud;

public interface IGoogleCloudStorage
{
    Task<string> UploadImageAsync(byte[] imageBytes);
}
