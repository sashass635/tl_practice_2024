using Domain.Repositories;

namespace Infrastructure.Foundation.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly TheaterDbContext _dbContext;
        protected readonly IUnitOfWork _unitOfWork;

        public Repository( TheaterDbContext dbContext, IUnitOfWork unitOfWork )
        {
            _dbContext = dbContext;
            _unitOfWork = unitOfWork;
        }

        public void Add( T item )
        {
            _dbContext.Set<T>().Add( item );
            _unitOfWork.SaveChanges();
        }

        public List<T> GetAll()
        {
            return _dbContext.Set<T>().ToList();
        }

        public void Remove( T item )
        {
            _dbContext.Set<T>().Remove( item );
            _unitOfWork.SaveChanges();
        }
    }
}