namespace Courses;

public class Enrollment
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public Student Student { get; set; }
    public int CourseId { get; set; }
    public Course Course { get; set; }
    public DateTime EnrollmentDate { get; set; }
    public string Grade { get; set; }
    
    public string GetListData()
    {
        return Id + " | Student Id: " + StudentId + ",Course:" + Course.Id;
    }

}