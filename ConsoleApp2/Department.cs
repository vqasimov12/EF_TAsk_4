namespace ConsoleApp2;
public class Department
{
    public int DepartmentId { get; set; }
    public double Financing { get; set; } = 0;
    public string Name { get; set; }
    public List<Teacher> Teachers { get; set; } = [];
}
