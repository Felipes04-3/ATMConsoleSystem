using System;
using System.Collections.Generic;
using System.Text;

namespace ATMSystem.ATMSystem.Handlers
{
    internal class LogService
    {

        // Define o seu caminho personalizado utilizando o seu diretório do OneDrive
        private readonly string _folderLogs = @"C:\Users\felps\OneDrive\Documentos\Estudos\C#\C#_projects\Logs";

        public void Record(Guid userId, string transactionType, decimal cash, decimal balance)
        {

            // Garante que a pasta "Logs" será criada dentro do seu caminho de estudos se ela não existir
            Directory.CreateDirectory(_folderLogs);


            //Define o arquivo individual do usuário (ex: historico_usuario_1.txt)
            string filePath = Path.Combine(_folderLogs, $"historic_user_{userId}.txt");

            string historicLine = $"[Data: {DateTime.Now:dd/MM/yyyy  HH:mm:ss}] | Operation: {transactionType} | Amount: R$ {cash:N2} | Current Balance: R$ {balance:N2}{Environment.NewLine}";

            // Adiciona no final do arquivo sem sobrescrever os registros anteriores
            File.AppendAllText(filePath, historicLine);
        }


        public string GetRecords(Guid userId)
        {
            string filePath = Path.Combine(_folderLogs, $"historic_user_{userId}.txt");

            if (!File.Exists(filePath))
            {
                return "No transaction record found.";
            }

            // Lê todo o conteúdo acumulado do arquivo de texto de uma só vez
            return File.ReadAllText(filePath);
        }
    }
}
