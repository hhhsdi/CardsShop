using GpuStore.Utilities;

namespace GpuStore.Presentation.Menus
{
    public static class AuthMenu
    {
        public static void Display()
        {
            ConsoleUtilities.ClearScreen();
            Console.WriteLine("=== АВТОРИЗАЦИЯ ===");

            Console.WriteLine("1. Вход");
            Console.WriteLine("2. Регистрация");
            Console.WriteLine("0. Назад");

            int choice = InputValidator.GetIntInput("Выберите действие: ", 0, 2);

            switch (choice)
            {
                case 1:
                    Login();
                    break;
                case 2:
                    Register();
                    break;
            }
        }

        private static void Login()
        {
            string email = InputValidator.GetNonEmptyString("Email: ");
            string password = InputValidator.GetNonEmptyString("Пароль: ");

            ConsoleUtilities.ShowSuccess("Вход выполнен успешно!");
            Console.ReadKey();
        }

        private static void Register()
        {
            string email = InputValidator.GetNonEmptyString("Email: ");
            string password = InputValidator.GetNonEmptyString("Пароль: ");
            string confirmPassword = InputValidator.GetNonEmptyString("Подтвердите пароль: ");

            ConsoleUtilities.ShowSuccess("Регистрация прошла успешно!");
            Console.ReadKey();
        }
    }
}