using Core.Entities;
using Core.Helper;
using Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Concrete
{
    public class AdminRepository
    {
        public Admin GetByUsernameAndPassword(string username, string password)
        {
            return DbContext.Admins.FirstOrDefault(a => a.Username.ToLower() == username.ToLower() && PasswordHasher.Decrypt(a.Password) == password);
        }
    }
}
