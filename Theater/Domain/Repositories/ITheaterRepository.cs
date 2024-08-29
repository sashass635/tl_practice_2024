using Domain.Entities;

namespace Domain.Repositories;

public interface ITheaterRepository : IRepository<Theater>
{
    public Theater GetById( int id );
    public Theater Update( int id, Theater theater );
}