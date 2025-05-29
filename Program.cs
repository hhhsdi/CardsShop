public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            // Инициализация БД
            DatabaseManager.CreateDatabase();
            DatabaseManager.TestConnection();

            // Настройка зависимостей
            var videoCardRepo = new VideoCardRepository();
            var manufacturerRepo = new ManufacturerRepository();

            var videoCardService = new VideoCardService(videoCardRepo, manufacturerRepo);
            var manufacturerService = new ManufacturerService(manufacturerRepo);

            // Запуск приложения
            var consoleUI = new ConsoleUI(videoCardService, manufacturerService);
            consoleUI.Run();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fatal error: {ex.Message}");
            Console.WriteLine(ex.StackTrace);
        }
    }
}