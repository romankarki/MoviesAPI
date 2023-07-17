using System.ComponentModel.DataAnnotations;

public class Movies
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public double Rating { get; set; }
}