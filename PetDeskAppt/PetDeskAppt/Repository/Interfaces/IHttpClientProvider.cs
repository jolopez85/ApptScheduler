using System.Collections.Generic;
using System.Threading.Tasks;

namespace PetDeskAppt.Repository.Interfaces
{
    public interface IHttpClientProvider
    {
        string BaseUrl { get; }

        Task<IEnumerable<T>> GetAll<T>(string url);

        Task<T> GetById<T>(string url, string id);

        Task<T> Post<T, U>(string url, U request);

    }
}
