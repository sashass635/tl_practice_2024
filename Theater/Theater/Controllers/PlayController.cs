using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Theater.Contracts.Requests;
using Theater.Mappers;

namespace Theater.Controllers
{
    [ApiController]
    [Route( "api/play" )]
    public class PlayController : ControllerBase
    {
        private readonly IPlayRepository _playRepository;

        public PlayController( IPlayRepository playRepository )
        {
            _playRepository = playRepository;
        }

        [HttpGet, Route( "{id:int}" )]
        public IActionResult GetPlay( [FromRoute] int id )
        {
            Play play = _playRepository.GetById( id );
            if ( play == null )
            {
                return NotFound( $"There is no composition with such id = {id}" );
            }

            CreatePlayRequest playDTO = PlayMapper.ToPlayDTOMap( play );
            return Ok( playDTO );
        }

        [HttpGet, Route( "theater/{theaterid:int}" )]
        public IActionResult GetByTheaterId( [FromRoute] int theaterid )
        {
            List<Play> play = _playRepository.GetByTheaterId( theaterid );
            List<CreatePlayRequest> playsDTO = play.Select( PlayMapper.ToPlayDTOMap ).ToList();

            return Ok( playsDTO );
        }

        [HttpGet, Route( "composition/{compositionId:int}" )]
        public IActionResult GetByCompositionId( [FromRoute] int compositionId )
        {
            List<Play> play = _playRepository.GetByCompositionId( compositionId );
            List<CreatePlayRequest> playsDTO = play.Select( PlayMapper.ToPlayDTOMap ).ToList();

            return Ok( playsDTO );
        }

        [HttpPost]
        public IActionResult CreatePlay( [FromBody] CreatePlayRequest play )
        {
            Play newPlay = new Play( play.Name, play.StartDate, play.EndDate, play.TicketPrice, play.Description, play.TheaterId, play.CompositionId );
            _playRepository.Add( newPlay );
            CreatePlayRequest playDTO = PlayMapper.ToPlayDTOMap( newPlay );

            return Ok( playDTO );
        }

        [HttpDelete, Route( "{id:int}" )]
        public IActionResult DeletePlay( [FromRoute] int id )
        {
            Play play = _playRepository.GetById( id );
            if ( play == null )
            {
                return NotFound( $"Play with such id = {id} does not exist" );
            }

            _playRepository.Remove( play );
            return Ok();
        }
    }
}