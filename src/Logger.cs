﻿// Ignore Spelling: webcrawler
//9 out of 10
using System;
using System.IO;
namespace webcrawler.Logs
{
    class Logger
    {
        private readonly string log_path = AppDomain.CurrentDomain.BaseDirectory + "logs.txt";
        private readonly ConsoleColor warningColour = ConsoleColor.DarkYellow;
        private readonly ConsoleColor errorColour = ConsoleColor.Red;
        private readonly ConsoleColor infoColour = ConsoleColor.Green;
        private readonly object lockObject = new object();
        public void Warning(string msg)
        {
            DateTime currentDateTime = DateTime.Now;
            WriteColoredTextToFile(log_path, $"WARNING {msg} - {currentDateTime}", warningColour);
        }
        public void Error(Exception ex)
        {
            DateTime currentDateTime = DateTime.Now;
            WriteColoredTextToFile(log_path, $"ERROR: {ex.Message} - Stack Trace: {ex.StackTrace} - {currentDateTime}", errorColour);
        }
        public void Info(string msg)
        {
            DateTime currentDateTime = DateTime.Now;
            WriteColoredTextToFile(log_path, $"INFO {msg} - {currentDateTime}", infoColour);
        }
        private void WriteColoredTextToFile(string filePath, string text, ConsoleColor color)
        {
            ConsoleColor originalColor = Console.ForegroundColor;

            lock (lockObject)
            {
                try
                {
                    Console.ForegroundColor = color;
                    using (StreamWriter writer = File.AppendText(filePath))
                    {
                        writer.WriteLine(text);
                    }
                }
                finally
                {
                    Console.ForegroundColor = originalColor;
                }
            }
        }
    }
}
