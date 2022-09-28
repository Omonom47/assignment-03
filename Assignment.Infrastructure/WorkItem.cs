namespace Assignment.Infrastructure;
using Assignment.Core;

public class WorkItem
{


    public int Id { get; set; }
    [StringLength(100)]
    public string Title {get;set;}

    public int AssignedTo{get; set;}
    public string? Description{get; set;}

    public State state{get;set;}

    public IList<int>? Tags{get; set;}

}
