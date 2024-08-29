using Domain.Entities;

namespace Domain.Repositories;

public interface ICompositionRepository : IRepository<Composition>
{
    public Composition GetById( int id );
    public List<Composition> GetByAuthorId( int authorId );
}