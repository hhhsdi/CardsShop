using GpuStore.Utilities;

namespace GpuStore.Presentation.Menus
{
    public static class OrdersMenu
    {
        public static void Display()
        {
            ConsoleUtilities.ClearScreen();
            Console.WriteLine("=== МОИ ЗАКАЗЫ ===");
            Console.WriteLine("Список ваших заказов...");

            Console.WriteLine("\n1. Просмотреть детали заказа");
            Console.WriteLine("0. Назад");

            int choice = InputValidator.GetIntInput("Выберите действие: ", 0, 1);

            if (choice == 1)
            {
                ViewOrderDetails();
            }
        }

        private static void ViewOrderDetails()
        {
            int orderId = InputValidator.GetIntInput("Введите ID заказа: ", 1);
            ConsoleUtilities.ShowSuccess($"Детали заказа {orderId}");
            Console.ReadKey();
        }
    }
}