using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using System.Reflection;
using _1CService.Application.Mapping;
using _1CService.Application.UseCases.Auth;
using _1CService.Application.Interfaces.Services;

namespace _1CService.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddTransient<ISignUpUser, SignUpUser>();
            services.AddTransient<ISignInUser, SignInUser>();
            return services;
        }
    }
}
