using System;
using System.Data.SQLite;
using System.IO;

public class DatabaseManager
{
    private const string DatabasePath = "videocard_store.db"; // Путь к файлу БД

    public static void CreateDatabase()
    {
        if (!File.Exists(DatabasePath))
        {
            SQLiteConnection.CreateFile(DatabasePath);
            Console.WriteLine("Database created successfully.");

            // Execute the SQL script to create tables and seed data
            ExecuteSqlScript("CreateDatabase.sql"); // Замените на имя вашего файла
        }
        else
        {
            Console.WriteLine("Database already exists.");
        }
    }

    public static void ExecuteSqlScript(string scriptPath)
    {
        try
        {
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={DatabasePath};Version=3;"))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand("", connection))
                {
                    // Read the SQL script from the file
                    string script = File.ReadAllText(scriptPath);

                    // Execute the script
                    command.CommandText = script;
                    command.ExecuteNonQuery();
                }
                Console.WriteLine($"SQL script '{scriptPath}' executed successfully.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error executing SQL script: {ex.Message}");
        }
    }

    public static SQLiteConnection GetConnection()
    {
        SQLiteConnection connection = new SQLiteConnection($"Data Source={DatabasePath};Version=3;");
        return connection;
    }

    public static void TestConnection()
    {
        try
        {
            using (SQLiteConnection connection = GetConnection())
            {
                connection.Open();
                Console.WriteLine("Connection to database successful!");

                // Simple query to test
                using (SQLiteCommand command = new SQLiteCommand("SELECT COUNT(*) FROM VideoCards", connection))
                {
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    Console.WriteLine($"Number of video cards in database: {count}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error connecting to database: {ex.Message}");
        }
    }

    public static void CreateDatabaseFileIfNotExists()
    {
        if (!File.Exists(DatabasePath))
        {
            SQLiteConnection.CreateFile(DatabasePath);
            Console.WriteLine("Database file created: " + DatabasePath);
        }
        else
        {
            Console.WriteLine("Database file already exists: " + DatabasePath);
        }
    }
    
    

}

