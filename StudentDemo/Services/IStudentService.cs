using StudentDemo.Models;

namespace StudentDemo.Services;

public interface IStudentService
{
    IEnumerable<Student> GetAll();
    Student? GetById(int id);
    void Add(Student student);
}
