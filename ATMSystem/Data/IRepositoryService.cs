using System;
using System.Collections.Generic;
using System.Text;
using ATMSystem.ATMSystem.Models;

namespace ATMSystem.ATMSystem.Data
{
    internal interface IRepositoryService
    {
        List<User> _users { get; set; }

        void AddUsers(User user);

        bool Update(string name, string newName);

        bool Delete(string name, string password);
        User? GetByPassword(string password);
    }
}
