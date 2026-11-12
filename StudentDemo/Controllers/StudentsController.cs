using Microsoft.AspNetCore.Mvc;
using StudentDemo.Services;

namespace StudentDemo.Controllers;

public class StudentsController : Controller
{
    private readonly IStudentService _studentService;

    public StudentsController(IStudentService studentService)
    {
        _studentService = studentService;
    }
}
