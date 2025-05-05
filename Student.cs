namespace Courses;

public class Student
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime EnrollmentDate { get; set; }
    public List<Enrollment> Enrollments { get; set; } = new List<Enrollment>();


    public async Task DeleteAsync(AppDbContext context)
    {
        var student = context.Students.Find(Id);
        if (student != null)
        {
            context.Students.Remove(student);
            await context.SaveChangesAsync();
        }
    }
    
    public string GetListData()
    {
        return Id + " | " + FirstName + " " + LastName + " | " + Email;
    }

    public async Task UpdateValuesAsync(string firstNameNew, string lastNameNew, DateTime birthDateNew, string emailNew, string phoneNumberNew, DateTime enrollmentDateNew)
    {
        FirstName = firstNameNew;
        LastName = lastNameNew;
        BirthDate = birthDateNew;
        Email = emailNew;
        PhoneNumber = phoneNumberNew;
        EnrollmentDate = enrollmentDateNew;
    }
}