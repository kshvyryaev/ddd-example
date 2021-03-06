using System.Threading.Tasks;
using DddExample.Application.Commands.Books;
using DddExample.Application.Queries;
using DddExample.Application.Requests.Books;
using DddExample.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DddExample.Api.Controllers
{
    [Route("books")]
    public class BooksController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IBooksQueries _queries;

        public BooksController(IMediator mediator, IBooksQueries queries)
        {
            _mediator = mediator;
            _queries = queries;
        }

        /// <summary>
        /// Метод создания книги
        /// </summary>
        /// <param name="request">Запрос на создание книги</param>
        /// <returns>Id книги</returns>
        [HttpPost]
        public async Task<ActionResult<int>> CreateBookAsync([FromBody] CreateBookRequest request)
        {
            var command = new CreateBookCommand(request.Name, request.Description, request.TypeId, request.Chapters);
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        
        /// <summary>
        /// Метод изменения книги
        /// </summary>
        /// <param name="request">Запрос на изменение книги</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult> UpdateBookAsync([FromBody] UpdateBookRequest request)
        {
            var command = new UpdateBookCommand(request.Id, request.Name, request.Description, request.TypeId, request.Chapters);
            await _mediator.Send(command);
            return Ok();
        }
        
        /// <summary>
        /// Метод удаления книги
        /// </summary>
        /// <param name="id">Id книги</param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteBookAsync([FromRoute] int id)
        {
            var command = new DeleteBookCommand(id);
            await _mediator.Send(command);
            return Ok();
        }

        /// <summary>
        /// Метод получения книги
        /// </summary>
        /// <param name="id">Id книги</param>
        /// <returns>Книга</returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<BookResponse>> GetBookAsync([FromRoute] int id)
        {
            var response = await _queries.GetBookAsync(id);
            return Ok(response);
        }
    }
}