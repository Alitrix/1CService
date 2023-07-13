using System.Text;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using _1CService.Application.Interfaces.Services;
using _1CService.Application.Models;
using _1CService.Application.DTO;

namespace _1CService.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IHttpContextAccessor _ctx;
        private readonly LinkGenerator _linkGenerator;
        private readonly EmailConfiguration _emailConfig;

        public EmailService(IHttpContextAccessor ctx, 
                            LinkGenerator linkGenerator, 
                            EmailConfiguration emailConfig) =>
            (_ctx, _linkGenerator, _emailConfig) = (ctx, linkGenerator, emailConfig);

        public async Task<string> SendEmailConfirmTokenAsync(AppUser user, string subject, string token)
        {
            if (user == null) return string.Empty;

            if (string.IsNullOrEmpty(user.Email)) return string.Empty;

            if (_ctx.HttpContext == null) return string.Empty;

            var callbackUrl = _linkGenerator.GetUriByName(_ctx.HttpContext, "email-confirm", new { userid = user.Id, token });
            var message = "Для подтверждения регистрации перейдите по ссылке <a href=\"" + callbackUrl + "\">here</a>.";

            return await SendEmailToAsync(user.Email, subject, message).ConfigureAwait(false);
        }
        public async Task<string> SendEmailRequestUpgradeRights(AppUser from_user, string subject, string token)
        {
            if (from_user == null) return string.Empty;

            if (string.IsNullOrEmpty(from_user.Email)) return string.Empty;

            if (_ctx.HttpContext == null) return string.Empty;

            var acceptRequestUrl = _linkGenerator.GetUriByName(_ctx.HttpContext, "add-role-accept", new { user_id = from_user.Id, token_guid = token });
            var deniedRequestUrl = _linkGenerator.GetUriByName(_ctx.HttpContext, "add-role-denied", new { user_id = from_user.Id, token_guid = token });

            StringBuilder message = new();
            message.Append($"Входящий запрос на добавление права доступа Менеджер, для : {from_user.Email}.      ");
            message.Append($"Разрешить доступ  <a href={acceptRequestUrl}>here</a>.      ");
            message.Append($"Запретить доступ  <a href={deniedRequestUrl}>here</a>.");

            return await SendEmailToAsync(_emailConfig.NotificationMail, subject, message.ToString()).ConfigureAwait(false);
        }
        public async Task<string> SendEmailToAsync(string to_email, string subject, string message_text)
        {
            if (_ctx.HttpContext == null)
                return string.Empty;

            using var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("""Администрация ООО "СМИК""", _emailConfig.From));
            emailMessage.To.Add(new MailboxAddress("", to_email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message_text
            };

            using var client = new SmtpClient();
            await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, true).ConfigureAwait(false);
            await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password).ConfigureAwait(false);
            var retStr = await client.SendAsync(emailMessage).ConfigureAwait(false);

            await client.DisconnectAsync(true).ConfigureAwait(false);

            return retStr;
        }
    }
}
