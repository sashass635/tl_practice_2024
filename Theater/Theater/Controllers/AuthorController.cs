using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Theater.Contracts.Requests;
using Theater.Mappers;

namespace Theater.Controllers
{
    [ApiController]
    [Route( "api/author" )]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorController( IAuthorRepository authorRepository )
        {
            _authorRepository = authorRepository;
        }

        [HttpGet, Route( "{id:int}" )]
        public IActionResult GetAuthor( [FromRoute] int id )
        {
            Author author = _authorRepository.GetById( id );
            if ( author == null )
            {
                return NotFound( $"There is no author with such id = {id}" );
            }

            CreateAuthorRequest authorDTO = AuthorMapper.ToAuthorDTOMap( author );

            return Ok( authorDTO );
        }

        [HttpPost]
        public IActionResult CreateAuthor( [FromBody] CreateAuthorRequest author )
        {
            Author newAuthor = new Author( author.Name, author.DateBirth ); ;
            _authorRepository.Add( newAuthor );
            CreateAuthorRequest authorDTO = AuthorMapper.ToAuthorDTOMap( newAuthor );

            return Ok( authorDTO );
        }

        [HttpDelete, Route( "{id:int}" )]
        public IActionResult DeleteAuthor( [FromRoute] int id )
        {
            Author author = _authorRepository.GetById( id );
            if ( author == null )
            {
                return NotFound( $"Author with such id = {id} does not exist" );
            }

            _authorRepository.Remove( author );

            return Ok();
        }
    }
}