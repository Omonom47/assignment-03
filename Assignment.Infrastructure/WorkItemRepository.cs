namespace Assignment.Infrastructure;

public class WorkItemRepository : IWorkItemRepository
{
  private readonly KanbanContext _context;

  public WorkItemRepository(KanbanContext context)
  {
    _context = context;
  }
  public (Response Response, int WorkItemId) Create(WorkItemCreateDTO workItem)
  {
    var entity = new WorkItem
    {
      Title = workItem.Title,
      Description = workItem.Description,
      State = State.New,
      Tags = workItem.Tags
    };

    _context.WorkItems.Add(entity);
    var response = Response.Created;
    return (response, entity.Id);

  }

  public Response Delete(int workItemId)
  {
    var workItem = _context.WorkItems.FirstOrDefault(w => w.Id == workItemId);
    Response response;

    if (workItem is null)
    {
      response = Response.NotFound;
      return response;
    }

    switch (workItem.State)
    {
      case State.New:
        _context.WorkItems.Remove(workItem);
        response = Response.Deleted;
        break;
      case State.Active:
        workItem.State = State.Removed;
        response = Response.Updated;
        break;
      case State.Resolved:
      case State.Closed:
      case State.Removed:
        response = Response.Conflict;
        break;
      default:
        response = Response.BadRequest;
        break;
    }
    _context.SaveChanges();
    return response;
  }

  public WorkItemDetailsDTO Find(int workItemId)
  {
    throw new NotImplementedException();
  }

  public IReadOnlyCollection<WorkItemDTO> Read()
  {
    throw new NotImplementedException();
  }

  public IReadOnlyCollection<WorkItemDTO> ReadByState(State state)
  {
    throw new NotImplementedException();
  }

  public IReadOnlyCollection<WorkItemDTO> ReadByTag(string tag)
  {
    throw new NotImplementedException();
  }

  public IReadOnlyCollection<WorkItemDTO> ReadByUser(int userId)
  {
    throw new NotImplementedException();
  }

  public IReadOnlyCollection<WorkItemDTO> ReadRemoved()
  {
    throw new NotImplementedException();
  }

  public Response Update(WorkItemUpdateDTO workItem)
  {
    Response response;
    var entity = _context.WorkItems.Find(workItem.Id);

    if (entity is null)
    {
      response = Response.NotFound;
      return response;
    }
    else
    {
      entity.Title = workItem.Title;
      entity.Description = workItem.Description ?? entity.Description;
      entity.Tags = workItem.Tags ?? entity.Tags;
      if (workItem is not null)
      {
        entity.State = workItem.State;
      }
      response = Response.Updated;
      _context.SaveChanges();
    }
    return response;
  }
}
