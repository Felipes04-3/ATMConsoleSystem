using System;
using System.Collections.Generic;
using System.Text;
using ATMSystem.ATMSystem.Models;

namespace ATMSystem.ATMSystem.Data
{
    internal class Repository : IRepositoryService
    {
        public List<User> _users { get; set; } = new List<User>();


        public void AddUsers(User user)
        {
            _users.Add(user);
        }

        public bool Update(string name, string newName)
        {

            User? user = _users.FirstOrDefault(u => u.Name == name);
            if (user == null) { Console.WriteLine("User not found or Name is incorrect"); return false; }

            else { user.Name = newName;  return true; }


        }

        public bool Delete(string name, string password)
        {
            User? user = _users.FirstOrDefault(u => u.Name == name && u.Password == password);

            if (user == null) { Console.WriteLine("User not found or Name and Password are incorrect"); return false; }
            else { _users.Remove(user); return true; }
        }

        public User? GetByPassword(string password)
        {
            return _users.FirstOrDefault(u => u.Password == password);
        }
    }
}
