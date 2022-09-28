using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
namespace Assignment.Infrastructure.Tests;

public class TagRepositoryTests : IDisposable
{

  private readonly KanbanContext _context;
  private readonly TagRepository _repository;

  public TagRepositoryTests()
  {
     var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            var build = new DbContextOptionsBuilder<KanbanContext>();
            build.UseSqlite(connection);
            var context = new KanbanContext(build.Options);
            context.Database.EnsureCreated();

      _context = context;
      _repository = new TagRepository();
  }
  [Fact]
  public void Test_tags_can_delete_force()
  {
    //Arrange

    //Act
    //Assert
  }

  [Fact]
  public void Test_tags_cannot_delete_return_Conflict()
  {
    //Arrange

    //Act
    //Assert
  }

  [Fact]
  public void Test_creat_tag_already_exist_return_Conflict()
  {
    //Arrange

    //Act
    //Assert
  }

  public void Dispose()
  {
    _context.Dispose();
  }
}


