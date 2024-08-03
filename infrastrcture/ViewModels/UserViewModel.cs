using Core.Enities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infrastrcture.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
