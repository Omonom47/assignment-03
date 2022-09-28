using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
namespace Assignment.Infrastructure.Tests;

public class WorkItemRepositoryTests : IDisposable
{   
    private readonly KanbanContext _context;
    private readonly WorkItemRepository _repository;
    public WorkItemRepositoryTests()
      {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<KanbanContext>();
        builder.UseSqlite();
        var context = new KanbanContext(builder.Options);
        context.Database.EnsureCreated();
        context.WorkItems.AddRange(new WorkItem(){Id = 0, Title = "pink post it", State = State.Active}, 
                                    new WorkItem(){Id = 1, Title = "blue post it", State = Core.State.New},
                                    new WorkItem(){Id = 2, Title = "green post it", State = Core.State.Resolved},
                                    new WorkItem(){Id = 3, Title = "green post it", State = Core.State.Closed},
                                    new WorkItem(){Id = 4, Title = "green post it", State = Core.State.Removed});
        context.Users.Add(new User(){Id = 5, Name = "Clara"});
        context.SaveChanges();

        _context = context;
        _repository = new WorkItemRepository();

     }
    [Fact]
    public void Test_only_new_can_be_deleted()
    {
        //Arrange 
        var response =  _repository.Delete(1);
        response.Should().Be(Response.Deleted);

        //Act
        var entity = _context.WorkItems.Find(1);

        //Assert
        entity.Should().BeNull();

    }

     [Fact]
    public void Test_delete_active_state_should_be_removed() => _repository.Find(0).Should().Be(State.Removed);
    // {
    //     var response =  _repository.Delete(0);
    // }

    [Fact]
  public void Test_delete_resolved_return_conflict() => _repository.Delete(2).Should().Be(Response.Conflict);
    // {
    //     //Arrange 
    //     var response =  _repository.Delete(1);
    //     response.Should().Be(Response.Deleted);

    //     //Act
    //     var entity = _context.WorkItems.Find(1);

    //     //Assert
    //     entity.Should().BeNull();

    // }

    [Fact]
    public void Test_delete_closed_return_conflict() => _repository.Delete(3).Should().Be(Response.Conflict);

    [Fact]
    public void Test_delete_removed_return_conflict() => _repository.Delete(4).Should().Be(Response.Conflict);

    [Fact]
    public void create_should_return_new_and_created_current_time()
    {
        var (response, workitem) = _repository.Create(new WorkItemCreateDTO("black post it", 5,  "Clara", new List<string>()));
        response.Should().Be(Response.Created);
        _repository.Find(5).Should().Be(State.New);
    }

    [Fact]
    public void assign_not_excisting_user_should_return_badrequest()
    {

    }


    public void Dispose()
   {
    _context.Dispose();
   }
  

}
