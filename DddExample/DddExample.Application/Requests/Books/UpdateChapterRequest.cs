namespace DddExample.Application.Requests.Books
{
    public class UpdateChapterRequest
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public string Description { get; set; }
    }
}