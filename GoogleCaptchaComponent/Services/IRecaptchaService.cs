using System.Threading.Tasks;

namespace GoogleCaptchaComponent.Services;

public interface IRecaptchaService
{
    Task ReloadAsync();
}