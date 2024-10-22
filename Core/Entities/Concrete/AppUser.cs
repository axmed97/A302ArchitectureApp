﻿using Microsoft.AspNetCore.Identity;


namespace Core.Entities.Concrete
{
    public class AppUser : IdentityUser
    {
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiredDate { get; set; }
        public string? PhotoPath { get; set; }
    }
}