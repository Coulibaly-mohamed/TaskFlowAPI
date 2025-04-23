
public class User
{
    public int UserId { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }

    public required string PasswordHash { get; set; }
    //Collection des projets appartenant a l'user
    public ICollection<Project> Projets { get; set; } = [];

}


public class UserClassDTO()
{
    public required string Name { get; set; }
    public required string Email { get; set; }

}