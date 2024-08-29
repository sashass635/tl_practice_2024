using Domain.Entities;

namespace Domain.Repositories;

public interface IAuthorRepository : IRepository<Author>
{
    public Author GetById( int id );
}