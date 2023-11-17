using AuthenticatorApp.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AuthenticatorMvc.Controllers
{
    public class AuthContext : DbContext
    {

        public AuthContext(DbContextOptions<AuthContext> options) : base(options)
        {

        }

        public DbSet<UserViewModel> Users { get; set; }

    }
}


 