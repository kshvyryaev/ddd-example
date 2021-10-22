using System;
using System.Collections.Generic;
using DddExample.Application.CommandRequests.Books;
using MediatR;

namespace DddExample.Application.Commands.Books
{
    public class CreateBookCommand : IRequest<int>
    {
        public CreateBookCommand(
            string name,
            string description,
            int typeId,
            IEnumerable<CreateChapterRequest> chapters)
        {
            Name = name;
            Description = description;
            TypeId = typeId;
            Chapters = chapters ?? Array.Empty<CreateChapterRequest>();
        }
        
        public string Name { get; }
        
        public string Description { get; }
        
        public int TypeId { get; }
        
        public IEnumerable<CreateChapterRequest> Chapters { get; }
    }
}