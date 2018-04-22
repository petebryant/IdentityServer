using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Models.SecureViewModels
{
    public class IndexViewModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
    }
}
