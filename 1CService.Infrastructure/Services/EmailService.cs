using _1CService.Application.Interfaces.Services;
using _1CService.Application.Models;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;

namespace _1CService.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IHttpContextAccessor _ctx;
        private readonly LinkGenerator _linkGenerator;

        public EmailService(IHttpContextAccessor ctx, LinkGenerator linkGenerator) =>
            (_ctx, _linkGenerator ) = (ctx, linkGenerator);

        public async Task SendEmailAsync(AppUser user, string subject, string token)
        {
            if (user == null)
                return;

            if (_ctx.HttpContext == null)
                return;

            var emailMessage = new MimeMessage();
            var callbackUrl = _linkGenerator.GetUriByName(_ctx.HttpContext, "email-confirm", new { userid = user.Id, token });
            var message = "Для подтверждения регистрации перейдите по ссылке <a href=\"" + callbackUrl + "\">here</a>.";

            emailMessage.From.Add(new MailboxAddress("""Администрация ООО "СМИК""", "resentsmyk@mail.ru"));
            emailMessage.To.Add(new MailboxAddress("", user.Email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using var client = new SmtpClient();
            await client.ConnectAsync("smtp.mail.ru", 465, true).ConfigureAwait(false);
            await client.AuthenticateAsync("*********", "*********").ConfigureAwait(false);
            await client.SendAsync(emailMessage).ConfigureAwait(false);

            await client.DisconnectAsync(true).ConfigureAwait(false);
        }
    }
}
