using Microsoft.AspNetCore.Mvc;
using StudentDemo.Services;

namespace StudentDemo.Controllers;

public class AccountController : Controller
{
    private readonly IUserService _userService;

    public AccountController(IUserService userService)
    {
        _userService = userService;
    }
}
