using System.Threading.Tasks;
using DddExample.Application.Responses;

namespace DddExample.Application.Queries
{
    public interface IBooksQueries
    {
        Task<BookResponse> GetBookAsync(int id);
    }
}