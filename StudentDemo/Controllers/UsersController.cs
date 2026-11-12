using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentDemo.Models;
using StudentDemo.Services;

namespace StudentDemo.Controllers;

[Authorize]
public class UsersController : Controller
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    public IActionResult Index()
    {
        return View(_userService.GetAll());
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new UserCreateForm());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(UserCreateForm form)
    {
        if (!ModelState.IsValid) return View(form);

        var user = new User { Username = form.Username.Trim(), FullName = form.FullName.Trim() };
        if (!_userService.Add(user, form.Password))
        {
            ModelState.AddModelError(nameof(form.Username), "Tên đăng nhập đã tồn tại");
            return View(form);
        }

        TempData["Success"] = "Thêm người dùng thành công";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var user = _userService.GetById(id);
        if (user is null) return NotFound();

        return View(new UserEditForm { Id = user.Id, Username = user.Username, FullName = user.FullName });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(UserEditForm form)
    {
        if (_userService.GetById(form.Id) is null) return NotFound();
        if (!ModelState.IsValid) return View(form);

        var user = new User { Id = form.Id, Username = form.Username.Trim(), FullName = form.FullName.Trim() };
        if (!_userService.Update(user, form.NewPassword))
        {
            ModelState.AddModelError(nameof(form.Username), "Tên đăng nhập đã tồn tại");
            return View(form);
        }

        TempData["Success"] = "Cập nhật người dùng thành công";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        var user = _userService.GetById(id);
        if (user is null) return NotFound();

        return View(user);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        if (User.FindFirstValue(ClaimTypes.NameIdentifier) == id.ToString())
        {
            TempData["Error"] = "Không thể xóa tài khoản đang đăng nhập";
            return RedirectToAction(nameof(Index));
        }

        if (!_userService.Delete(id)) return NotFound();

        TempData["Success"] = "Xóa người dùng thành công";
        return RedirectToAction(nameof(Index));
    }
}
