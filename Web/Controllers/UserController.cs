using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Core.Enities;
using Core.Interfaces;
using infrastrcture.ViewModels;
using System.Net.Mail;
using System.Net;

public class UserController : Controller
{
    private readonly IUnitofwork _unitOfWork;
    private readonly IMapper _mapper;

    public UserController(IUnitofwork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public IActionResult Register()
    {

        var currentUrl = Url.Action("Index", "Home", null, Request.Scheme);

        // إرسال رابط URL الحالي إلى العرض
        ViewData["CurrentUrl"] = currentUrl;

        // إرسال اسم المستخدم إلى العرض
        var viewModel = new UserViewModel
        {
            Username = "YourUsername" // استبدل باسم المستخدم الفعلي
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Register(UserViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = _mapper.Map<User>(model);
            user.Username = GenerateUsername();
            _unitOfWork.Users.Add(user);
             _unitOfWork.Complete(); // استخدم CompleteAsync للتأكد من أنه غير متزامن.

            // أرسل البيانات إلى الـ View لتعرضها
            model.Username = user.Username; // أضف الـ Username الذي تم تعيينه.
            ViewBag.RegistrationSuccess = true; // لإظهار رسالة النجاح.

            return View(model); // أعد المستخدم إلى نفس الصفحة مع البيانات.
        }
        return View(model);
    }




    private string GenerateUsername()
    {
        return $"user{Guid.NewGuid().ToString().Substring(0, 8)}";
    }

    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Login(LoginViewModel user)
    {
        if (ModelState.IsValid)
        {
            var user23 = _unitOfWork.Users.Get(u => u.Username == user.Username).FirstOrDefault();
            if (user23 != null)
            {
                // Redirect to the home page or any other page
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["ErrorMessage"] = "Username does not exist.";
            }
        }
        return View(user);
    }

    [HttpGet]
    public IActionResult ForgotUsername()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ForgotUsername(ForgotUsernameViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = _unitOfWork.Users.GetByEmail(model.Email);

            if (user != null)
            {
                var username = user.Username;
                var resetLink = Url.Action("ResetUsername", "User", new { email = model.Email, username = username }, Request.Scheme);

                // Compose email
                var mailMessage = new MailMessage
                {
                    From = new MailAddress("mostafakhairalla789@gmail.com"),
                    Subject = "Your Username Retrieval",
                    Body = $"<p>Dear User,</p><p>Your username is: <strong>{username}</strong></p><p>Click the link below to set a new username:</p><p><a href=\"{resetLink}\">Set New Username</a></p>",
                    IsBodyHtml = true
                };
                mailMessage.To.Add(model.Email);

                using (var smtpClient = new SmtpClient("smtp.example.com") // Configure your SMTP server settings
                {
                    Port = 587,
                    Credentials = new NetworkCredential("mostafakhairalla789@example.com", "EgyptianMan01021246865*"),
                    EnableSsl = true,
                })
                {
                    await smtpClient.SendMailAsync(mailMessage);
                }

                TempData["SuccessMessage"] = "An email with instructions to reset your username has been sent.";
                return RedirectToAction("ForgotUsername");
            }

            TempData["ErrorMessage"] = "No user found with this email address.";
        }
        return View(model);
    }

    [HttpGet]
    public IActionResult ResetUsername(string email, string username)
    {
        var model = new ResetUsernameViewModel { Email = email, OldUsername = username };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> ResetUsername(ResetUsernameViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = _unitOfWork.Users.GetByEmail(model.Email);
            if (user != null)
            {
                user.Username = model.NewUsername;
                _unitOfWork.Complete();

                TempData["SuccessMessage"] = "Username has been updated successfully.";
                return RedirectToAction("Login", "User");
            }

            TempData["ErrorMessage"] = "Unable to reset the username. Please try again.";
        }
        return View(model);
    }


}
