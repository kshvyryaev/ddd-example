using System;
using System.Collections.Generic;
using DddExample.Application.CommandRequests.Books;
using MediatR;

namespace DddExample.Application.Commands.Books
{
    public class UpdateBookCommand : IRequest
    {
        public UpdateBookCommand(
            int id,
            string name,
            string description,
            int typeId,
            IEnumerable<UpdateChapterRequest> chapters)
        {
            Id = id;
            Name = name;
            Description = description;
            TypeId = typeId;
            Chapters = chapters ?? Array.Empty<UpdateChapterRequest>();
        }

        public int Id { get; }
        
        public string Name { get; }
        
        public string Description { get; }
        
        public int TypeId { get; }
        
        public IEnumerable<UpdateChapterRequest> Chapters { get; }
    }
}