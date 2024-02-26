using EmailSendlerApp.Application.Services;
using EmailSendlerApp.Domain.Enteties;
using Microsoft.AspNetCore.Mvc;

namespace EmailSenderApp;

[ApiController]
[Route("api/[controller]/[action]")]
public class CodeController : Controller
{

    private readonly IEmailService _emailService;

    public CodeController(IEmailService emailService)
    {
        _emailService = emailService;
    }

    [HttpPost]
    public async Task<IActionResult> SendEmail([FromBody] EmailModel model)
    {
        await _emailService.SendMessageAsync(model);



        return Ok("Email Sent Successfully");
    }


    [HttpPost]
    public async Task<IActionResult> CheckCode([FromBody] string code)
    {
        bool isCodeCorrect = await _emailService.CheckEmailAsync(code);

        if (isCodeCorrect)
        {
            return Ok("Code is correct");
        }
        else
        {
            return BadRequest("Incorrect code");
        }
    }


    [HttpPost]
    public async Task<IActionResult> SetPassword([FromBody] UserModel user)
    {
        await _emailService.AddUser(user);

        if (user.Status)
        {
            return Ok("Already Registered");
        }

        return Ok("Password set successfully");
    }

}