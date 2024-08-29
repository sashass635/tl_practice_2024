using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Foundation.Repositories
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthorRepository( TheaterDbContext context, IUnitOfWork unitOfWork )
            : base( context, unitOfWork )
        {
        }

        public Author GetById( int id )
        {
            return _dbContext.Set<Author>().FirstOrDefault( a => a.Id == id );
        }
    }
}