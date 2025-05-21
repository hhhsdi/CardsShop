using GpuStore.Presentation.Menus;

namespace GpuStore.Presentation
{
    public static class Application
    {
        private static bool _isRunning = true;

        public static void Run()
        {
            while (_isRunning)
            {
                MainMenu.Display();
            }
        }

        public static void Exit()
        {
            _isRunning = false;
        }
    }
}