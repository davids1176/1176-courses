using System;
using System.Linq;
using Courses;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static async Task Main(string[] args)
    {
        await Menu();
    }

    static async Task Menu()
    {
        while (true)
        {
            Console.WriteLine("What do you want to manage?");
            Console.WriteLine("1 | Students");
            Console.WriteLine("2 | Courses");
            Console.WriteLine("3 | Enrollments");
            Console.WriteLine("0 | Exit");
            string input1 = Console.ReadLine();
            switch (input1)
            {
                case "0":
                    return; // exit the loop
                case "1":
                    Console.WriteLine("1 | List Students:");
                    Console.WriteLine("2 | Add New Student:");
                    Console.WriteLine("3 | Delete Student:");
                    Console.WriteLine("4 | Update Student:");
                    Console.WriteLine("5 | Search Students:");
                    string input2 = Console.ReadLine();
                    switch (input2)
                    {
                        case "1":
                            await ListAllStudents();
                            break;
                        case "2":
                            Console.WriteLine("Enter First Name: ");
                            string firstName = Console.ReadLine();
                            Console.WriteLine("Enter Last Name: ");
                            string lastName = Console.ReadLine();
                            Console.WriteLine("Enter Birth Date (yyyy-mm-dd): ");
                            DateTime birthDate = SafelyParseDate(Console.ReadLine());
                            Console.WriteLine("Enter Email: ");
                            string email = Console.ReadLine();
                            Console.WriteLine("Enter Phone Number: ");
                            string phoneNumber = Console.ReadLine();
                            Console.WriteLine("Enter Enrollment Date: (yyyy-mm-dd): ");
                            DateTime enrollmentDate = SafelyParseDate(Console.ReadLine());
                            await NewStudent(firstName, lastName, birthDate, email, phoneNumber, enrollmentDate);
                            break;
                        case "3":
                            Console.WriteLine("Enter student ID: ");
                            int id = SafelyParseInt(Console.ReadLine());
                            using (AppDbContext context = new AppDbContext())
                            {
                                FindStudentById(id).DeleteAsync(context);
                            }
                            break;
                        case "4":
                            Console.WriteLine("Enter student ID: ");
                            int id1 = SafelyParseInt(Console.ReadLine());
                            Console.WriteLine("Enter First Name: ");
                            string firstNameNew = Console.ReadLine();
                            Console.WriteLine("Enter Last Name: ");
                            string lastNameNew = Console.ReadLine();
                            Console.WriteLine("Enter Birth Date (yyyy-mm-dd): ");
                            DateTime birthDateNew = SafelyParseDate(Console.ReadLine());
                            Console.WriteLine("Enter Email: ");
                            string emailNew = Console.ReadLine();
                            Console.WriteLine("Enter Phone Number: ");
                            string phoneNumberNew = Console.ReadLine();
                            Console.WriteLine("Enter Enrollment Date: (yyyy-mm-dd): ");
                            DateTime enrollmentDateNew = SafelyParseDate(Console.ReadLine());
                            await FindStudentById(id1).UpdateValuesAsync(firstNameNew, lastNameNew, birthDateNew, emailNew, phoneNumberNew, enrollmentDateNew);
                            break;
                        case "5":
                            Console.WriteLine("Enter Student Name: (First [] Last)");
                            Console.WriteLine(SearchStudentByName(Console.ReadLine()).GetListData());
                            break;
                    }
                    break;
                case "2":
                    Console.WriteLine("1 | List Courses:");
                    Console.WriteLine("2 | Add New Course:");
                    Console.WriteLine("3 | Delete Course:");
                    Console.WriteLine("4 | Update Course:");
                    Console.WriteLine("5 | Search Courses:");
                    string input3 = Console.ReadLine();
                    switch (input3)
                    {
                        case "1":
                            await ListAllCourses();
                            break;
                        case "2":
                            Console.WriteLine("Enter Course Name:");
                            string courseName = Console.ReadLine();
                            Console.WriteLine("Enter Course Credits:");
                            int credits = SafelyParseInt(Console.ReadLine());
                            Console.WriteLine("Enter Course Department: ");
                            string department = Console.ReadLine();
                            Console.WriteLine("Enter Course Description:");
                            string description = Console.ReadLine();
                            await NewCourse(courseName, credits, department, description);
                            break;
                        case "3":
                            Console.WriteLine("Enter Course ID:");
                            using (AppDbContext context = new AppDbContext())
                            {
                                await FindCourseById(SafelyParseInt(Console.ReadLine())).DeleteAsync(context);
                            }
                            break;
                        case "4":
                            Console.WriteLine("Enter Course ID:");
                            int id2 = SafelyParseInt(Console.ReadLine());
                            Console.WriteLine("Enter Course Name:");
                            string courseNameNew = Console.ReadLine();
                            Console.WriteLine("Enter Course Credits:");
                            int creditsNew = SafelyParseInt(Console.ReadLine());
                            Console.WriteLine("Enter Course Department:");
                            string departmentNew = Console.ReadLine();
                            Console.WriteLine("Enter Course Description:");
                            string descriptionNew = Console.ReadLine();
                            await FindCourseById(id2).UpdateValuesAsync(courseNameNew, creditsNew, departmentNew, descriptionNew);
                            break;
                        case "5":
                            Console.WriteLine("Enter Course Name:");
                            Console.WriteLine(SearchCourseByName(Console.ReadLine()).GetListData());
                            break;
                    }
                    break;
                case "3":
                    Console.WriteLine("1 | List All Enrollments:");
                    Console.WriteLine("2 | Enroll Student in Course:");
                    Console.WriteLine("3 | Drop Student from a Course:");
                    Console.WriteLine("4 | List courses taken by a Student:");
                    Console.WriteLine("5 | List students enrolled in a Course:");
                    Console.WriteLine("6 | Update student's grade:");
                    string input4 = Console.ReadLine();
                    switch (input4)
                    {
                        case "1":
                            await ListAllEnrollments();
                            break;
                        case "2":
                            Console.WriteLine("Enter Student ID:");
                            int studentId1 = SafelyParseInt(Console.ReadLine());
                            Console.WriteLine("Enter Course ID:");
                            int courseId1 = SafelyParseInt(Console.ReadLine());
                            await EnrollStudent(studentId1, courseId1);
                            break;
                        case "3":
                            Console.WriteLine("Enter Student ID:");
                            int studentId2 = SafelyParseInt(Console.ReadLine());
                            Console.WriteLine("Enter Course ID:");
                            int courseId2 = SafelyParseInt(Console.ReadLine());
                            await DropStudentFromCourse(studentId2, courseId2);
                            break;
                        case "4":
                            Console.WriteLine("Enter Student ID:");
                            int studentId3 = SafelyParseInt(Console.ReadLine());
                            await ListCoursesByStudent(studentId3);
                            break;
                        case "5":
                            Console.WriteLine("Enter Course ID:");
                            int courseId4 = SafelyParseInt(Console.ReadLine());
                            await ListStudentsByCourse(courseId4);
                            break;
                        case "6":
                            Console.WriteLine("Enter Student ID:");
                            int studentId5 = SafelyParseInt(Console.ReadLine());
                            Console.WriteLine("Enter Course ID:");
                            int courseId6 = SafelyParseInt(Console.ReadLine());
                            Console.WriteLine("Enter New Grade:");
                            string grade = Console.ReadLine();
                            await UpdateGrade(studentId5, courseId6, grade);
                            break;
                    }
                    break;
            }
        }
    }

    static int SafelyParseInt(string input)
    {
        if (int.TryParse(input, out int result))
        {
            return result;
        }
        else
        {
            return 0;
        }
    }

    static async Task EnrollStudent(int studentId, int courseId)
    {
        using (var context = new AppDbContext())
        {
            var enrollment = new Enrollment
            {
                StudentId = studentId,
                CourseId = courseId,
                Grade = "1"
            };
            await context.Enrollments.AddAsync(enrollment);
            await context.SaveChangesAsync();
        }
    }

    static async Task DropStudentFromCourse(int studentId, int courseId)
    {
        using (var context = new AppDbContext())
        {
            var enrollment = await context.Enrollments
                .FirstOrDefaultAsync(e => e.StudentId == studentId && e.CourseId == courseId);

            if (enrollment == null)
            {
                Console.WriteLine("Enrollment not found.");
                return;
            }

            context.Enrollments.Remove(enrollment);
            await context.SaveChangesAsync();
        }
    }

    static async Task ListCoursesByStudent(int studentId)
    {
        using (var context = new AppDbContext())
        {
            var courses = await context.Enrollments
                .Where(e => e.StudentId == studentId)
                .Include(e => e.Course)
                .ToListAsync();

            if (!courses.Any())
            {
                Console.WriteLine("No courses found for this student.");
                return;
            }

            Console.WriteLine("Courses taken by student:");
            foreach (var enrollment in courses)
            {
                Console.WriteLine(enrollment.GetListData());
            }
        }
    }

    static async Task ListStudentsByCourse(int courseId)
    {
        using (var context = new AppDbContext())
        {
            var students = await context.Enrollments
                .Where(e => e.CourseId == courseId)
                .Include(e => e.Student)
                .ToListAsync();

            if (!students.Any())
            {
                Console.WriteLine("No students enrolled in this course.");
                return;
            }

            Console.WriteLine("Students enrolled in course:");
            foreach (var enrollment in students)
            {
                Console.WriteLine(enrollment.GetListData());
            }
        }
    }

    static async Task UpdateGrade(int studentId, int courseId, string grade)
    {
        using (var context = new AppDbContext())
        {
            Enrollment? enrollment = await context.Enrollments
                .FirstOrDefaultAsync(e => e.StudentId == studentId && e.CourseId == courseId);

            if (enrollment == null)
            {
                Console.WriteLine("Enrollment not found.");
                return;
            }

            enrollment.Grade = grade;
            await context.SaveChangesAsync();
        }
    }

    static DateTime SafelyParseDate(string input)
    {
        if (DateTime.TryParse(input, out DateTime result))
        {
            return result;
        }
        else
        {
            return DateTime.MinValue;
        }
    }

    static async Task ListAllStudents()
    {
        using (var context = new AppDbContext())
        {
            List<Student?> students = await context.Students.ToListAsync();
            if (!students.Any())
            {
                Console.WriteLine("No students found.");
                return;
            }
            foreach (Student? student in students)
            {
                Console.WriteLine(student.GetListData());
            }
        }
    }

    static async Task ListAllCourses()
    {
        using (var context = new AppDbContext())
        {
            List<Course> courses = await context.Courses.ToListAsync();
            if (!courses.Any())
            {
                Console.WriteLine("No courses found.");
                return;
            }
            foreach (Course course in courses)
            {
                Console.WriteLine(course.GetListData());
            }
        }
    }

    static async Task ListAllEnrollments()
    {
        using (var context = new AppDbContext())
        {
            var enrollments = await context.Enrollments.ToListAsync();
            if (!enrollments.Any())
            {
                Console.WriteLine("No enrollments found.");
                return;
            }
            foreach (var enrollment in enrollments)
            {
                Console.WriteLine(enrollment.GetListData());
            }
        }
    }

    static async Task NewStudent(string firstName, string lastName, DateTime birthDate, string email, string phoneNumber, DateTime enrollmentDate)
    {
        using (var context = new AppDbContext())
        {
            await context.Students.AddAsync(new Student
            {
                FirstName = firstName,
                LastName = lastName,
                BirthDate = birthDate,
                Email = email,
                PhoneNumber = phoneNumber
            });
            await context.SaveChangesAsync();
        }
    }

    static Student SearchStudentByName(string fullName)
    {
        using (var context = new AppDbContext())
        {
            Student? student = context.Students
                .FirstOrDefault(s => (s.FirstName + " " + s.LastName).Equals(fullName, StringComparison.OrdinalIgnoreCase));

            if (student == null)
            {
                Console.WriteLine("Student not found.");
                return null;
            }

            return student;
        }
    }

    static Course SearchCourseByName(string Name)
    {
        using (var context = new AppDbContext())
        {
            Course? course = context.Courses
                .FirstOrDefault(c => (c.CourseName).Equals(Name, StringComparison.OrdinalIgnoreCase));

            if (course == null)
            {
                Console.WriteLine("Course not found.");
                return null;
            }

            return course;
        }
    }

    static Student FindStudentById(int id)
    {
        using (var context = new AppDbContext())
        {
            var student = context.Students.Find(id);
            if (student == null)
            {
                Console.WriteLine("Student not found by ID.");
            }
            return student;
        }
    }

    static async Task NewCourse(string courseName, int credits, string department, string description)
    {
        using (var context = new AppDbContext())
        {
            await context.Courses.AddAsync(new Course()
            {
                CourseName = courseName,
                Credits = credits,
                Department = department,
                Description = description
            });
            await context.SaveChangesAsync();
        }
    }

    static Course FindCourseById(int id)
    {
        using (var context = new AppDbContext())
        {
            var course = context.Courses.Find(id);
            if (course == null)
            {
                Console.WriteLine("Course not found by ID.");
            }

            return course;
        }
    }
}
