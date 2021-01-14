using System.Threading.Tasks;

namespace URLShortenerApi.Helpers
{
    public interface IURLHelper
    {
        Task<int> Decode(string Url);
        Task<string> Encode(int Key);
        Task<string> GetTitle(string url);
    }
}