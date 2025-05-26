using System;

public class Program
{
    public static void Main(string[] args)
    {
        DatabaseManager.CreateDatabase();

        DatabaseManager.TestConnection();

        ConsoleUI.Run();
    }
}