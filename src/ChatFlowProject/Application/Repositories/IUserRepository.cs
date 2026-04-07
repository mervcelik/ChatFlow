using Domain.Entities;
using Core.Persistence.Repositories;

namespace Application.Repositories;

public interface IUserRepository : IRepository<User>
{
}
