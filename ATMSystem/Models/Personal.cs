using System;
using System.Collections.Generic;
using System.Text;

namespace ATMSystem.ATMSystem.Models
{
    internal class Personal : User
    {
        public override CustomerType Type { get; } = CustomerType.Personal;
        public override string? Password { get; set; }
        protected override decimal MaximumLimit => 100000.00m;
        public override decimal Balance { get; set; } = 10.00m;
        protected override decimal Tax { get; } = 10.00m;

        public Personal() { }

        public Personal(string password, string name) : base(password, name)
        {
            if (ValidatePassword(password)) { Password = password; }
            if (ValidName(name)) { Name = name; }
        }


        public bool Deposit(decimal cash)
        {
            if (cash <= 0) { Console.WriteLine("Depósito não pode ser abaixo de 0.00"); return false; }

            if (Balance + cash > MaximumLimit){ 
                
                Console.WriteLine("Account balance limit exceeded."); return false; }

            else { Balance += cash; return true; }
        }

        public bool Sake(decimal sake)
        {
            decimal balance = Balance;

            if (sake > balance) { Console.WriteLine("Saque acima do valor em conta."); return false; }
            else if (sake <= 0) { Console.WriteLine("Digite um valor para sacar."); return false; }
            else
            {
                Balance -= (Tax + sake);
                return true;
            }


        }
    }
}
