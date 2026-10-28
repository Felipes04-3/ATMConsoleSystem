using ATMSystem.ATMSystem.Models;
using ATMSystem.ATMSystem.Data;


namespace ATMSystem.ATMSystem.Handlers
{
    internal class TransactionService
    {
        private readonly IRepositoryService _userRepository;
        private readonly LogService _logService = new();


        public TransactionService(IRepositoryService userRepository, LogService logService)
        {
            _userRepository = userRepository;
            _logService = logService;
        }


        public void DepositResponse(string password, decimal cash)
        {
            User? user = _userRepository.GetByPassword(password);

            if ( user == null) { 
                Console.WriteLine("Password incorrect."); return;
            }

            bool operation = false;

            if (user is Personal personalUser)
            {
                operation = personalUser.Deposit(cash);
            }
            else if (user is Corporate corporateUser)
            {
                operation = corporateUser.Deposit(cash);
            }

            if (operation)
            {

                // Registra no arquivo .txt apenas se o depósito foi bem-sucedido
                _logService.Record(user.Id, "Deposit", cash, user.Balance);

                Console.WriteLine("Deposit Successfully.");
            }
        }


        public void SakeResponse(string password, decimal cash)
        {
            User? user = _userRepository.GetByPassword(password);

            if (user == null) { 
                Console.WriteLine("Password is incorrect."); return; 
            }

            bool operation = false;

            if (user is Personal personalUser)
            {
                operation = personalUser.Sake(cash);
            }
            else if (user is Corporate corporateUser)
            {
                operation = corporateUser.Sake(cash);
            }

            if (operation)
            {
                // Registra no arquivo .txt apenas se o saque foi bem-sucedido
                _logService.Record(user.Id, "Sake", cash, user.Balance);

                Console.WriteLine("Sake Successfully.");
            }
        }
    }
}
