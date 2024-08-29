using Domain.Entities;

namespace Domain.Repositories;

public interface IWorkingHoursRepository : IRepository<WorkingHours>
{
    public WorkingHours GetById( int id );
    public List<WorkingHours> GetByTheaterId( int id );
}