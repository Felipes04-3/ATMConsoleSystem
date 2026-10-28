using ATMSystem.ATMSystem.Data;
using ATMSystem.ATMSystem.Handlers;
using ATMSystem.ATMSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ATMSystem.ATMSystem.ConsoleUI
{
    internal class InputHandler
    {

        private readonly User _user;
        private readonly Repository _repository;

        public InputHandler(User user, Repository repository)
        {
            _user = user;
            _repository = repository;
        }

        // Construtor para o menu inicial que gerencia o repositório geral
        public InputHandler(Repository repository)
        {
            _repository = repository;
        }

        public static bool ValidType(string input)
        {
            if (int.TryParse(input, out _))
            {
                return false;
            }
            return true;
        }

        public void AddUserResponse()
        {
            while (true)
            {
                try
                {
                    if (_user == null) break;

                    Console.WriteLine("Type your name: ");
                    string? name = Console.ReadLine();

                    if (!_user.ValidName(name) || !ValidType(name))
                    {
                        throw new ArgumentException("Name is incorrect. Try again.");
                    }

                    Console.WriteLine("Type your password: ");
                    string? password = Console.ReadLine();

                    if (!_user.ValidatePassword(password))
                    {
                        throw new ArgumentException("Password is incorrect. Try again.");
                    }

                    _repository.AddUsers(_user);
                    break;
                }
                catch (ArgumentException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"[Error] {ex.Message}\n");
                    Console.ResetColor();
                    continue;
                }
            }
        }


        public static void Start()
        {
            Repository repository = new Repository();
            LogService logService = new LogService();
            TransactionService transactionService = new TransactionService(repository, logService);

            while (true)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("\n     ┌──────────────────────────────────────────────────┐");
                Console.WriteLine("     │                    NU-ATM        HOME            │");
                Console.WriteLine("     └──────────────────────────────────────────────────┘");

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("     │                                                  │");
                Console.WriteLine("     │    [1] Entrar na Conta (Login por Senha)         │");
                Console.WriteLine("     │    [2] Cadastrar Novo Usuário                    │");
                Console.WriteLine("     │    [3] Atualizar Nome de Usuário (Update)        │");
                Console.WriteLine("     │    [4] Deletar Conta (Delete)                    │");
                Console.WriteLine("     │    [5] Sair da Aplicação                         │");
                Console.WriteLine("     │                                                  │");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("     └──────────────────────────────────────────────────┘");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\n     Escolha uma opção: ");
                string? opcao = Console.ReadLine();

                if (opcao == "1")
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("\n     ┌──────────────────────────────────────────────────┐");
                    Console.WriteLine("     │                   AUTENTICAÇÃO                   │");
                    Console.WriteLine("     └──────────────────────────────────────────────────┘");

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("\n     Digite sua senha: ");
                    string? senha = Console.ReadLine();

                    if (!string.IsNullOrEmpty(senha))
                    {
                        User? loggedUser = repository.GetByPassword(senha);

                        if (loggedUser != null)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\n     [Sucesso] User logged successfully.");
                            System.Threading.Thread.Sleep(1200);

                            // Transiciona para o menu principal logado
                            MainMenu(loggedUser, transactionService);
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\n     [Erro] User not found or Password is incorrect.");
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            WaitKeyBlinking("     Pressione [ENTER] para continuar...");
                            
                        }
                    }
                }
                else if (opcao == "2")
                {
                    // --- CADASTRO ---
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("\n     ┌──────────────────────────────────────────────────┐");
                    Console.WriteLine("     │                  NOVO CADASTRO                   │");
                    Console.WriteLine("     └──────────────────────────────────────────────────┘");

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("\n     Digite o tipo de conta ([1] Personal / [2] Corporate): ");
                    string? tipoConta = Console.ReadLine();

                    User novoUsuario = (tipoConta == "2") ? new Corporate() : new Personal();

                    InputHandler inputHandler = new InputHandler(novoUsuario, repository);
                    inputHandler.AddUserResponse();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n     [Sucesso] Processo concluído!");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    WaitKeyBlinking("    Pressione [ENTER] para voltar ao menu...");
                    
                }
                else if (opcao == "3")
                {
                    // --- UPDATE ---
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("\n     ┌──────────────────────────────────────────────────┐");
                    Console.WriteLine("     │                 ATUALIZAR CADASTRO               │");
                    Console.WriteLine("     └──────────────────────────────────────────────────┘");

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("\n     Digite o nome atual do usuário: ");
                    string? nomeAtual = Console.ReadLine();

                    Console.Write("     Digite o novo nome: ");
                    string? novoNome = Console.ReadLine();

                    if (!string.IsNullOrEmpty(nomeAtual) && !string.IsNullOrEmpty(novoNome))
                    {
                        bool atualizado = repository.Update(nomeAtual, novoNome);
                        if (atualizado)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\n     [Sucesso] Nome atualizado com sucesso!");
                        }
                    }

                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    WaitKeyBlinking("    Pressione [ENTER] para voltar ao menu...");
                   
                }
                else if (opcao == "4")
                {
                    // --- DELETE ---
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("\n     ┌──────────────────────────────────────────────────┐");
                    Console.WriteLine("     │                   EXCLUIR CONTA                  │");
                    Console.WriteLine("     └──────────────────────────────────────────────────┘");

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("\n     Digite o nome do usuário: ");
                    string? nome = Console.ReadLine();

                    Console.Write("     Confirme a senha da conta: ");
                    string? senha = Console.ReadLine();

                    if (!string.IsNullOrEmpty(nome) && !string.IsNullOrEmpty(senha))
                    {
                        bool deletado = repository.Delete(nome, senha);
                        if (deletado)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\n     [Sucesso] Conta removida com sucesso!");
                        }
                    }

                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    WaitKeyBlinking("     Pressione [ENTER] para voltar ao menu...");
                }
                else if (opcao == "5")
                {
                    Console.ResetColor();
                    Console.Clear();
                    break;
                }
            }
        }


        public static void AuthMenu(User loggedUser)
        {
            if (loggedUser == null) return;

            Console.SetCursorPosition(0, 0);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            string formattedBalance = $"R$ {loggedUser.Balance:N2}";
            string maskedBalance = new string('*', formattedBalance.Length);

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("     ┌──────────────────────────────────────────────────┐");
            Console.WriteLine("     │                    NU-ATM        v1.0.1          │");
            Console.WriteLine("     └──────────────────────────────────────────────────┘");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"     │  Olá, {loggedUser.Name,-42} │");

            string tipoContaDescricao = loggedUser.Type == CustomerType.Personal ? "Conta Pessoal" : "Conta Empresarial";
            Console.WriteLine($"     │  Tipo: {tipoContaDescricao,-41} │");

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("     ├──────────────────────────────────────────────────┤");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"     │  Saldo Disponível:                               │");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"     │  {maskedBalance,-47} │");

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("     ├──────────────────────────────────────────────────┤");

            Console.ForegroundColor = ConsoleColor.Gray;
            if (loggedUser is Personal)
            {
                Console.WriteLine("     │  Taxa por Saque: R$ 10,00                        │");
            }
            else if (loggedUser is Corporate)
            {
                Console.WriteLine("     │  Taxa por Saque: R$ 25,00                        │");
            }

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("     └──────────────────────────────────────────────────┘");
            Console.ResetColor();
        }


        public static void MainMenu(User userLogged, TransactionService transactionService)
        {
            LogService logService = new LogService();

            if (userLogged == null) return;

            while (true)
            {
                Console.Clear();
                AuthMenu(userLogged);

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("\n     ┌──────────────────────────────────────────────────┐");
                Console.WriteLine("     │                  MENU PRINCIPAL                  │");
                Console.WriteLine("     ├──────────────────────────────────────────────────┤");
                Console.WriteLine("     │    [1] Realizar Depósito                         │");
                Console.WriteLine("     │    [2] Realizar Saque                            │");
                Console.WriteLine("     │    [3] Ver Histórico de Transações               │");
                Console.WriteLine("     │    [4] Sair / Deslogar                           │");
                Console.WriteLine("     └──────────────────────────────────────────────────┘");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\n     Escolha uma opção: ");
                string? opcao = Console.ReadLine();

                if (opcao == "1")
                {
                    Console.Clear();
                    AuthMenu(userLogged);

                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("\n     --- DEPÓSITO ---");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("     Digite o valor do depósito (R$): ");

                    if (decimal.TryParse(Console.ReadLine(), out decimal valorDeposito))
                    {
                        Console.Write("     Confirme sua senha: ");
                        string? senha = Console.ReadLine();

                        if (!string.IsNullOrEmpty(senha))
                        {
                            transactionService.DepositResponse(senha, valorDeposito);
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("     [Erro] Valor inválido.");
                    }

                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    WaitKeyBlinking("     Pressione [ENTER] para continuar...");
                }
                else if (opcao == "2")
                {
                    Console.Clear();
                    AuthMenu(userLogged);

                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("\n     --- SAQUE ---");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("     Digite o valor do saque (R$): ");

                    if (decimal.TryParse(Console.ReadLine(), out decimal valorSaque))
                    {
                        Console.Write("     Confirme sua senha: ");
                        string? senha = Console.ReadLine();

                        if (!string.IsNullOrEmpty(senha))
                        {
                            transactionService.SakeResponse(senha, valorSaque);
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("     [Erro] Valor inválido.");
                    }

                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    WaitKeyBlinking("     Pressione [ENTER] para continuar...");
                }
                else if (opcao == "3")
                {
                    
                    Console.Clear();
                    AuthMenu(userLogged);

                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("\n     --- HISTÓRICO DE TRANSAÇÕES ---");
                    Console.ForegroundColor = ConsoleColor.White;

                    // Busca os registros usando o Id do usuário logado
                    string historico = logService.GetRecords(userLogged.Id);

                    Console.WriteLine($"\n{historico}");

                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    WaitKeyBlinking("     Pressione [ENTER] para voltar ao menu...");
                }
                else if (opcao == "4")
                {
                    Console.ResetColor();
                    break;
                }
            }
        }


        public static void WaitKeyBlinking(string mensagem)
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write($"\r{mensagem}");
                Console.ResetColor();

                // Espera meio segundo verificando se o usuário apertou alguma tecla
                for (int i = 0; i < 10; i++)
                {
                    if (Console.KeyAvailable)
                    {
                        Console.ReadKey(true);
                        Console.WriteLine(); // Quebra a linha ao sair para o fluxo continuar limpo
                        return;
                    }
                    Thread.Sleep(50);
                }

                Console.Write($"\r{new string(' ', mensagem.Length)}");

                for (int i = 0; i < 10; i++)
                {
                    if (Console.KeyAvailable)
                    {
                        Console.ReadKey(true);
                        Console.WriteLine();
                        return;
                    }
                    Thread.Sleep(50);
                }
            }
        }
    }
}
