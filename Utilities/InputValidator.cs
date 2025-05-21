using System;

namespace GpuStore.Utilities
{
    public static class InputValidator
    {
        public static int GetIntInput(string prompt, int min = int.MinValue, int max = int.MaxValue)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int result) && result >= min && result <= max)
                {
                    return result;
                }
                ConsoleUtilities.ShowError($"Введите целое число от {min} до {max}");
            }
        }

        public static string GetNonEmptyString(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine().Trim();
                if (!string.IsNullOrEmpty(input))
                {
                    return input;
                }
                ConsoleUtilities.ShowError("Поле не может быть пустым");
            }
        }

        public static decimal GetDecimalInput(string prompt, decimal min = decimal.MinValue, decimal max = decimal.MaxValue)
        {
            while (true)
            {
                Console.Write(prompt);
                if (decimal.TryParse(Console.ReadLine(), out decimal result) && result >= min && result <= max)
                {
                    return result;
                }
                ConsoleUtilities.ShowError($"Введите число от {min} до {max}");
            }
        }
    }
}