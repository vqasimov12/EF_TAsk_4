using Microsoft.EntityFrameworkCore;

namespace ConsoleApp2;
public class UnivercityContext : DbContext
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Faculty> Faculties { get; set; }
    public DbSet<Department> Departments { get; set; }
    public UnivercityContext()
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Univercity;Integrated Security=True;");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Group

        modelBuilder.Entity<Group>()
            .HasKey(x => x.GroupId);
        modelBuilder.Entity<Group>().Property(x => x.GroupId)
         .HasColumnName("Id")
         .ValueGeneratedOnAdd()
         .HasColumnType("int");

        modelBuilder.Entity<Group>()
            .Property(x => x.Name)
            .HasMaxLength(10)
            .IsRequired().HasColumnType("nvarchar");
        modelBuilder.Entity<Group>().HasIndex(x => x.Name);
        modelBuilder.Entity<Group>()
             .ToTable(x => x.HasCheckConstraint("CK_Group_Name", "Name!= ''"));


        modelBuilder.Entity<Group>()
            .Property(x => x.Rating)
            .IsRequired();
        modelBuilder.Entity<Group>()
             .ToTable(x => x.HasCheckConstraint("CK_Group_GroupRating", "Rating >= 0 AND Rating<= 5"));

        modelBuilder.Entity<Group>()
             .Property(x => x.Year)
             .IsRequired();
        modelBuilder.Entity<Group>()
             .ToTable(x => x.HasCheckConstraint("CK_Group_GroupYear", "Year >= 1 AND Year<= 5"));
        #endregion

        #region Department

        modelBuilder.Entity<Department>()
            .Property(x => x.DepartmentId)
            .HasColumnName("Id")
            .HasColumnType("int");
        modelBuilder.Entity<Department>()
            .HasKey(x => x.DepartmentId);

        modelBuilder.Entity<Department>()
            .Property(x => x.Financing)
            .HasColumnType("money")
            .IsRequired()
            .HasDefaultValue(0);
        modelBuilder.Entity<Department>()
             .ToTable(x => x.HasCheckConstraint("CK_Department_Financinig", "Financing >= 0"));

        modelBuilder.Entity<Department>()
             .Property(x => x.Name)
             .HasColumnType("nvarchar")
             .IsRequired()
             .HasMaxLength(100);
        modelBuilder.Entity<Department>().HasIndex(x => x.Name);
        modelBuilder.Entity<Department>()
             .ToTable(x => x.HasCheckConstraint("CK_Department_Name", "Name !=''"));


        #endregion

        #region Faculty

        modelBuilder.Entity<Faculty>()
         .Property(x => x.FacultyId)
         .HasColumnName("Id")
         .HasColumnType("int");
        modelBuilder.Entity<Faculty>()
            .HasKey(x => x.FacultyId);

        modelBuilder.Entity<Faculty>()
           .Property(x => x.Name)
           .HasColumnType("nvarchar")
           .IsRequired()
           .HasMaxLength(100);
        modelBuilder.Entity<Faculty>().HasIndex(x => x.Name);
        modelBuilder.Entity<Faculty>()
             .ToTable(x => x.HasCheckConstraint("CK_Faculty_Name", "Name !=''"));

        #endregion

        #region Teacher

        modelBuilder.Entity<Teacher>()
            .Property(x => x.TeacherId)
            .HasColumnName("Id")
            .HasColumnType("int");
        modelBuilder.Entity<Teacher>()
            .HasKey(x => x.TeacherId);

        modelBuilder.Entity<Teacher>()
            .HasOne(s => s.Department)
            .WithMany(g => g.Teachers)
            .HasForeignKey(s => s.TeacherId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Department");


        modelBuilder.Entity<Teacher>()
             .Property(x => x.FirstName)
             .HasColumnType("nvarchar")
             .IsRequired()
             .HasColumnName("Name");
        modelBuilder.Entity<Teacher>()
             .ToTable(x => x.HasCheckConstraint("CK_Teacher_Name", "Name !=''"));


        modelBuilder.Entity<Teacher>()
             .Property(x => x.LastName)
             .HasColumnType("nvarchar")
             .IsRequired()
             .HasColumnName("Surname");
        modelBuilder.Entity<Teacher>()
             .ToTable(x => x.HasCheckConstraint("CK_Teacher_Surname", "Surname !=''"));


        modelBuilder.Entity<Teacher>()
            .Property(x => x.EmploymentDate)
            .HasColumnType("date")
            .IsRequired();
        modelBuilder.Entity<Teacher>().ToTable(x => x.HasCheckConstraint("CK_Teacher_EmploymentDate", "EmploymentDate >= '1990-01-01'"));

        modelBuilder.Entity<Teacher>()
            .Property(x => x.Premium)
            .IsRequired()
            .HasColumnType("money")
            .HasDefaultValue(0);
        modelBuilder.Entity<Teacher>().ToTable(x => x.HasCheckConstraint("CK_Teacher_Premium", "Premium >= 0"));

        modelBuilder.Entity<Teacher>()
            .Property(x => x.Salary)
            .IsRequired()
            .HasColumnType("money")
            .HasDefaultValue(0);
        modelBuilder.Entity<Teacher>().ToTable(x => x.HasCheckConstraint("CK_Teacher_Salary", "Salary >= 0"));



        #endregion

        #region Student

        modelBuilder.Entity<Student>()
            .Property(x => x.StudentId)
            .HasColumnName("Id")
            .HasColumnType("int");
        modelBuilder.Entity<Student>()
            .HasKey(x => x.StudentId);

        modelBuilder.Entity<Student>()
             .Property(x => x.FirstName)
             .HasColumnType("nvarchar")
             .IsRequired()
             .HasColumnName("Name");
        modelBuilder.Entity<Student>()
     .ToTable(x => x.HasCheckConstraint("CK_Student_Name", "Name !=''"));


        modelBuilder.Entity<Student>()
             .Property(x => x.LastName)
             .HasColumnType("nvarchar")
             .IsRequired()
             .HasColumnName("Surname");
        modelBuilder.Entity<Student>()
            .ToTable(x => x.HasCheckConstraint("CK_Student_Surname", "Surname !=''"));



        modelBuilder.Entity<Student>()
           .HasOne(s => s.Faculty)
           .WithMany(f => f.Students)
           .HasForeignKey(x => x.StudentId)
           .OnDelete(DeleteBehavior.Cascade)
           .HasConstraintName("FK_Faculty");

        modelBuilder.Entity<Student>()
           .HasOne(s => s.Group)
           .WithMany(f => f.Students)
           .HasForeignKey(x => x.StudentId)
           .OnDelete(DeleteBehavior.Cascade)
           .HasConstraintName("FK_Group");
        #endregion

    }
}
