namespace Assignment.Infrastructure;

public class User{
    public int Id{get;}

    [StringLength(100)]
    public string? Name{get;set;}
    [StringLength(100)]
    public string? Email{get;set;}

    public IList<WorkItem>? WorkItems{get; set;}
}
