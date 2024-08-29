using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Theater.Contracts.Requests;
using Theater.Mappers;

namespace Theater.Controllers
{
    [ApiController]
    [Route( "api/composition" )]
    public class CompositionController : ControllerBase
    {
        private readonly ICompositionRepository _compositionRepository;

        public CompositionController( ICompositionRepository compositionRepository )
        {
            _compositionRepository = compositionRepository;
        }

        [HttpGet, Route( "{id:int}" )]
        public IActionResult GetComposition( [FromRoute] int id )
        {
            Composition composition = _compositionRepository.GetById( id );
            if ( composition == null )
            {
                return NotFound( $"There is no composition with such id = {id}" );
            }

            CreateCompositionRequest compositionDTO = CompositionMapper.ToCompositionDTOMap( composition );

            return Ok( compositionDTO );
        }

        [HttpGet, Route( "author/{authorId:int}" )]
        public IActionResult GetByAuthorId( [FromRoute] int authorId )
        {
            List<Composition> compositions = _compositionRepository.GetByAuthorId( authorId );
            List<CreateCompositionRequest> compositionsDTO = compositions.Select( CompositionMapper.ToCompositionDTOMap ).ToList();

            return Ok( compositionsDTO );
        }

        [HttpPost]
        public IActionResult CreateComposition( [FromBody] CreateCompositionRequest composition )
        {
            Composition newComposition = new Composition( composition.Name, composition.ShortDescription, composition.CharactersInfo, composition.AuthorId );
            _compositionRepository.Add( newComposition );
            CreateCompositionRequest compositionDTO = CompositionMapper.ToCompositionDTOMap( newComposition );

            return Ok( compositionDTO );
        }

        [HttpDelete, Route( "{id:int}" )]
        public IActionResult DeleteComposition( [FromRoute] int id )
        {
            Composition composition = _compositionRepository.GetById( id );
            if ( composition == null )
            {
                return NotFound( $"Composition with such id = {id} does not exist" );
            }

            _compositionRepository.Remove( composition );
            return Ok();
        }
    }
}