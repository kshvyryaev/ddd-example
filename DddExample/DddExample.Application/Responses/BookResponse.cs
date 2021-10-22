using System.Collections.Generic;

namespace DddExample.Application.Responses
{
    public class BookResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int TypeId { get; set; }

        public string TypeName { get; set; }

        public IEnumerable<ChapterResponse> Chapters { get; set; }
    }
}