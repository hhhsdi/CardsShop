using GpuStore.Presentation;
using GpuStore.Utilities;
using static System.Net.Mime.MediaTypeNames;

namespace GpuStore
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.CursorVisible = false;

            try
            {
                Presentation.Application.Run();
            }
            catch (Exception ex)
            {
                ConsoleUtilities.ShowError($"Критическая ошибка: {ex.Message}");
                ConsoleUtilities.LogError(ex);
                Console.ReadKey();
            }
        }
    }
}