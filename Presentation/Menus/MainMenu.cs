using GpuStore.Utilities;

namespace GpuStore.Presentation.Menus
{
    public static class MainMenu
    {
        public static void Display()
        {
            ConsoleUtilities.ClearScreen();

            Console.WriteLine("1. Каталог видеокарт");
            Console.WriteLine("2. Корзина");
            Console.WriteLine("3. Мои заказы");
            Console.WriteLine("4. Войти в систему");
            Console.WriteLine("0. Выход");

            int choice = InputValidator.GetIntInput("Выберите пункт меню: ", 0, 4);

            switch (choice)
            {
                case 1:
                    CatalogMenu.Display();
                    break;
                case 2:
                    CartMenu.Display();
                    break;
                case 3:
                    OrdersMenu.Display();
                    break;
                case 4:
                    AuthMenu.Display();
                    break;
                case 0:
                    Application.Exit();
                    break;
            }
        }
    }
}