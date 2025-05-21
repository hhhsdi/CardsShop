using GpuStore.Services;
using GpuStore.Utilities;
using System;

namespace GpuStore.Presentation.Menus
{
    public static class CatalogMenu
    {
        private static readonly GpuService _gpuService = new GpuService();

        public static void Display()
        {
            try
            {
                ConsoleUtilities.ClearScreen();
                Console.WriteLine("=== КАТАЛОГ ВИДЕОКАРТ ===");

                var gpus = _gpuService.GetAllGpus();

                if (gpus.Count == 0)
                {
                    ConsoleUtilities.ShowWarning("Каталог пуст");
                    Console.ReadKey();
                    return;
                }

                foreach (var gpu in gpus)
                {
                    Console.WriteLine($"{gpu.Id}. {gpu.Manufacturer} {gpu.Model} - {gpu.Price:C}");
                }

                Console.WriteLine("\n1. Добавить в корзину");
                Console.WriteLine("2. Фильтровать");
                Console.WriteLine("0. Назад");

                int choice = InputValidator.GetIntInput("Выберите действие: ", 0, 2);

                switch (choice)
                {
                    case 1:
                        AddToCart();
                        break;
                    case 2:
                        FilterGpus();
                        break;
                }
            }
            catch (Exception ex)
            {
                ConsoleUtilities.ShowError(ex.Message);
                Console.ReadKey();
            }
        }

        private static void AddToCart()
        {
            int gpuId = InputValidator.GetIntInput("Введите ID видеокарты: ", 1);
            int quantity = InputValidator.GetIntInput("Введите количество: ", 1, 10);

            // Здесь будет вызов сервиса корзины
            ConsoleUtilities.ShowSuccess("Товар добавлен в корзину!");
            Console.ReadKey();
        }

        private static void FilterGpus()
        {
            ConsoleUtilities.ClearScreen();
            Console.WriteLine("=== ФИЛЬТРЫ ===");

            decimal minPrice = InputValidator.GetDecimalInput("Минимальная цена: ", 0);
            decimal maxPrice = InputValidator.GetDecimalInput("Максимальная цена: ", minPrice);

            // Здесь будет вызов сервиса с фильтрацией
            ConsoleUtilities.ShowSuccess("Фильтр применен");
            Console.ReadKey();
        }
    }
}