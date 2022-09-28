using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
namespace Assignment.Infrastructure.Tests;

public class UserRepositoryTests : IDisposable
{
  private readonly KanbanContext _context;
  private readonly UserRepository _repository;

  public UserRepositoryTests()
  {
    var connection = new SqliteConnection("Filename=:memory:");
    connection.Open();
    var build = new DbContextOptionsBuilder<KanbanContext>();
    build.UseSqlite(connection);
    var context = new KanbanContext(build.Options);
    context.Database.EnsureCreated();

    context.Users.AddRange(
      new User() {Id = 0, Name = "Ron Weasly", Email = "ronweasly@gmail.com"},
      new User() {Id = 1, Name = "Harry Potter", Email = "harryp@hotmail.dk"});

    context.Tags.AddRange(new Tag()
      { Id = 0, Name = "Frontend"});

      _context = context;
      _repository = new UserRepository();

  }
   [Fact]
  public void Test_user_can_delete_force() => _repository.Delete(0, true).Should().Be(Response.Deleted);


    [Fact]
  public void Test_user_cannot_delete_return_Conflict() => _repository.Delete(0, true).Should().Be(Response.Conflict);


  [Fact]
  public void Test_try_create_user_already_exsist_return_Conflict()
  {
    //Arrange
    var (response, id) = _repository.Create(new UserCreateDTO("Harry Potter", "harryp@hotmail.dk"));
    response.Should().Be(Response.Conflict);

  }


  public void Dispose()
  {
     _context.Dispose();
  }
}
