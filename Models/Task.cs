using System.Collections.ObjectModel;

enum Status
{
    Afaire,
    EnCours,
    Terminé
}
public class Task
{
    public int TaskId { get; set; }
    public required string Title { get; set; }

    public int ProjectId { get; set; } //Foreign key

    public DateTime DueDate { get; set; }

    public List<string> Commentaire = [];


}

public class TaskDTO()

{
    public required string Title { get; set; }
    public DateTime DueDate { get; set; }

    public List<string> Commentaire = [];

}