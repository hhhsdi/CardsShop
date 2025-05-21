using GpuStore.Utilities;

namespace GpuStore.Presentation.Menus
{
    public static class CartMenu
    {
        public static void Display()
        {
            ConsoleUtilities.ClearScreen();
            Console.WriteLine("=== КОРЗИНА ===");
            Console.WriteLine("Здесь будет содержимое корзины...");

            Console.WriteLine("\n1. Оформить заказ");
            Console.WriteLine("2. Удалить товар");
            Console.WriteLine("0. Назад");

            int choice = InputValidator.GetIntInput("Выберите действие: ", 0, 2);

            switch (choice)
            {
                case 1:
                    Checkout();
                    break;
                case 2:
                    RemoveItem();
                    break;
            }
        }

        private static void Checkout()
        {
            ConsoleUtilities.ShowSuccess("Заказ оформлен!");
            Console.ReadKey();
        }

        private static void RemoveItem()
        {
            int itemId = InputValidator.GetIntInput("Введите ID товара для удаления: ", 1);
            ConsoleUtilities.ShowSuccess($"Товар {itemId} удален из корзины");
            Console.ReadKey();
        }
    }
}