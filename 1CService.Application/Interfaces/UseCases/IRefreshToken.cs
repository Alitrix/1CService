﻿using _1CService.Application.Models.Auth.Request;
using _1CService.Application.Models.Auth.Response;

namespace _1CService.Application.Interfaces.UseCases
{
    public interface IRefreshToken
    {
        Task<JwtAuthToken> Refresh(RefreshTokenQuery refreshToken);
    }
}