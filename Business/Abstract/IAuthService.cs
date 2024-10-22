﻿using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Entities.DTOs.AuthDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAuthService
    {
        Task<IResult> RegisterAsync(RegisterDTO registerDTO);
        Task<IDataResult<Token>> LoginAsync(LoginDTO loginDTO);
        Task<IDataResult<string>> UpdateRefreshToken(string refreshToken, AppUser appUser);
        Task<IDataResult<Token>> RefreshTokenLoginAsync(string refreshToken);
        Task<IResult> AssignRoleToUserAsync(string userId, string[] role);
        Task<IResult> LogoutAsync(string userId);
    }
}
