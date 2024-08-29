using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Theater.Contracts.Requests;
using Theater.Mappers;

namespace Theater.Controllers
{
    [ApiController]
    [Route( "api/theater" )]
    public class TheaterController : ControllerBase
    {
        private readonly ITheaterRepository _theaterRepository;

        public TheaterController( ITheaterRepository theaterRepository )
        {
            _theaterRepository = theaterRepository;
        }

        [HttpGet, Route( "{id:int}" )]
        public IActionResult GetTheater( [FromRoute] int id )
        {
            Domain.Entities.Theater theater = _theaterRepository.GetById( id );
            if ( theater == null )
            {
                return NotFound( $"There is no theater with such id = {id}" );
            }

            CreateTheaterRequest theaterDTO = TheaterMapper.ToTheaterDTOMap( theater );

            return Ok( theaterDTO );
        }

        [HttpPost]
        public IActionResult CreateTheater( [FromBody] CreateTheaterRequest theater )
        {
            Domain.Entities.Theater newTheater = new Domain.Entities.Theater( theater.Name, theater.Address, theater.OpeningDate, theater.Description, theater.PhoneNumber );

            if ( theater.WorkingHours != null )
            {
                foreach ( CreateWorkingHoursRequest wh in theater.WorkingHours )
                {
                    WorkingHours workingHours = new WorkingHours( wh.OpeningDate, wh.ClosingDate, wh.IsWeekend );
                    newTheater.WorkingHours.Add( workingHours );
                }
            }

            _theaterRepository.Add( newTheater );
            CreateTheaterRequest theaterDTO = TheaterMapper.ToTheaterDTOMap( newTheater );

            return Ok( theaterDTO );
        }

        [HttpPut, Route( "{id:int}" )]
        public IActionResult UpdateTheater( [FromRoute] int id, [FromBody] UpdateTheaterRequest theater )
        {
            Domain.Entities.Theater newTheater = _theaterRepository.GetById( id );

            if ( newTheater == null )
            {
                return NotFound( $"There is no theater with such id = {id}" );
            }

            newTheater.Update( theater.Name, theater.Description, theater.PhoneNumber );
            Domain.Entities.Theater updatedTheatre = _theaterRepository.Update( id, newTheater );
            CreateTheaterRequest theaterDTO = TheaterMapper.ToTheaterDTOMap( updatedTheatre );

            return Ok( theaterDTO );
        }

        [HttpDelete, Route( "{id:int}" )]
        public IActionResult DeletePlay( [FromRoute] int id )
        {
            Domain.Entities.Theater theater = _theaterRepository.GetById( id );
            if ( theater == null )
            {
                return NotFound( $"Theater with such id = {id} does not exist" );
            }

            _theaterRepository.Remove( theater );
            return Ok();
        }
    }
}