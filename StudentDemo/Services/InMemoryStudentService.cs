using StudentDemo.Models;

namespace StudentDemo.Services;

public class InMemoryStudentService : IStudentService
{
    private readonly List<Student> _students = new()
    {
        new Student { Id = 1, FullName = "Nguyễn Văn An", Email = "an.nguyen@example.com", Age = 20 },
        new Student { Id = 2, FullName = "Trần Thị Bình", Email = "binh.tran@example.com", Age = 21 },
        new Student { Id = 3, FullName = "Lê Minh Cường", Email = "cuong.le@example.com", Age = 19 },
        new Student { Id = 4, FullName = "Phạm Thu Dung", Email = "dung.pham@example.com", Age = 22 },
        new Student { Id = 5, FullName = "Hoàng Quốc Huy", Email = "huy.hoang@example.com", Age = 24 },
    };

    public IEnumerable<Student> GetAll() => _students;

    public Student? GetById(int id) => _students.FirstOrDefault(s => s.Id == id);

    public void Add(Student student)
    {
        student.Id = _students.Count == 0 ? 1 : _students.Max(s => s.Id) + 1;
        _students.Add(student);
    }
}
