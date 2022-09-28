namespace Assignment.Infrastructure;

public class UserRepository : IUserRepository
{
    private readonly KanbanContext _kanban;
    public (Response Response, int UserId) Create(UserCreateDTO user)
    {
        var entity = _kanban.Users.FirstOrDefault(k => k.Email == user.Email);

        Response resp;

        if (entity is null)
        {
            entity = new User { Name = user.Name, Email = user.Email };

            _kanban.Add(entity);
            _kanban.SaveChanges();
            resp = Response.Created;

        }
        else
        {
            resp = Response.Conflict;
        }

        return (resp, entity.Id);

    }

    public Response Delete(int userId, bool force = false)
    {
        var user = _kanban.Users.FirstOrDefault(u => u.Id == userId);

        Response response;

        if (user.WorkItems.Any() && !force)
        {
            response = Response.Conflict;
        }
        else
        {
            response = Response.Deleted;
        }

        return response;
    }

    public UserDTO Find(int userId)
    {
        var quer = from u in _kanban.Users
                   where u.Id == userId
                   select new { Id = u.Id, Name = u.Name, Email = u.Email };

        var user = quer.ElementAt(0);
        return new UserDTO(user.Id, user.Name, user.Email);
    }

    public IReadOnlyCollection<UserDTO> Read()
    {
        throw new NotImplementedException();
    }

    public Response Update(UserUpdateDTO user)
    {
        throw new NotImplementedException();
    }
}
