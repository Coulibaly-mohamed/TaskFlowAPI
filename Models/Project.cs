public class Project
{
    public int ProjectId { get; set; }
    public required string Name { get; set; }

    public string? Description { get; set; }

    public DateTime CreationDate { get; private set; }

    public int UserId { get; set; }


}

public class ProjectClassDTO()
{
    public required string Name { get; set; }

    public string? Description { get; set; }

    public DateTime CreationDate { get; private set; }

}

