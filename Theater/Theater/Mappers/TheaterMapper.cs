using Domain.Entities;
using Theater.Contracts.Requests;

namespace Theater.Mappers
{
    public class TheaterMapper
    {
        public static CreateTheaterRequest ToTheaterDTOMap( Domain.Entities.Theater theater )
        {
            return new CreateTheaterRequest
            {
                Name = theater.Name,
                Address = theater.Address,
                OpeningDate = theater.OpeningDate,
                Description = theater.Description,
                PhoneNumber = theater.PhoneNumber,
                WorkingHours = theater.WorkingHours.Select( wh => new CreateWorkingHoursRequest
                {
                    OpeningDate = wh.OpeningDate,
                    ClosingDate = wh.ClosingDate,
                    IsWeekend = wh.IsWeekend
                } ).ToList()
            };
        }

        public static Domain.Entities.Theater ToTheaterDTOMap( CreateTheaterRequest theaterDTO )
        {
            Domain.Entities.Theater theater = new Domain.Entities.Theater( theaterDTO.Name, theaterDTO.Address, theaterDTO.OpeningDate, theaterDTO.Description, theaterDTO.PhoneNumber );
            if ( theaterDTO.WorkingHours != null )
            {
                foreach ( CreateWorkingHoursRequest wh in theaterDTO.WorkingHours )
                {
                    theater.WorkingHours.Add( new WorkingHours( wh.OpeningDate, wh.ClosingDate, wh.IsWeekend ) );
                }
            }
            return theater;
        }
    }
}