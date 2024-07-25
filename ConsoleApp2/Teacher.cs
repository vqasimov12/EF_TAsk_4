namespace ConsoleApp2;
public class Teacher
{
    public int TeacherId { get; set; }
    public DateTime EmploymentDate { get; set; }
    public string FirstName { get; set; }
    public double Premium { get; set; }
    public double Salary { get; set; }
    public string LastName { get; set; }
    public Department Department { get; set; }
    public List<Group> Groups { get; set; } = [];
}
