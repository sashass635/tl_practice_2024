using Domain.Entities;
using Theater.Contracts.Requests;

namespace Theater.Mappers
{
    public class WorkingHoursMapper
    {
        public static CreateWorkingHoursRequest ToWorkingHoursDTOMap( WorkingHours workingHours )
        {
            return new CreateWorkingHoursRequest
            {
                OpeningDate = workingHours.OpeningDate,
                ClosingDate = workingHours.ClosingDate,
                IsWeekend = workingHours.IsWeekend
            };
        }

        public static WorkingHours ToWorkingHoursDTOMap( CreateWorkingHoursRequest workingHoursDTO )
        {
            return new WorkingHours(
                workingHoursDTO.OpeningDate,
                workingHoursDTO.ClosingDate,
                workingHoursDTO.IsWeekend
            );
        }
    }
}