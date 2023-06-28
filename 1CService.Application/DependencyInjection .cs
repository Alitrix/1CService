using Microsoft.Extensions.DependencyInjection;
using _1CService.Application.Mapping;
using _1CService.Application.UseCases.Auth;
using _1CService.Application.Interfaces.Services;
using _1CService.Application.UseCases.BlankOrderHandler.Queries;
using _1CService.Application.UseCases.BlankOrderHandler.Commands;

namespace _1CService.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddTransient<IRefreshToken, RefreshToken>();
            services.AddTransient<ISignUpUser, SignUpUser>();
            services.AddTransient<ISignInUser, SignInUser>();
            services.AddTransient<IBlankOrderService, BlankOrderService>();
            services.AddTransient<IExecuteService, ExecuteService>();
            services.AddTransient<ICommentService, CommentService>();

            return services;
        }
    }
}
