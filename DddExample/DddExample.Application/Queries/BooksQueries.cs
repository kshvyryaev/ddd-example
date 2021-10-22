using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DddExample.Application.Options;
using DddExample.Application.Responses;
using DddExample.Domain.Aggregates.BookAggregate;
using DddExample.Domain.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace DddExample.Application.Queries
{
    public class BooksQueries : IBooksQueries
    {
        private readonly DatabaseOptions _options;

        public BooksQueries(IOptions<DatabaseOptions> options)
        {
            _options = options.Value;
        }
        
        public async Task<BookResponse> GetBookAsync(int id)
        {
            await using var connection = new SqlConnection(_options.ConnectionString);

            connection.Open();

            var book = await connection.QueryFirstOrDefaultAsync<BookResponse>(
                @"SELECT B.[Id]
                            ,B.[Name]
                            ,B.[Description]
                            ,BT.[Id] AS TypeId
                            ,BT.[Name] AS TypeName
                        FROM [dbo].[Books] AS B
                        INNER JOIN [dbo].[BookTypes] as BT ON B.[TypeId] = BT.[Id]
                        WHERE B.[Id] = @id AND B.[IsDeleted] = 0",
                new { id });

            if (book == null)
                throw new NotFoundException(nameof(Book), id);

            var chaptersQuery = await connection.QueryAsync<ChapterResponse>(
                @"SELECT C.[Id]
                            ,C.[Name]
                            ,C.[Description]
                        FROM [dbo].[Chapters] AS C
                        WHERE C.[BookId] = @id AND C.[IsDeleted] = 0",
                new { id });

            book.Chapters = chaptersQuery.ToList();

            return book;
        }
    }
}