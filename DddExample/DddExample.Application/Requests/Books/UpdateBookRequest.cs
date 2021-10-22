using System.Collections.Generic;

namespace DddExample.Application.Requests.Books
{
    public class UpdateBookRequest
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public string Description { get; set; }
        
        public int TypeId { get; set; }

        public IEnumerable<UpdateChapterRequest> Chapters { get; set; }
    }
}