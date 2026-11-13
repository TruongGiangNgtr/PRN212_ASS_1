using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentDemo.Models;
using StudentDemo.Services;

namespace StudentDemo.Controllers;

[Authorize]
public class StudentsController : Controller
{
    private readonly IStudentService _studentService;

    public StudentsController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    public IActionResult Index()
    {
        return View(_studentService.GetAll());
    }

    [Route("students/{id:int}")]
    public IActionResult Details(int id)
    {
        var student = _studentService.GetById(id);
        if (student is null)
        {
            return StudentNotFound(id);
        }

        return View(student);
    }

    public IActionResult Create()
    {
        return View(new Student());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Student student)
    {
        FixEmptyAgeMessage();
        if (!ModelState.IsValid)
        {
            return View(student);
        }

        _studentService.Add(student);
        TempData["Success"] = "Thêm thành công.";
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(int id)
    {
        var student = _studentService.GetById(id);
        if (student is null)
        {
            return StudentNotFound(id);
        }

        return View(student);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Student student)
    {
        student.Id = id;
        FixEmptyAgeMessage();
        if (!ModelState.IsValid)
        {
            return View(student);
        }

        if (!_studentService.Update(student))
        {
            return StudentNotFound(id);
        }

        TempData["Success"] = "Cập nhật thành công.";
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Delete(int id)
    {
        var student = _studentService.GetById(id);
        if (student is null)
        {
            return StudentNotFound(id);
        }

        return View(student);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        if (!_studentService.Delete(id))
        {
            return StudentNotFound(id);
        }

        TempData["Success"] = "Xóa thành công.";
        return RedirectToAction(nameof(Index));
    }

    // Age là int nên ô tuổi để trống bị lỗi gán dữ liệu bằng tiếng Anh; đổi sang thông báo tiếng Việt.
    private void FixEmptyAgeMessage()
    {
        if (ModelState.TryGetValue(nameof(Student.Age), out var entry)
            && entry.Errors.Count > 0
            && string.IsNullOrWhiteSpace(entry.AttemptedValue))
        {
            entry.Errors.Clear();
            ModelState.AddModelError(nameof(Student.Age), "Vui lòng nhập tuổi.");
        }
    }

    private ViewResult StudentNotFound(int id)
    {
        Response.StatusCode = StatusCodes.Status404NotFound;
        return View("NotFound", id);
    }
}
