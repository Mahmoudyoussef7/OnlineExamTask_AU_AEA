using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineExam.Application.Interfaces;
using OnlineExam.Core.Entities;
using OnlineExam.UI.Models;

namespace OnlineExam.UI.Controllers;

public class AccountController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AccountController> _logger;

    public AccountController(IUnitOfWork unitOfWork, ILogger<AccountController> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Login()
    {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            User user = await _unitOfWork.Users.GetUserByEmail(model.Email);

            if (user != null && user.Password == model.Password)
            {
                _logger.LogInformation("User logged in.");
                SetUserCookies(user);
                return RedirectToAction("Index", "Home");
            }
        }
        ModelState.AddModelError(string.Empty, "Invalid email or password");
        return View();
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Register()
    {
        ViewBag.Roles = new SelectList(await _unitOfWork.UserRoles.GetAllAsync());
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var existingUser = await _unitOfWork.Users.GetUserByEmail(model.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("Email", "Email is already taken");
                ViewBag.Roles = new SelectList(await _unitOfWork.UserRoles.GetAllAsync());
                return View(model);
            }

            var user = new User { UserName = model.Email, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName, RoleId = model.UserRole, IsActive=true, Password=model.Password };

            await _unitOfWork.Users.AddAsync(user);
            _logger.LogInformation("User created a new account with password.");
            SetUserCookies(user); 
            return RedirectToAction("Index", "Home");
        }
        ViewBag.Roles = new SelectList(await _unitOfWork.UserRoles.GetAllAsync());
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        DeleteUserCookies();
        _logger.LogInformation("User logged out.");
        return RedirectToAction("Login", "Account");
    }

    private void SetUserCookies(User user)
    {
        // Create the cookie options
        var cookieOptions = new CookieOptions
        {
            // Set the cookie expiration time
            Expires = DateTime.UtcNow.AddDays(7),
            // Set the cookie to be accessible only via HTTP
            HttpOnly = true,
            // Set the cookie to be secure (HTTPS) only
            Secure = true,
            // Set the same-site policy to strict
            SameSite = SameSiteMode.Strict
        };

        // Set the user ID and role ID in separate cookies
        Response.Cookies.Append("UserId", user.Id.ToString(), cookieOptions);
        Response.Cookies.Append("RoleId", user.RoleId.ToString(), cookieOptions);
    }

    private void DeleteUserCookies()
    {
        // Remove the user cookies
        Response.Cookies.Delete("UserId");
        Response.Cookies.Delete("RoleId");
    }
}
