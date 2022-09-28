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
        throw new NotImplementedException();
    }

    public UserDTO Find(int userId)
    {
        throw new NotImplementedException();
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
