using Domain.Entities;
using Theater.Contracts.Requests;

namespace Theater.Mappers
{
    public class AuthorMapper
    {
        public static CreateAuthorRequest ToAuthorDTOMap( Author author )
        {
            return new CreateAuthorRequest
            {
                Name = author.Name,
                DateBirth = author.DateBirth,

            };
        }

        public static Author ToAuthorDTOMap( CreateAuthorRequest authorDTO )
        {
            return new Author(
                authorDTO.Name,
                authorDTO.DateBirth
            );
        }
    }
}