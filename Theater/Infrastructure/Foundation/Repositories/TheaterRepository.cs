using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Foundation.Repositories
{

    public class TheaterRepository : Repository<Theater>, ITheaterRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        public TheaterRepository( TheaterDbContext context, IUnitOfWork unitOfWork )
            : base( context, unitOfWork )
        {
        }

        public Theater GetById( int id )
        {
            return _dbContext.Set<Theater>().FirstOrDefault( t => t.Id == id );
        }

        public Theater Update( int id, Theater theater )
        {
            Theater oldTheater = _dbContext.Set<Theater>().FirstOrDefault( t => t.Id == id );
            if ( theater == null )
            {
                return null;
            }
            _dbContext.Update( theater );
            _unitOfWork.SaveChanges();

            return theater;
        }
    }
}