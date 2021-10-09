using System.Threading.Tasks;

namespace Currencies
{
    public interface ICurrenciesApiCacheService : ICurrenciesApi
    {
        Task Initialize();
    }
}
