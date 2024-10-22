using Business.Abstract;
using Business.FluentValidation.AuthValidations;
using Business.StatusMessages;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.ErrorResults;
using Core.Utilities.Results.Concrete.SuccessResults;
using Core.Utilities.Security.Abstract;
using Entities.Concrete;
using Entities.DTOs.AuthDTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        public AuthManager(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task<IResult> AssignRoleToUserAsync(string userId, string[] role)
        {
            var checkUser = await _userManager.FindByIdAsync(userId);
            if(checkUser == null)
                return new ErrorResult(message: AuthMessage.UserNotFound, statusCode: HttpStatusCode.NotFound);

            var result = await _userManager.AddToRolesAsync(checkUser, role);

            if(result.Succeeded)
                return new SuccessResult(HttpStatusCode.OK);
            else
            {
                string response = string.Empty;
                foreach (var error in result.Errors)
                {
                    response += error.Description + ". ";
                }
                return new ErrorResult(response, HttpStatusCode.BadRequest);
            }
        }

        public async Task<IDataResult<Token>> LoginAsync(LoginDTO loginDTO)
        {
            var checkUser = await _userManager.FindByEmailAsync(loginDTO.UsernameOrEmail);
            if(checkUser == null)
                checkUser = await _userManager.FindByNameAsync(loginDTO.UsernameOrEmail);

            if(checkUser == null)
                return new ErrorDataResult<Token>(AuthMessage.UserNotFound, HttpStatusCode.BadRequest);

            var result = await _signInManager.PasswordSignInAsync(checkUser, loginDTO.Password, true, false);
            var roles = await _userManager.GetRolesAsync(checkUser);

            if (result.Succeeded)
            {
                Token token = await _tokenService.CreateAccessToken(checkUser, roles.ToList());
                var response = await UpdateRefreshToken(token.RefreshToken, checkUser);
                return new SuccessDataResult<Token>(data: token, HttpStatusCode.OK);
            }
            else
            {
                return new ErrorDataResult<Token>("Parol və ya E-poçt yanlışdır!", HttpStatusCode.BadRequest);
            }

        }

        public async Task<IResult> LogoutAsync(string userId)
        {
            var findUser = await _userManager.FindByIdAsync(userId);
            if (findUser == null)
                return new ErrorResult(AuthMessage.UserNotFound, HttpStatusCode.Unauthorized);

            findUser.RefreshToken = null;
            findUser.RefreshTokenExpiredDate = null;

            await _userManager.UpdateAsync(findUser);
            return new SuccessResult(HttpStatusCode.OK);
        }

        public async Task<IDataResult<Token>> RefreshTokenLoginAsync(string refreshToken)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);
            var roles = await _userManager.GetRolesAsync(user);
            if (user != null && user.RefreshTokenExpiredDate > DateTime.Now)
            {
                Token token = await _tokenService.CreateAccessToken(user, roles.ToList());
                token.RefreshToken = refreshToken;
                return new SuccessDataResult<Token>(data: token, HttpStatusCode.OK);
            }
            else
                return new ErrorDataResult<Token>(message: AuthMessage.UserNotFound, HttpStatusCode.BadRequest);
        }

        public async Task<IResult> RegisterAsync(RegisterDTO registerDTO)
        {
            var validator = new RegisterValidation();
            var validationResult = validator.Validate(registerDTO);
            if (!validationResult.IsValid)
                return new ErrorResult(message: validationResult.ToString(), HttpStatusCode.BadRequest);

            var checkEmail = await _userManager.FindByEmailAsync(registerDTO.Email);
            if (checkEmail != null)
                return new ErrorResult("Email is already exist", System.Net.HttpStatusCode.BadRequest);

            var checkUsername = await _userManager.FindByNameAsync(registerDTO.Username);

            if (checkUsername != null)
                return new ErrorResult("Username is already exist", System.Net.HttpStatusCode.BadRequest);

            User newUser = new()
            {
                Firstname = registerDTO.Firstname,
                Lastname = registerDTO.Lastname,
                Email = registerDTO.Email,
                UserName = registerDTO.Username
            };

            var result = await _userManager.CreateAsync(newUser, registerDTO.Password);

            if (result.Succeeded)
                return new SuccessResult(System.Net.HttpStatusCode.Created);
            else
            {
                string response = string.Empty;
                foreach (var error in result.Errors)
                {
                    response += error.Description + ".";
                }
                return new ErrorResult(response, System.Net.HttpStatusCode.BadRequest);
            }
        }

        public async Task<IDataResult<string>> UpdateRefreshToken(string refreshToken, AppUser appUser)
        {
            if (appUser is not null)
            {
                appUser.RefreshToken = refreshToken;
                appUser.RefreshTokenExpiredDate = DateTime.Now.AddMonths(1);
                IdentityResult identityResult = await _userManager.UpdateAsync(appUser);
                if (identityResult.Succeeded)
                    return new SuccessDataResult<string>(data: refreshToken, HttpStatusCode.OK);
                else
                {
                    string response = string.Empty;
                    foreach (var error in identityResult.Errors)
                    {
                        response += error.Description + ".";
                    }
                    return new ErrorDataResult<string>(statusCode: HttpStatusCode.BadRequest, message: response);
                }
            }
            else
                return new ErrorDataResult<string>(message: "İstifadəçi tapılmadı", HttpStatusCode.Unauthorized);
        }


    }
}
