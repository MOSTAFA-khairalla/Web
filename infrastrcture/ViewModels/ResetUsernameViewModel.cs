using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infrastrcture.ViewModels
{
    public class ResetUsernameViewModel
    {
        public string Email { get; set; }
        public string OldUsername { get; set; }

        [Required]
        public string NewUsername { get; set; }
    }
}
