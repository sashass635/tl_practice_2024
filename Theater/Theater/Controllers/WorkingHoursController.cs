using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Theater.Contracts.Requests;
using Theater.Mappers;

namespace Theater.Controllers
{
    [ApiController]
    [Route( "api/working_hours" )]
    public class WorkingHoursController : ControllerBase
    {
        private IWorkingHoursRepository _workingHoursRepository;

        public WorkingHoursController( IWorkingHoursRepository workingHoursRepository )
        {
            _workingHoursRepository = workingHoursRepository;
        }

        [HttpGet, Route( "{id:int}" )]
        public IActionResult GetWorkingHours( [FromRoute] int id )
        {
            WorkingHours workingHours = _workingHoursRepository.GetById( id );

            if ( workingHours == null )
            {
                return NotFound( $"There is no working hours with such id = {id}" );
            }

            CreateWorkingHoursRequest workingHoursDTO = WorkingHoursMapper.ToWorkingHoursDTOMap( workingHours );
            return Ok( workingHoursDTO );
        }

        [HttpGet, Route( "theater/{TheaterId:int}" )]
        public IActionResult GetByTheaterId( [FromRoute] int TheaterId )
        {
            List<WorkingHours> workingHoursList = _workingHoursRepository.GetByTheaterId( TheaterId );
            List<CreateWorkingHoursRequest> workingHoursDTOs = workingHoursList.Select( WorkingHoursMapper.ToWorkingHoursDTOMap ).ToList();

            return Ok( workingHoursDTOs );
        }

        [HttpPost]
        public IActionResult CreateWorkingHours( [FromBody] CreateWorkingHoursRequest workingHours )
        {
            WorkingHours newWorkingHours = new WorkingHours( workingHours.OpeningDate, workingHours.ClosingDate, workingHours.IsWeekend );
            _workingHoursRepository.Add( newWorkingHours );
            WorkingHours workingHoursDTO = WorkingHoursMapper.ToWorkingHoursDTOMap( workingHours );

            return Ok( workingHoursDTO );
        }

        [HttpDelete, Route( "{id:int}" )]
        public IActionResult Delete( [FromRoute] int id )
        {
            WorkingHours workingHours = _workingHoursRepository.GetById( id );

            if ( workingHours == null )
            {
                return NotFound( $"Working hours with such id = {id} does not exist" );
            }

            _workingHoursRepository.Remove( workingHours );
            return Ok();
        }
    }
}