using System;

namespace GpuStore.Utilities
{
    public static class ConsoleUtilities
    {
        public static void ShowError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Ошибка: {message}");
            Console.ResetColor();
        }

        public static void ShowSuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void ShowWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Внимание: {message}");
            Console.ResetColor();
        }

        public static void LogError(Exception ex)
        {
            // В реальном приложении здесь должна быть запись в лог-файл
            Console.WriteLine($"Детали ошибки: {ex}");
        }

        public static void ClearScreen()
        {
            Console.Clear();
            Console.WriteLine("=== ОНЛАЙН-МАГАЗИН ВИДЕОКАРТ ===");
            Console.WriteLine();
        }
    }
}