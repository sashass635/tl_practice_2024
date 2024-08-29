using Domain.Entities;
using Theater.Contracts.Requests;

namespace Theater.Mappers
{
    public class CompositionMapper
    {
        public static CreateCompositionRequest ToCompositionDTOMap( Composition composition )
        {
            return new CreateCompositionRequest
            {
                Name = composition.Name,
                ShortDescription = composition.ShortDescription,
                CharactersInfo = composition.CharactersInfo,
                AuthorId = composition.AuthorId
            };
        }

        public static Composition ToCompositionDTOMap( CreateCompositionRequest compositionDTO )
        {
            return new Composition(
                compositionDTO.Name,
                compositionDTO.ShortDescription,
                compositionDTO.CharactersInfo,
                compositionDTO.AuthorId
            );
        }
    }
}