using Microsoft.Extensions.DependencyInjection;
using _1CService.Application.Mapping;
using _1CService.Application.UseCases.AuthHandler;
using _1CService.Application.UseCases.BlankOrderHandler.Queries;
using _1CService.Application.UseCases.BlankOrderHandler.Commands;
using _1CService.Application.UseCases.ProfileHandler.Command;
using _1CService.Application.UseCases.ProfileHandler.Queries;
using _1CService.Application.Interfaces.UseCases;

namespace _1CService.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAuthApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddTransient<IRefreshToken, RefreshToken>();
            services.AddTransient<ISignUpUser, SignUpUser>();
            services.AddTransient<ISignInUser, SignInUser>();
            services.AddTransient<ISignOutUser, SignOutUser>();
            services.AddTransient<IRequestAddRights, RequestAddRights>();
            services.AddTransient<IRoleAddToUser, RoleAddToUser>();
            services.AddTransient<IGetProfileAppUser, GetProfileAppUser>();
            services.AddTransient<ISetProfileAppUser, SetProfileAppUser>();
            services.AddTransient<IEmailTokenValidation, EmailTokenValidation>();

            return services;
        }
        public static IServiceCollection Add1CApplication(this IServiceCollection services)
        {
            services.AddTransient<IGetBlankOrder, GetBlankOrder>();
            services.AddTransient<IGetBlankOrderDetail, GetBlankOrderDetail>();
            services.AddTransient<IAcceptToWorkBlankOrder, AcceptToWorkBlankOrder>();
            services.AddTransient<IAddCommentToBlankOrder, AddCommentToBlankOrder>();

            return services;
        }
    }
}
