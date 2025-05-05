namespace Courses;

public class Course
{
    public int Id { get; set; }
    public string CourseName { get; set; }
    public int Credits { get; set; }
    public string Department { get; set; }
    public string Description { get; set; }
    public List<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    
    public async Task DeleteAsync(AppDbContext context)
    {
        var course = context.Courses.Find(Id);
        if (course != null)
        {
            context.Courses.Remove(course);
            await context.SaveChangesAsync();
        }
    }
    
    public string GetListData()
    {
        return Id + " | " + CourseName;
    }
    
    public async Task UpdateValuesAsync(string courseNameNew, int creditsNew, string departmemntNew, string descriptionNew)
    {
        CourseName = courseNameNew;
        Credits = creditsNew;
        Department = departmemntNew;
        Description = descriptionNew;
    }
}