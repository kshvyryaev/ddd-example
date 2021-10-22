using System.Collections.Generic;

namespace DddExample.Application.Requests.Books
{
    public class CreateBookRequest
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int TypeId { get; set; }

        public IEnumerable<CreateChapterRequest> Chapters { get; set; }
    }
}