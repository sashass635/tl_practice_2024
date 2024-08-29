using Domain.Entities;
using Theater.Contracts.Requests;

namespace Theater.Mappers
{
    public class PlayMapper
    {
        public static CreatePlayRequest ToPlayDTOMap( Play play )
        {
            return new CreatePlayRequest
            {
                Name = play.Name,
                StartDate = play.StartDate,
                EndDate = play.EndDate,
                TicketPrice = play.TicketPrice,
                Description = play.Description,
                TheaterId = play.TheaterId,
                CompositionId = play.CompositionId
            };
        }

        public static Play ToPlayDTOMap( CreatePlayRequest playDTO )
        {
            return new Play(
                playDTO.Name,
                playDTO.StartDate,
                playDTO.EndDate,
                playDTO.TicketPrice,
                playDTO.Description,
                playDTO.TheaterId,
                playDTO.CompositionId
            );
        }
    }
}