﻿using Microsoft.AspNetCore.Identity;

namespace MobileShop.Models
{
    public class User : IdentityUser
    {
        public string Role { get; set; }
    }
}
