namespace ConsoleApp2;
public class Group
{
    public int GroupId { get; set; }
    public string Name { get; set; }
    public int Rating { get; set; }
    public int Year { get; set; }
    public List<Student> Students { get; set; } = [];
    public List<Teacher> Teachers { get; set; } = [];

}
