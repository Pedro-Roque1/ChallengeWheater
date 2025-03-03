﻿using System.ComponentModel.DataAnnotations;

namespace WeatherAppApi.Models.Requests
{
    public class UserLoginRequest
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
