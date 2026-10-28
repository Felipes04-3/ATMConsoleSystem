namespace ATMSystem.ATMSystem.Models
{

    public enum CustomerType
    {
        Personal,
        Business
    }

    public abstract class User : BaseEntity
    {
        public string? Name { get; set; }
        public abstract decimal Balance { get; set; }
        public abstract string? Password { get; set; }
        protected abstract decimal MaximumLimit{ get; }
        protected abstract decimal Tax { get; }

        public abstract CustomerType Type { get; }


        public User() { }

       protected User(string password, string name)
       {
            if (ValidName(name)) { Name = name; }
       }


        public bool ValidName(string name)
        {
            if (String.IsNullOrWhiteSpace(name)) { Console.WriteLine("Name cannot be null."); return false; }
            else if (name.Length < 4 || name.Length > 15) { Console.WriteLine("Name must be beetwen 4 and 15 characters"); return false; }
            else { Name = name; return true; }
        }


        public bool ValidatePassword(string password)
        {
            if (String.IsNullOrWhiteSpace(password)) { Console.WriteLine("Password cannot be null."); return false; }
            else if (password.Length < 4 || password.Length > 10)
            {
                Console.WriteLine("password must be between 4 and 10 characters");
                return false;
            }
            else { Password = password; return true; }
        }
    }
}
