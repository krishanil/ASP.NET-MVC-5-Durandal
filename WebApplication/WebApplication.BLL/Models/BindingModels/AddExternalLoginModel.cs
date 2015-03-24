﻿using System.ComponentModel.DataAnnotations;

namespace WebApplication.BLL.Models.BindingModels
{
    public class AddExternalLoginModel
    {
        [Required]
        [Display(Name = "External access token")]
        public string ExternalAccessToken { get; set; }
    }
}