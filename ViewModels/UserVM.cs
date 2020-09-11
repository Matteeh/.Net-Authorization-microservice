using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Authorization.ViewModels
{

    public class UserVM
    {
        public string Id { get; set; }
        public string Email { get; set; }
    }
}