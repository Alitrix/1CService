using _1CService.Application.Interfaces.Services;
using _1CService.Application.Interfaces.Services.Auth;
using _1CService.Application.Models;
using Microsoft.AspNetCore.Identity;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;

namespace _1CService.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IHttpContextAccessor _ctx;
        private readonly IAppUserService _appUserService;
        private readonly UserManager<AppUser> _userManager;
        private readonly LinkGenerator _linkGenerator;

        public EmailService(IHttpContextAccessor ctx, 
                            IAppUserService appUserService, 
                            UserManager<AppUser> userManager, 
                            LinkGenerator linkGenerator)
        {
            _ctx = ctx;
            _appUserService = appUserService;
            _userManager = userManager;
            _linkGenerator = linkGenerator;
        }

        public async Task<string> GenerateEmailConfirmationToken(AppUser? user = null)
        {
            var currentUser = user?? await _appUserService.GetCurrentUser();
            if (currentUser == null)
                return string.Empty;

            var originalCode = await _userManager.GenerateEmailConfirmationTokenAsync(currentUser);
            return originalCode;
        }
        public async Task SendEmailAsync(AppUser user, string subject, string token)
        {
            if (user == null)
                return;

            if (_ctx.HttpContext == null)
                return;

            var emailMessage = new MimeMessage();
            var callbackUrl = _linkGenerator.GetUriByName(_ctx.HttpContext, "email-confirm", new { userid = user.Id, token = token });
            var message = "Для подтверждения регистрации перейдите по ссылке <a href=\"" + callbackUrl + "\">here</a>.";

            emailMessage.From.Add(new MailboxAddress("""Администрация ООО "СМИК""", "resentsmyk@mail.ru"));
            emailMessage.To.Add(new MailboxAddress("", user.Email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using var client = new SmtpClient();
            await client.ConnectAsync("smtp.mail.ru", 465, true);
            await client.AuthenticateAsync("resentsmyk@mail.ru", "v1LzDd48vkxVS0fEs80v");
            await client.SendAsync(emailMessage);

            await client.DisconnectAsync(true);
        }
    }
}
