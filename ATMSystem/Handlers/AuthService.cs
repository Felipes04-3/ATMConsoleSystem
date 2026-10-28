using System;
using System.Collections.Generic;
using System.Text;
using ATMSystem.ATMSystem.Models;
using ATMSystem.ATMSystem.Data;

namespace ATMSystem.ATMSystem.Handlers
{
    internal class AuthService
    {
        private readonly IRepositoryService _userRepository;


        public AuthService(IRepositoryService userRepository)
        {
            _userRepository = userRepository;
        }


        public User? AuthUser(string password)
        {
            User? user = _userRepository.GetByPassword(password);

            if (user == null) { Console.WriteLine("User not found or Password is incorrect."); return null; }
            else { Console.WriteLine("User logged successfully."); return user; }
        }
    }
}
