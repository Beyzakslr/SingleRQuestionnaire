using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;


//  e-posta gönderme işlemini simüle eder 
public class DummyEmailSender : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {

        return Task.CompletedTask;
    }
}
