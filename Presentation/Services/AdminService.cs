using Core.Entities;
using Core.Helper;
using Core.Helpers;
using Data.Repositories.Abstract;
using Data.Repositories.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Services
{
    class AdminService
    {
        private readonly AdminRepository _adminRepos;
        public AdminService()
        {
            _adminRepos = new AdminRepository();
        }
        public Admin Authorize()
        {
        LoginCheck:
            Console.WriteLine("\n---- Login ----");
            Console.Write("Username :");
            string username = Console.ReadLine();

            Console.Write("Password :");
            var password = string.Empty;
            ConsoleKey key;
            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && password.Length > 0)
                {
                    Console.Write("\b \b");
                    password = password[0..^1];
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    Console.Write("*");
                    password += keyInfo.KeyChar;
                }
            } while (key != ConsoleKey.Enter);


            var admin = _adminRepos.GetByUsernameAndPassword(username, password);
            if (admin is null)
            {
                Console.Clear();
                ConsoleHelper.WriteWithColor("Username or password is incorrect", ConsoleColor.Red);
                goto LoginCheck;
            }
            return admin;
        }
    }
}
