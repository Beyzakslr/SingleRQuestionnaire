using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;


// Bu servis, e-posta gönderme işlemini simüle eder ama aslında hiçbir şey yapmaz.
public class DummyEmailSender : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {

        return Task.CompletedTask;
    }
}
